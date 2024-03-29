﻿using System;
using System.Diagnostics;
using DisplayService.Services;

namespace DisplayService.Model
{
    [DebuggerDisplay("Text: Value={Value}, X={X}, Y={Y}")]
    public partial class RenderActions
    {
        /// <summary>
        /// Places text on the screen
        /// </summary>
        public class Text : IRenderAction, IAlignable, ICoordinates
        {
            private float fontSize = 32;
            private string value;

            /// <summary>
            /// X coordinate to place the text.
            /// </summary>
            /// <example>10</example>
            public int X { get; set; }

            /// <summary>
            /// Y coordinate to place the text.
            /// </summary>
            /// <example>100</example>
            public int Y { get; set; }

            /// <summary>
            /// Text value.
            /// </summary>
            public string Value
            {
                get => this.value;
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        throw new ArgumentException("Value must not be null or empty", nameof(this.Value));
                    }

                    this.value = value;
                }
            }

            /// <summary>
            /// Text horizontal alignment.
            /// </summary>
            public HorizontalAlignment HorizontalTextAlignment { get; set; }

            /// <summary>
            /// Text vertical alignment.
            /// </summary>
            public VerticalAlignment VerticalTextAlignment { get; set; }

            /// <summary>
            /// Filename or font family of the font to use (optional)
            /// </summary>
            /// <example>/home/pi/NotoSans-Black.ttf</example>
            public string Font { get; set; }

            public bool Bold { get; set; }

            /// <summary>
            /// Font size of the text (optional)
            /// </summary>
            /// <example>60</example>
            public float FontSize
            {
                get => this.fontSize;
                set
                {
                    if (value <= 0)
                    {
                        throw new ArgumentException("FontSize must be greater than 0.", nameof(this.FontSize));
                    }

                    this.fontSize = value;
                }
            }

            public bool AdjustsFontSizeToFitWidth { get; set; }

            public bool AdjustsFontSizeToFitHeight { get; set; }

            /// <summary>
            /// Font weight of the text (100 - 900, optional)
            /// </summary>
            /// <example>400</example>
            public int FontWeight { get; set; } = 400;

            /// <summary>
            /// Font width of the text (1 - 9, optional)
            /// </summary>
            /// <example>5</example>
            public int FontWidth { get; set; } = 5;

            /// <summary>
            /// Hex color string representing the color of the text (optional)
            /// </summary>
            /// <example>#000000</example>
            public string ForegroundColor { get; set; } = "#FF000000";

            public string BackgroundColor { get; set; }

            public HorizontalAlignment HorizontalAlignment { get; set; }

            public VerticalAlignment VerticalAlignment { get; set; }

            public void Render(IRenderService renderService)
            {
                renderService.Text(this);
            }
        }
    }
}
