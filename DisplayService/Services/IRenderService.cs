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

        void Graphic(RenderActions.Graphic graphic);

        void Text(RenderActions.Text text);

        void Rectangle(RenderActions.Rectangle rectangle);
        
        void StackLayout(RenderActions.StackLayout stackLayout);
    }
}
