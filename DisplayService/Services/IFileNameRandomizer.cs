namespace DisplayService.Services
{
    public interface IFileNameRandomizer
    {
        /// <summary>
        /// Returns the next randomized file name or file path.
        /// </summary>
        /// <param name="path">The file name or file path pattern.</param>
        /// <returns>The randomized file name or file path.</returns>
        string Next(string path);
    }
}