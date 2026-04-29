using Xunit;
using System.IO;
using Qwen359b.Services;
using Qwen359b.Models;

namespace Tests
{
    public class MediaScannerServiceTests
    {
        [Fact]
        public void ScanDirectory_ShouldIdentifyMediaFiles()
        {
            // Arrange
            var service = new MediaScannerService();
            string testPath = "test_media_dir"; // Assume a temporary directory for testing

            // Create dummy files/folders for testing purposes (In a real scenario, this setup would be more robust)
            Directory.CreateDirectory(testPath);
            File.WriteAllText("test_media_dir/image1.jpg", "dummy content");
            File.WriteAllText("test_media_dir/video1.mp4", "dummy content");
            File.WriteAllText("test_media_dir/document.txt", "not a media file");

            // Act
            var result = service.ScanDirectory(testPath);

            // Assert
            Assert.True(result.Any());
            Assert.Contains(result, item => item.FileName == "image1.jpg" && item.MediaType == "Image");
            Assert.Contains(result, item => item.FileName == "video1.mp4" && item.MediaType == "Video");
            Assert.DoesNotContain(result, item => item.FileName == "document.txt");

            // Cleanup (Optional but good practice)
            Directory.Delete(testPath, true);
        }
    }
}