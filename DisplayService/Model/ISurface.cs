namespace DisplayService.Model
{
    public interface ISurface : IAlignable, ICoordinates
    {
        public int Width { get; set; }

        public int Height { get; set; }
    }
}