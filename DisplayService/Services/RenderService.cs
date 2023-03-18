using System;
using System.IO;
using DisplayService.Model;
using DisplayService.Settings;
using Microsoft.Extensions.Logging;
using SkiaSharp;

namespace DisplayService.Services
{
    /// <summary>
    /// This service is responsible for rendering all the existing rendering actions
    /// into a bitmap image which can later be used to display on the display hardware.
    /// </summary>
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
            this.Clear();
        }

        public void Clear()
        {
            this.logger.LogDebug($"Clear(color={this.renderSettings.BackgroundColor})");
            this.canvas.Clear(SKColor.Parse(this.renderSettings.BackgroundColor));
        }

        public void Render(params IRenderAction[] renderActions)
        {
            foreach (var renderAction in renderActions)
            {
                renderAction.Render(this);
            }
        }

        public void Image(RenderActions.Image image)
        {
            this.Image(this.canvas, image);
        }

        public void Graphic(RenderActions.Graphic graphic)
        {
            this.Graphic(this.canvas, graphic);
        }

        public void Text(RenderActions.Text text)
        {
            this.Text(this.canvas, text);
        }

        private SKRect Text(SKCanvas canvas, RenderActions.Text text)
        {
            text.Value = text.Value.Replace("\r", " ").Replace("\n", string.Empty);

            try
            {
                var alignmentYOffset = CalculateAlignmentYOffset(canvas.DeviceClipBounds, text);

                var paint = RenderTools.GetPaint(text.Font, text.FontSize, text.FontWeight, text.FontWidth, text.ForegroundColor, text.Bold);
                var (width, height, horizontalOffset, verticalOffset, left, top) = RenderTools.GetBounds(text.X, text.Y, text.Value, text.HorizontalTextAlignment, text.VerticalTextAlignment, paint);


                verticalOffset += (int)alignmentYOffset;
                top += (int)alignmentYOffset;

                if (text.AdjustsFontSizeToFitWidth)
                {
                    var canvasWidth = canvas.DeviceClipBounds.Width;
                    var canvasHeight = canvas.DeviceClipBounds.Height;

                    if (left + width > canvasWidth)
                    {
                        var maxFontSize = GetMaxFontSize(canvasWidth - left, null, paint.Typeface, text.Value);
                        if (text.FontSize != maxFontSize)
                        {
                            // In case we got a change in font size,
                            // we retry to draw the text
                            text.FontSize = maxFontSize;
                            return this.Text(canvas, text);
                        }
                    }
                }

                if (text.AdjustsFontSizeToFitHeight)
                {
                    var canvasWidth = canvas.DeviceClipBounds.Width;
                    var canvasHeight = canvas.DeviceClipBounds.Height;

                    if (top + height > canvasHeight)
                    {
                        var maxFontSize = GetMaxFontSize(null, canvasHeight - top, paint.Typeface, text.Value);
                        if (text.FontSize != maxFontSize)
                        {
                            // In case we got a change in font size,
                            // we retry to draw the text
                            text.FontSize = maxFontSize;
                            return this.Text(canvas, text);
                        }
                    }
                }

                var textXPosition = text.X + horizontalOffset;
                var textYPosition = text.Y + verticalOffset;

                var backgroundRect = SKRect.Create(x: left, y: top, width: width, height: height);

                // Draw text background
                if (text.BackgroundColor != null)
                {
                    using (var backgroundPaint = new SKPaint { Color = SKColor.Parse(text.BackgroundColor) })
                    {
                        canvas.DrawRect(backgroundRect, backgroundPaint);
                    }
                }

                // Draw text foreground
                this.logger.LogDebug($"DrawText(text=\"{text.Value}\", x={textXPosition}, y={textYPosition})");
                canvas.DrawText(text.Value, textXPosition, textYPosition, paint);

                return backgroundRect;
            }
            catch (Exception ex)
            {
                throw new Exception($"Text failed with exception: {ex.Message}", ex);
            }
        }

        private static float GetMaxFontSize(double? maxWidth, double? maxHeight, SKTypeface typeface, string text, float degreeOfCertainty = 1f, float minFontSize = 1f, float maxFontSize = 1000f)
        {
            var max = maxFontSize; // The upper bound. We know the font size is below this value
            var min = minFontSize; // The lower bound, We know the font size is equal to or above this value
            var last = -1f; // The last calculated value.
            float value;
            while (true)
            {
                value = min + ((max - min) / 2); // Find the half way point between Max and Min
                using (var ft = new SKFont(typeface, value))
                using (var paint = new SKPaint(ft))
                {
                    var rect = new SKRect();
                    var width = paint.MeasureText(text, ref rect);
                    if ((maxWidth is double maxWidthValue && rect.Width > maxWidthValue) || (maxHeight is double maxHeightValue && rect.Height > maxHeightValue)) // Measure the string size at this font size
                    {
                        // The text size is too large
                        // therefore the max possible size is below value
                        last = value;
                        max = value;
                    }
                    else
                    {
                        // The text fits within the area
                        // therefore the min size is above or equal to value
                        min = value;

                        // Check if this value is within our degree of certainty
                        if (Math.Abs(last - value) <= degreeOfCertainty)
                        {
                            return last; // Value is within certainty range, we found the best font size!
                        }

                        //This font difference is not within our degree of certainty
                        last = value;
                    }
                }
            }
        }

        public void Rectangle(RenderActions.Rectangle rectangle)
        {
            this.Rectangle(this.canvas, rectangle);
        }

        private SKRect Rectangle(SKCanvas canvas, RenderActions.Rectangle rectangle)
        {
            try
            {
                var x = CalculateX(rectangle);
                var y = CalculateY(rectangle);

                this.logger.LogDebug($"DrawRect(x: {x}, y: {y}, width: {rectangle.Width}, height: {rectangle.Height})");
                var rect = SKRect.Create(x, y, width: rectangle.Width, height: rectangle.Height);

                // Draw background color
                var paint = new SKPaint
                {
                    Style = SKPaintStyle.Fill,
                    Color = SKColor.Parse(rectangle.BackgroundColor),
                };
                canvas.DrawRect(rect, paint);

                // Draw stroke (if set)
                if (rectangle.StrokeWidth > 0)
                {
                    paint.Style = SKPaintStyle.Stroke;
                    paint.Color = SKColor.Parse(rectangle.StrokeColor);
                    paint.StrokeWidth = rectangle.StrokeWidth;

                    if (rectangle.CornerRadius > 0f)
                    {
                        canvas.DrawRoundRect(rect, rectangle.CornerRadius, rectangle.CornerRadius, paint);
                    }
                    else
                    {
                        canvas.DrawRect(rect, paint);
                    }
                }

                paint.Dispose();

                return rect;
            }
            catch (Exception ex)
            {
                throw new Exception($"Rectangle failed with exception: {ex.Message}", ex);
            }
        }

        public void Canvas(RenderActions.Canvas canvasObj)
        {
            this.Canvas(this.canvas, canvasObj);
        }

        private SKRect Canvas(SKCanvas canvas, RenderActions.Canvas canvasObj)
        {
            try
            {
                var x = CalculateX(canvasObj);
                var y = CalculateY(canvasObj);

                this.logger.LogDebug($"DrawRect(x: {x}, y: {y}, width: {canvasObj.Width}, height: {canvasObj.Height})");
                var canvasRect = SKRect.Create(x, y, width: canvasObj.Width, height: canvasObj.Height);

                using (var canvasImage = new SKBitmap(canvasObj.Width, canvasObj.Height, isOpaque: false))
                {
                    using (var innerCanvas = new SKCanvas(canvasImage))
                    {
                        // Draw background color
                        if (SKColor.TryParse(canvasObj.BackgroundColor, out var backgroundColor) && backgroundColor != SKColor.Empty)
                        {
                            var backgroundRect = SKRect.Create(x: 0, y: 0, width: canvasObj.Width, height: canvasObj.Height);
                            using (var backgroundPaint = new SKPaint
                            {
                                Style = SKPaintStyle.Fill,
                                Color = backgroundColor,
                            })
                            {
                                innerCanvas.DrawRect(backgroundRect, backgroundPaint);
                            }
                        }

                        {
                            foreach (var renderAction in canvasObj)
                            {
                                SKRect? renderedRect = null;

                                if (renderAction is RenderActions.Text text)
                                {
                                    renderedRect = this.Text(innerCanvas, text);
                                }
                                else if (renderAction is RenderActions.StackLayout innerStackLayout)
                                {
                                    renderedRect = this.StackLayout(innerCanvas, innerStackLayout);
                                }
                                else if (renderAction is RenderActions.Graphic graphic)
                                {
                                    renderedRect = this.Graphic(innerCanvas, graphic);
                                }
                                else if (renderAction is RenderActions.Image image)
                                {
                                    renderedRect = this.Image(innerCanvas, image);
                                }
                                else if (renderAction is RenderActions.Rectangle rectangle)
                                {
                                    renderedRect = this.Rectangle(innerCanvas, rectangle);
                                }
                                else
                                {
                                    throw new NotSupportedException($"Render action '{renderAction.GetType().Name}' is currently not supported");
                                }
                            }
                        }
                    }

                    canvas.DrawBitmap(canvasImage, canvasRect);

                    return canvasRect;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Canvas failed with exception: {ex.Message}", ex);
            }
        }

        public void StackLayout(RenderActions.StackLayout stackLayout)
        {
            this.StackLayout(this.canvas, stackLayout);
        }

        private SKRect StackLayout(SKCanvas canvas, RenderActions.StackLayout stackLayout)
        {
            try
            {
                var x = CalculateX(stackLayout);
                var y = CalculateY(stackLayout);

                this.logger.LogDebug($"DrawRect(x: {x}, y: {y}, width: {stackLayout.Width}, height: {stackLayout.Height})");
                var stackLayoutRect = SKRect.Create(x, y, width: stackLayout.Width, height: stackLayout.Height);

                using (var stackLayoutImage = new SKBitmap(stackLayout.Width, stackLayout.Height, isOpaque: false))
                {
                    using (var innerCanvas = new SKCanvas(stackLayoutImage))
                    {
                        // Draw background color
                        if (SKColor.TryParse(stackLayout.BackgroundColor, out var backgroundColor) && backgroundColor != SKColor.Empty)
                        {
                            var backgroundRect = SKRect.Create(x: 0, y: 0, width: stackLayout.Width, height: stackLayout.Height);
                            using (var backgroundPaint = new SKPaint
                            {
                                Style = SKPaintStyle.Fill,
                                Color = backgroundColor,
                            })
                            {
                                innerCanvas.DrawRect(backgroundRect, backgroundPaint);
                            }
                        }

                        if (stackLayout.Children != null)
                        {
                            var childOffset = 0;
                            foreach (var renderAction in stackLayout.Children)
                            {
                                SKRect? renderedRect = null;

                                if (renderAction is ICoordinates coordinates)
                                {
                                    if (stackLayout.Orientation == StackOrientation.Horizontal)
                                    {
                                        coordinates.X += childOffset;
                                    }
                                    else
                                    {
                                        coordinates.Y += childOffset;
                                    }
                                }

                                if (renderAction is RenderActions.Text text)
                                {
                                    renderedRect = this.Text(innerCanvas, text);
                                }
                                else if (renderAction is RenderActions.StackLayout innerStackLayout)
                                {
                                    renderedRect = this.StackLayout(innerCanvas, innerStackLayout);
                                }
                                else if (renderAction is RenderActions.Graphic graphic)
                                {
                                    renderedRect = this.Graphic(innerCanvas, graphic);
                                }
                                else if (renderAction is RenderActions.Image image)
                                {
                                    renderedRect = this.Image(innerCanvas, image);
                                }
                                else if (renderAction is RenderActions.Rectangle rectangle)
                                {
                                    renderedRect = this.Rectangle(innerCanvas, rectangle);
                                }
                                else
                                {
                                    throw new NotSupportedException($"Render action '{renderAction.GetType().Name}' is currently not supported");
                                }

                                if (renderedRect is SKRect r)
                                {
                                    childOffset += stackLayout.Orientation == StackOrientation.Horizontal ? (int)r.Width : (int)r.Height;
                                }

                                childOffset += stackLayout.Spacing;
                            }
                        }

                    }

                    canvas.DrawBitmap(stackLayoutImage, stackLayoutRect);

                    return stackLayoutRect;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"StackLayout failed with exception: {ex.Message}", ex);
            }
        }

        private static float CalculateAlignmentYOffset(SKRect canvas, IAlignable alignable)
        {
            float y = 0;
            switch (alignable.VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    y = canvas.Top;
                    break;
                case VerticalAlignment.Center:
                    y = canvas.Height / 2;
                    break;
                case VerticalAlignment.Bottom:
                    y = canvas.Bottom;
                    break;
            }

            return y;
        }

        private static float CalculateY(ISurface surface)
        {
            float y = surface.Y;
            switch (surface.VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    y = surface.Y;
                    break;
                case VerticalAlignment.Center:
                    y = surface.Y - (surface.Height / 2);
                    break;
                case VerticalAlignment.Bottom:
                    y = surface.Y - surface.Height;
                    break;
            }

            return y;
        }

        private static float CalculateX(ISurface surface)
        {
            float x = surface.X;
            switch (surface.HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    x = surface.X;
                    break;
                case HorizontalAlignment.Center:
                    x = surface.X - (surface.Width / 2);
                    break;
                case HorizontalAlignment.Right:
                    x = surface.X - surface.Width;
                    break;
            }

            return x;
        }

        public Stream GetScreen()
        {
            var memoryStream = new MemoryStream();
            using (var wStream = new SKManagedWStream(memoryStream))
            {
                if (this.renderSettings.Rotation == 0)
                {
                    this.screen.Encode(wStream, SKEncodedImageFormat.Png, 100);
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
                            image.Encode(wStream, SKEncodedImageFormat.Png, 100);
                        }
                    }
                }
            }

            memoryStream.Position = 0;
            return memoryStream;
        }

        private SKRect Image(SKCanvas canvas, RenderActions.Image image)
        {
            if (image is null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            SKBitmap skBitmap;

            if (image is RenderActions.FileImage fileImage)
            {
                skBitmap = RenderTools.GetImage(this.renderSettings, image.X, image.Y, fileImage.Filename);
            }
            else if (image is RenderActions.StreamImage embeddedResourceImage)
            {
                skBitmap = RenderTools.GetImage(this.renderSettings, image.X, image.Y, embeddedResourceImage.Image);
            }
            else
            {
                throw new NotSupportedException($"Image of type {image.GetType().Name} is not supported");
            }

            if (image.Width == -1)
            {
                image.Width = skBitmap.Width;
            }

            if (image.Height == -1)
            {
                image.Height = skBitmap.Height;
            }

            var x = CalculateX(image);
            var y = CalculateY(image);

            var rect = SKRect.Create(x, y, image.Width, image.Height);

            if (skBitmap != null)
            {
                // Draw image background
                if (image.BackgroundColor != null)
                {
                    using (var backgroundPaint = new SKPaint { Color = SKColor.Parse(image.BackgroundColor) })
                    {
                        canvas.DrawRect(rect, backgroundPaint);
                    }
                }

                // Draw image
                using (skBitmap)
                {
                    if (skBitmap.Width != image.Width || skBitmap.Height != image.Height)
                    {
                        using (var skBitmapResized = skBitmap.Resize(new SKImageInfo(image.Width, image.Height), SKFilterQuality.High))
                        {
                            this.logger.LogDebug($"DrawBitmap(img.ByteCount=\"{skBitmapResized.ByteCount}\", x={x}, y={y})");
                            canvas.DrawBitmap(skBitmapResized, x, y);
                        }
                    }
                    else
                    {
                        this.logger.LogDebug($"DrawBitmap(img.ByteCount=\"{skBitmap.ByteCount}\", x={x}, y={y})");
                        canvas.DrawBitmap(skBitmap, x, y);
                    }
                }
            }

            return rect;
        }

        private SKRect Graphic(SKCanvas canvas, RenderActions.Graphic graphic)
        {
            if (graphic.X < 0 || graphic.X >= this.renderSettings.Width)
            {
                throw new ArgumentOutOfRangeException(nameof(graphic.X), graphic.X, "X coordinate is not within the screen");
            }

            if (graphic.Y < 0 || graphic.Y >= this.renderSettings.Height)
            {
                throw new ArgumentOutOfRangeException(nameof(graphic.Y), graphic.Y, "Y coordinate is not within the screen");
            }

            try
            {
                using var img = SKBitmap.Decode(graphic.Data);
                this.logger.LogDebug($"DrawBitmap(img.ByteCount=\"{img.ByteCount}\", graphic.X={graphic.X}, graphic.Y={graphic.Y})");
                canvas.DrawBitmap(img, graphic.X, graphic.Y);

                return new SKRect(graphic.X, graphic.Y, img.Width, img.Height);
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
