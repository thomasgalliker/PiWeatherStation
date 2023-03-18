using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using DisplayService.Services;

namespace DisplayService.Model
{
    public partial class RenderActions
    {
        [DebuggerDisplay("Canvas: X={X}, Y={Y}, Width={Width}, Height={Height}")]
        public class Canvas : Collection<IRenderAction>, IRenderAction, ISurface
        {
            public Canvas()
            {
            }

            public int X { get; set; }

            public int Y { get; set; }

            public int Width { get; set; }

            public int Height { get; set; }

            public HorizontalAlignment HorizontalAlignment { get; set; }

            public VerticalAlignment VerticalAlignment { get; set; }

            public string BackgroundColor { get; set; } = "#00000000";

            public void Render(IRenderService renderService)
            {
                renderService.Canvas(this);
            }
        }
    }
}
