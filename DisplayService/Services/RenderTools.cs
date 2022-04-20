
using System;
using System.IO;
using System.Text.RegularExpressions;
using DisplayService.Model;
using DisplayService.Settings;
using SkiaSharp;

namespace DisplayService.Services
{
    /// <summary>
    /// Tools used for rendering
    /// </summary>
    public static class RenderTools
    {
        /// <summary>
        /// Clean up a file name of unsupported characters
        /// </summary>
        /// <param name="name">File name to clean</param>
        /// <returns>Returns a cleaned up file name</returns>
        public static string CleanFileName(string name)
        {
            string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            string invalidRegStr = string.Format(@"([{0}]*\/\\\.+$)|([{0}]+)", invalidChars);

            return Regex.Replace(name, invalidRegStr, "_");
        }

        /// <summary>
        /// Get a TypeFace for a font
        /// </summary>
        /// <param name="font">Font for the TypeFace</param>
        /// <param name="weight">Weight of the font</param>
        /// <param name="width">Width of the font</param>
        /// <returns>Returns a TypeFace</returns>
        public static SKTypeface GetTypeface(string font, int weight, int width)
        {
            SKTypeface typeface = null;
            if (!string.IsNullOrWhiteSpace(font))
            {
                try
                {
                    if (File.Exists(font))
                    {
                        typeface = SKTypeface.FromFile(font);
                    }
                    else if (weight > 0 && width > 0)
                    {
                        typeface = SKTypeface.FromFamilyName(font, weight, width, SKFontStyleSlant.Upright);
                    }
                    else if (weight > 0)
                    {
                        typeface = SKTypeface.FromFamilyName(font, weight, 5, SKFontStyleSlant.Upright);
                    }
                    else if (width > 0)
                    {
                        typeface = SKTypeface.FromFamilyName(font, 400, width, SKFontStyleSlant.Upright);
                    }
                    else
                    {
                        typeface = SKTypeface.FromFamilyName(font);
                    }
                }
                catch
                {
                    typeface = null;
                }
            }

            return typeface;
        }

        /// <summary>
        /// Get an image
        /// </summary>
        /// <param name="settings">Render settings to use</param>
        /// <param name="x">X coordinate to use</param>
        /// <param name="y">Y coordinate to use</param>
        /// <param name="filename">File name to get</param>
        /// <returns>Returns a bitmap of the image</returns>
        public static SKBitmap GetImage(IRenderSettings settings, int x, int y, string filename)
        {
            if (x < 0 || x >= settings.Width)
            {
                throw new ArgumentOutOfRangeException(nameof(x), x, "X coordinate is not within the screen");
            }

            if (y < 0 || y >= settings.Height)
            {
                throw new ArgumentOutOfRangeException(nameof(y), y, "Y coordinate is not within the screen");
            }

            if (!File.Exists(filename))
            {
                throw new ArgumentException("File not found", nameof(filename));
            }

            SKBitmap skBitmap = null;

            try
            {
                skBitmap = SKBitmap.Decode(filename) ?? throw new ArgumentException("Unable to decode image", nameof(filename));
            }
            catch (Exception ex)
            {
                if (skBitmap != null)
                {
                    skBitmap.Dispose();
                }

                throw new ArgumentException("An exception occurred trying to load image: " + ex.Message, nameof(filename), ex);
            }

            return skBitmap;
        }

        public static SKBitmap GetImage(IRenderSettings settings, int x, int y, Stream imageStream)
        {
            if (x < 0 || x >= settings.Width)
            {
                throw new ArgumentOutOfRangeException(nameof(x), x, "X coordinate is not within the screen");
            }

            if (y < 0 || y >= settings.Height)
            {
                throw new ArgumentOutOfRangeException(nameof(y), y, "Y coordinate is not within the screen");
            }

            if (imageStream == null || imageStream.Length == 0)
            {
                throw new ArgumentException("Image stream must not be empty", nameof(imageStream));
            }

            SKBitmap skBitmap = null;

            try
            {
                skBitmap = SKBitmap.Decode(imageStream) ?? throw new ArgumentException("Unable to decode image", nameof(imageStream));
            }
            catch (Exception ex)
            {
                if (skBitmap != null)
                {
                    skBitmap.Dispose();
                }

                throw new ArgumentException("An exception occurred trying to load image: " + ex.Message, nameof(imageStream), ex);
            }

            return skBitmap;
        }


        /// <summary>
        /// Get a paint object for text
        /// </summary>
        /// <param name="font">Font to use</param>
        /// <param name="fontSize">Font size</param>
        /// <param name="fontWeight">Font weight</param>
        /// <param name="fontWidth">Font width</param>
        /// <param name="foregroundColor">Font hex color</param>
        /// <param name="bold">Bold setting</param>
        /// <returns>Returns a paint object for the text</returns>
        // TODO: Split this very chaotic method into several single-purpose methods
        public static SKPaint GetPaint(string font, float fontSize, int fontWeight, int fontWidth, string foregroundColor, bool bold)
        {
            if (fontSize <= 0 || fontSize > 9999)
            {
                throw new ArgumentOutOfRangeException(nameof(fontSize), fontSize, "Font size must be greater than zero and less than 10000");
            }

            if ((fontWeight < 100 && fontWeight != 0) || fontWeight > 900)
            {
                throw new ArgumentOutOfRangeException(nameof(fontWeight), fontWeight, "Font weight must be between 100 and 900");
            }

            if (fontWidth < 0 || fontWidth > 9)
            {
                throw new ArgumentOutOfRangeException(nameof(fontWidth), fontWidth, "Font width must be between 1 and to 9");
            }

            SKPaint paint = new()
            {
                TextSize = fontSize,
                IsAntialias = true,
            };
            if (!string.IsNullOrWhiteSpace(font))
            {
                paint.Typeface = GetTypeface(font, fontWeight, fontWidth) ?? throw new ArgumentException("Font not found", nameof(font));
            }

            if (string.IsNullOrWhiteSpace(foregroundColor))
            {
                paint.Color = new(0, 0, 0);
            }
            else
            {
                try
                {
                    paint.Color = SKColor.Parse(foregroundColor);
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException("Invalid hexColor", nameof(foregroundColor), ex);
                }
            }

            paint.FakeBoldText = bold;
            paint.IsStroke = false;

            return paint;
        }

        public static (int width, int height, int horizontalOffset, int verticalOffset, int left, int top) GetBounds(int x, int y, string text, HorizontalAlignment horizontalTextAlignment, VerticalAlignment verticalTextAlignment, SKPaint paint)
        {
            var rect = new SKRect();
            var width = paint.MeasureText(text, ref rect);
            var height = rect.Height;

            (float horizontalOffset, float left) = CalculateHorizontalBounds(x, horizontalTextAlignment, rect, width);

            (float verticalOffset, float top) = CalculateVerticalBounds(y, verticalTextAlignment, rect);

            return ((int)Math.Round(width), (int)Math.Round(height), (int)Math.Round(horizontalOffset), (int)Math.Round(verticalOffset), (int)Math.Round(left), (int)Math.Round(top));
        }

        //public static (int width, int height, int horizontalOffset, int verticalOffset, int left, int top) GetBounds(int x, int y, SKRect rect, HorizontalAlignment horizontalTextAlignment, VerticalAlignment verticalTextAlignment, SKPaint paint)
        //{
        //    var width = rect.Width;
        //    var height = rect.Height;
        //
        //    (float horizontalOffset, float left) = CalculateHorizontalBounds(x, horizontalTextAlignment, rect, width);
        //
        //    (float verticalOffset, float top) = CalculateVerticalBounds(y, verticalTextAlignment, rect);
        //
        //    return ((int)Math.Round(width), (int)Math.Round(height), (int)Math.Round(horizontalOffset), (int)Math.Round(verticalOffset), (int)Math.Round(left), (int)Math.Round(top));
        //}

        private static (float horizontalOffset, float left) CalculateHorizontalBounds(int x, HorizontalAlignment horizontalAlignment, SKRect rect, float width)
        {
            float horizontalOffset;
            float left;

            if (horizontalAlignment == HorizontalAlignment.Left)
            {
                horizontalOffset = 0;
                left = x + rect.Left;
            }
            else if (horizontalAlignment == HorizontalAlignment.Right)
            {
                horizontalOffset = 1 - width - rect.Left;
                left = x - width + 1;
            }
            else
            {
                horizontalOffset = 0 - rect.MidX;
                left = x - rect.MidX + rect.Left;
            }

            return (horizontalOffset, left);
        }

        private static (float verticalOffset, float top) CalculateVerticalBounds(int y, VerticalAlignment verticalAlignment, SKRect rect)
        {
            float verticalOffset;
            float top;

            if (verticalAlignment == VerticalAlignment.Top)
            {
                verticalOffset = 0 - rect.Top;
                top = y;
            }
            else if (verticalAlignment == VerticalAlignment.Bottom)
            {
                verticalOffset = 0;
                top = y + rect.Top;
            }
            else
            {
                verticalOffset = 1 - rect.MidY;
                top = y - rect.MidY + rect.Top + 1;
            }

            return (verticalOffset, top);
        }
    }
}