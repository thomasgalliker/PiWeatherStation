using System;
using System.IO;
using DisplayService.Model;
using DisplayService.Settings;
using Microsoft.Extensions.Logging;
using SkiaSharp;

namespace DisplayService.Services
{
    public class RenderService : IRenderService
    {
        private readonly ILogger<RenderService> logger;
        private readonly IRenderSettings renderSettings = null;
        private readonly SKBitmap screen;
        private readonly SKCanvas canvas;

        private bool disposed = false;

        public RenderService(ILogger<RenderService> logger, IRenderSettings renderSettings)
        {
            this.logger = logger;
            this.renderSettings = renderSettings;

            this.screen = new SKBitmap(this.renderSettings.Width, this.renderSettings.Height);
            this.canvas = new SKCanvas(this.screen);
            this.ClearCanvas();

            //if (this.cacheService.Exists())
            //{
            //    this.AddImage(new RenderActions.Image { X = 0, Y = 0, Filename = this.cacheService.CacheFile });
            //}
        }

        private void ClearCanvas()
        {
            this.logger.LogDebug($"Clear(color={this.renderSettings.BackgroundColor})");
            this.canvas.Clear(SKColor.Parse(this.renderSettings.BackgroundColor));
        }

        public void Clear()
        {
            this.ClearCanvas();

            this.OnScreenChanged(0, 0, this.renderSettings.Width, this.renderSettings.Height, false, "clear", null);
        }

        public void Refresh()
        {
            this.RefreshScreen();
        }

        public void Image(RenderActions.Image image)
        {
            this.AddImage(image);
        }

        public void Graphic(RenderActions.Graphic graphic)
        {
            this.AddGraphic(graphic);
        }

        public void Text(RenderActions.Text text)
        {
            text.Value = text.Value.Replace("\r", " ").Replace("\n", string.Empty);

            try
            {
                var paint = RenderTools.GetPaint(text.Font, text.FontSize, text.FontWeight, text.FontWidth, text.ForegroundColor, text.Bold);
                var (width, height, horizontalOffset, verticalOffset, left, top) = RenderTools.GetBounds(text.X, text.Y, text.Value, text.HorizontalTextAlignment, text.VerticalTextAlignment, paint);
                var textXPosition = text.X + horizontalOffset;
                var textYPosition = text.Y + verticalOffset;

                // Draw text background
                var backgroundPaint = new SKPaint { Color = SKColor.Parse(text.BackgroundColor) };
                this.canvas.DrawRect(x: left, y: top, w: width, h: height, backgroundPaint);
                backgroundPaint.Dispose();

                // Draw text foreground
                this.logger.LogDebug($"DrawText(text=\"{ text.Value}\", x={textXPosition}, y={textYPosition})");
                this.canvas.DrawText(text.Value, textXPosition, textYPosition, paint);
            }
            catch (Exception ex)
            {
                throw new Exception($"DrawText failed with exception: {ex.Message}", ex);
            }
        }

        public void Rectangle(RenderActions.Rectangle rectangle)
        {
            try
            {
                float x = rectangle.X;
                switch (rectangle.HorizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        x = rectangle.X;
                        break;
                    case HorizontalAlignment.Center:
                        x = rectangle.X - (rectangle.Width / 2);
                        break;
                    case HorizontalAlignment.Right:
                        x = rectangle.X - rectangle.Width;
                        break;
                }

                float y = rectangle.Y;
                switch (rectangle.VerticalAlignment)
                {
                    case VerticalAlignment.Top:
                        y = rectangle.Y;
                        break;
                    case VerticalAlignment.Center:
                        y = rectangle.Y - (rectangle.Height / 2);
                        break;
                    case VerticalAlignment.Bottom:
                        y = rectangle.Y - rectangle.Height;
                        break;
                }

                this.logger.LogDebug($"DrawRect(x: {x}, y: {y}, width: {rectangle.Width}, height: {rectangle.Height})");

                var rect = SKRect.Create(x, y, width: rectangle.Width, height: rectangle.Height);

                // the brush (fill with blue)
                var paint = new SKPaint
                {
                    Style = SKPaintStyle.Fill,
                    Color = SKColor.Parse(rectangle.BackgroundColor),
                };

                // Draw fill
                this.canvas.DrawRect(rect, paint);

                // Draw stroke (if set)
                if (rectangle.StrokeWidth > 0)
                {
                    paint.Style = SKPaintStyle.Stroke;
                    paint.Color = SKColor.Parse(rectangle.StrokeColor);
                    paint.StrokeWidth = rectangle.StrokeWidth;
                    this.canvas.DrawRect(rect, paint);
                }

                paint.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception($"DrawRect failed with exception: {ex.Message}", ex);
            }
        }

        [Obsolete]
        protected virtual void OnScreenChanged(int x, int y, int width, int height, bool delay, string command, string values)
        {
            //if (persist)
            {
                //Export(command + (values == null ? string.Empty : "\t" + values));
            }

            // Clip to ensure dimensions are within screen
            int hoffset = x < 0 ? 0 - x : 0;
            x += hoffset;
            width -= hoffset;

            int voffset = y < 0 ? 0 - y : 0;
            y += voffset;
            height -= voffset;

            if (x < this.renderSettings.Width && y < this.renderSettings.Height)
            {
                //ScreenChangedEventArgs evt = new()
                //{
                //    X = x,
                //    Y = y,
                //    Width = Math.Min(width, _renderSettings.Width - x),
                //    Height = Math.Min(height, _renderSettings.Height - y),
                //    Delay = delay,
                //};
                //ScreenChanged?.Invoke(this, evt);
            }
        }

        public Stream GetScreen()
        {
            var memoryStream = new MemoryStream();
            using (var wstream = new SKManagedWStream(memoryStream))
            {
                if (this.renderSettings.Rotation == 0)
                {
                    this.screen.Encode(wstream, SKEncodedImageFormat.Png, 100);
                }
                else
                {
                    var newWidth = this.renderSettings.Width;
                    var newHeight = this.renderSettings.Height;

                    if (this.renderSettings.IsPortrait)
                    {
                        newWidth = this.renderSettings.Height;
                        newHeight = this.renderSettings.Width;
                    }

                    using (var image = new SKBitmap(newWidth, newHeight, this.screen.ColorType, this.screen.AlphaType, this.screen.ColorSpace))
                    {
                        using (var surface = new SKCanvas(image))
                        {
                            surface.Translate(newWidth, 0);
                            surface.RotateDegrees(this.renderSettings.Rotation);
                            surface.DrawBitmap(this.screen, 0, 0);
                            image.Encode(wstream, SKEncodedImageFormat.Png, 100);
                        }
                    }
                }
            }

            memoryStream.Position = 0;
            return memoryStream;
        }

        private void RefreshScreen()
        {
            this.ClearCanvas();
            //Import();

            this.OnScreenChanged(0, 0, this.renderSettings.Width, this.renderSettings.Height, false, "refresh", null);
        }

        private void AddImage(RenderActions.Image image)
        {
            int width = 0;
            int height = 0;
            try
            {
                using SKBitmap img = RenderTools.GetImage(this.renderSettings, image.X, image.Y, image.Filename);
                this.logger.LogDebug($"DrawBitmap(img.ByteCount=\"{img.ByteCount}\", image.X={image.X}, image.Y={image.Y})");
                this.canvas.DrawBitmap(img, image.X, image.Y);
                width = img.Width;
                height = img.Height;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
#pragma warning disable CA2208 // Instantiate argument exceptions correctly
                throw new ArgumentException("An exception occurred trying to add image to the canvas: " + ex.Message, nameof(image.Filename), ex);
#pragma warning restore CA2208 // Instantiate argument exceptions correctly
            }

            this.OnScreenChanged(image.X, image.Y, width, height, image.Delay, "image", null /*JsonSerializer.Serialize<RenderActions.Image>(image)*/);
        }

        private void AddGraphic(RenderActions.Graphic graphic)
        {
            if (graphic.X < 0 || graphic.X >= this.renderSettings.Width)
            {
                throw new ArgumentOutOfRangeException(nameof(graphic.X), graphic.X, "X coordinate is not within the screen");
            }

            if (graphic.Y < 0 || graphic.Y >= this.renderSettings.Height)
            {
                throw new ArgumentOutOfRangeException(nameof(graphic.Y), graphic.Y, "Y coordinate is not within the screen");
            }

            int width = 0;
            int height = 0;
            try
            {
                using SKBitmap img = SKBitmap.Decode(graphic.Data);
                this.logger.LogDebug($"DrawBitmap(img.ByteCount=\"{img.ByteCount}\", graphic.X={graphic.X}, graphic.Y={graphic.Y})");
                this.canvas.DrawBitmap(img, graphic.X, graphic.Y);
                width = img.Width;
                height = img.Height;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
#pragma warning disable CA2208 // Instantiate argument exceptions correctly
                throw new ArgumentException("An exception occurred trying to add graphical image to the canvas: " + ex.Message, nameof(graphic.Data), ex);
#pragma warning restore CA2208 // Instantiate argument exceptions correctly
            }

            this.OnScreenChanged(graphic.X, graphic.Y, width, height, graphic.Delay, "graphic", null);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.canvas.Dispose();
                this.screen.Dispose();
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~RenderService() => this.Dispose(false);

    }
}
