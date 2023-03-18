using System;
using System.Collections.Generic;
using System.Diagnostics;
using DisplayService.Services;

namespace DisplayService.Model
{
    public partial class RenderActions
    {
        [DebuggerDisplay("StackLayout: X={X}, Y={Y}, Width={Width}, Height={Height}")]
        public class StackLayout : IRenderAction, ISurface
        {
            private int spacing;

            public StackLayout()
            {
                this.Children = new List<IRenderAction>();
            }

            public int X { get; set; }

            public int Y { get; set; }

            public int Width { get; set; }

            public int Height { get; set; }

            public HorizontalAlignment HorizontalAlignment { get; set; }

            public VerticalAlignment VerticalAlignment { get; set; }

            public string BackgroundColor { get; set; } = "#00000000";

            public StackOrientation Orientation { get; set; } = StackOrientation.Horizontal;

            public int Spacing
            {
                get => this.spacing;
                set
                {
                    if (value < 0)
                    {
                        throw new ArgumentException("Spacing must be a positive number.", nameof(this.Spacing));
                    }

                    this.spacing = value;
                }
            }

            public ICollection<IRenderAction> Children { get; set; }

            public void Render(IRenderService renderService)
            {
                renderService.StackLayout(this);
            }
        }
    }
}
