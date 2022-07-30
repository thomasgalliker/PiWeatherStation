namespace WeatherDisplay.Compilations
{
    public interface IDisplayCompilation
    {
        string Name { get; }

        void AddRenderActions();
    }
}