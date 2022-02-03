using System;
using System.IO;
using DisplayService.Model;
using DisplayService.Settings;
using SkiaSharp;

namespace DisplayService.Services
{
    public class RenderService : IRenderService
    {
        private readonly IRenderSettings renderSettings = null;
        private readonly SKBitmap screen;
        private readonly SKCanvas canvas;

        private bool disposed = false;

        public RenderService(IRenderSettings renderSettings)
        {
            this.renderSettings = renderSettings;

            screen = new SKBitmap(this.renderSettings.Width, this.renderSettings.Height);
            canvas = new SKCanvas(screen);
            canvas.Clear(this.renderSettings.Background);

           //if (this.cacheService.Exists())
           //{
           //    this.AddImage(new RenderActions.Image { X = 0, Y = 0, Filename = this.cacheService.CacheFile });
           //}
        }

        public void Clear()
        {
            Console.WriteLine($"Clear(color={renderSettings.Background})");
            this.canvas.Clear(renderSettings.Background);

            //if (clearState && renderSettings.Statefolder != null)
            //{
            //    string screenpath = renderSettings.Statefolder + "IoTDisplayScreen.png";
            //
            //    if (File.Exists(screenpath))
            //    {
            //        File.Delete(screenpath);
            //    }
            //}

            OnScreenChanged(0, 0, renderSettings.Width, renderSettings.Height, false, "clear", null);
        }

        public void Refresh()
        {
            RefreshScreen();
        }

        public void Image(RenderActions.Image image)
        {
            AddImage(image);
        }

        public void Graphic(RenderActions.Graphic graphic)
        {
            AddGraphic(graphic);
        }

        public void Text(RenderActions.Text text)
        {
            int width = 0;
            int height = 0;

            if (string.IsNullOrWhiteSpace(text.Value))
            {
                return;
            }

            if (text.FontSize == 0)
            {
                text.FontSize = 32;
            }

            text.Value = text.Value.Replace("\r", " ").Replace("\n", string.Empty);
            SKPaint paint = null;
            int horizontalOffset, verticalOffset, left, top;
            try
            {
                (paint, width, height, horizontalOffset, verticalOffset, left, top) = RenderTools.GetPaint(renderSettings, text.X, text.Y, text.Value, text.HorizAlign, text.VertAlign, text.Font, text.FontSize, text.FontWeight, text.FontWidth, text.HexColor, text.Bold);
                var textXPosition = text.X + horizontalOffset;
                var textYPosition = text.Y + verticalOffset;
                Console.WriteLine($"DrawText(text=\"{ text.Value}\", x={textXPosition}, y={textYPosition})");
                canvas.DrawText(text.Value, text.X + horizontalOffset, text.Y + verticalOffset, paint);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("An exception occurred trying to add text to the canvas:" + ex.Message, nameof(text), ex);
            }
            finally
            {
                if (paint != null)
                {
                    paint.Dispose();
                }
            }

            OnScreenChanged(left, top, width, height, text.Delay, "text", null /*JsonSerializer.Serialize<RenderActions.Text>(text)*/);
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

            if (x < renderSettings.Width && y < renderSettings.Height)
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
                if (renderSettings.Rotation == 0)
                {
                    screen.Encode(wstream, SKEncodedImageFormat.Png, 100);
                }
                else
                {
                    int newWidth = renderSettings.Width;
                    int newHeight = renderSettings.Height;

                    if (renderSettings.IsPortrait)
                    {
                        newWidth = renderSettings.Height;
                        newHeight = renderSettings.Width;
                    }

                    using SKBitmap image = new(newWidth, newHeight, screen.ColorType, screen.AlphaType, screen.ColorSpace);

                    using SKCanvas surface = new(image);
                    surface.Translate(newWidth, 0);
                    surface.RotateDegrees(renderSettings.Rotation);
                    surface.DrawBitmap(screen, 0, 0);
                    image.Encode(wstream, SKEncodedImageFormat.Png, 100);
                }
            }

            memoryStream.Position = 0;
            return memoryStream;
        }

        private void RefreshScreen()
        {
            canvas.Clear(renderSettings.Background);
            //Import();

            OnScreenChanged(0, 0, renderSettings.Width, renderSettings.Height, false, "refresh", null);
        }

        private void AddImage(RenderActions.Image image)
        {
            int width = 0;
            int height = 0;
            try
            {
                using SKBitmap img = RenderTools.GetImage(renderSettings, image.X, image.Y, image.Filename);
                Console.WriteLine($"DrawBitmap(img.ByteCount=\"{img.ByteCount}\", image.X={image.X}, image.Y={image.Y})");
                canvas.DrawBitmap(img, image.X, image.Y);
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

            OnScreenChanged(image.X, image.Y, width, height, image.Delay, "image", null /*JsonSerializer.Serialize<RenderActions.Image>(image)*/);
        }

        private void AddGraphic(RenderActions.Graphic graphic)
        {
            if (graphic.X < 0 || graphic.X >= renderSettings.Width)
            {
                throw new ArgumentOutOfRangeException(nameof(graphic.X), graphic.X, "X coordinate is not within the screen");
            }

            if (graphic.Y < 0 || graphic.Y >= renderSettings.Height)
            {
                throw new ArgumentOutOfRangeException(nameof(graphic.Y), graphic.Y, "Y coordinate is not within the screen");
            }

            int width = 0;
            int height = 0;
            try
            {
                using SKBitmap img = SKBitmap.Decode(graphic.Data);
                Console.WriteLine($"DrawBitmap(img.ByteCount=\"{img.ByteCount}\", graphic.X={graphic.X}, graphic.Y={graphic.Y})");
                canvas.DrawBitmap(img, graphic.X, graphic.Y);
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

            OnScreenChanged(graphic.X, graphic.Y, width, height, graphic.Delay, "graphic", null);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                canvas.Dispose();
                screen.Dispose();
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~RenderService() => Dispose(false);

    }
}
