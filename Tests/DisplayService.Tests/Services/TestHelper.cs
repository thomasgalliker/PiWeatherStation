using System.IO;
using System.Runtime.CompilerServices;
using Xunit.Abstractions;

namespace DisplayService.Tests.Services
{
    internal class TestHelper
    {
        private readonly ITestOutputHelper testOutputHelper;

        public TestHelper(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        internal void WriteFile(Stream bitmapStream, [CallerMemberName] string callerMemberName = null)
        {
            var testFile = Path.GetFullPath($"./{callerMemberName}.png");
            using (var fileStream = new FileStream(testFile, FileMode.Create, FileAccess.Write))
            {
                bitmapStream.CopyTo(fileStream);
            }

            this.testOutputHelper.WriteLine($"Testfile successfully written to: {testFile}");
        }
    }
}