using Xunit;
using System.IO;
using System.Linq;
using Qwen359b.Services;
using Qwen359b.Models;

namespace Tests
{
    public class MediaScannerServiceTests
    {
        [Fact]
        public async Task ScanDirectory_ShouldIdentifyMediaFiles()
        {
            // Arrange
            var service = new MediaScannerService();
            string testPath = Path.Combine(Path.GetTempPath(), "test_media_dir");

            try
            {
                // Create dummy files/folders for testing purposes (In a real scenario, this setup would be more robust)
                Directory.CreateDirectory(testPath);
                File.WriteAllText(Path.Combine(testPath, "image1.jpg"), "dummy content");
                File.WriteAllText(Path.Combine(testPath, "video1.mp4"), "dummy content");
                File.WriteAllText(Path.Combine(testPath, "document.txt"), "not a media file");

                // Act
                var result = await service.ScanDirectoriesAsync(new[] { testPath });

                // Assert
                Assert.NotEmpty(result);
                Assert.Contains(result, item => item.FileName == "image1.jpg" && item.MediaType == MediaType.Image);
                Assert.Contains(result, item => item.FileName == "video1.mp4" && item.MediaType == MediaType.Video);
                Assert.DoesNotContain(result, item => item.FileName == "document.txt");
            }
            finally
            {
                // Cleanup (Optional but good practice)
                if (Directory.Exists(testPath))
                {
                    Directory.Delete(testPath, true);
                }
            }
        }
    }
}