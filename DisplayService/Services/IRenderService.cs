using System;
using System.IO;
using DisplayService.Model;

namespace DisplayService.Services
{
    public interface IRenderService : IDisposable
    {
        Stream GetScreen();

        void Clear();

        void Render(params IRenderAction[] renderActions);

        void Image(RenderActions.Image image);
        //void Bitmap(RenderActions.BitmapImage image);
        //void Bitmap(RenderActions.FileImage image);

        void SvgImage(RenderActions.SvgImage svgImage);

        void Text(RenderActions.Text text);

        void Rectangle(RenderActions.Rectangle rectangle);
        
        void Canvas(RenderActions.Canvas canvas);

        void StackLayout(RenderActions.StackLayout stackLayout);
    }
}
