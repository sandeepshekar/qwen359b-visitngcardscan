using Xunit;
using System;
using Qwen359b.Models;

namespace Tests
{
    public class MediaItemTests
    {
        [Fact]
        public void MediaItem_Constructor_ShouldInitializePropertiesCorrectly()
        {
            // Arrange
            string testPath = "C:\\path\\to\\test.jpg";
            string displayName = "Test Image";
            string mediaType = "Image";

            // Act
            var item = new MediaItem(testPath, displayName, mediaType);

            // Assert
            Assert.Equal(testPath, item.FilePath);
            Assert.Equal(displayName, item.DisplayName);
            Assert.Equal(mediaType, item.MediaType);
        }

        [Fact]
        public void MediaItem_ShouldHandleNullOrEmptyInputsGracefully()
        {
            // Arrange
            string nullPath = null;
            string emptyName = "";
            string unknownType = "Unknown";

            // Act & Assert (Assuming the model handles these gracefully or throws expected exceptions)
            var item1 = new MediaItem(nullPath, "", unknownType);
            Assert.Null(item1.FilePath); // Or assert it defaults to a safe value if validation is in place
            Assert.Empty(item1.DisplayName);
        }

        [Fact]
        public void MediaItem_ShouldUpdatePropertiesCorrectly()
        {
            // Arrange
            var item = new MediaItem("old/path.jpg", "Old Name", "Image");

            // Act
            string newPath = "new/updated/path.png";
            string newName = "New Awesome Photo";
            string newType = "Image"; // Assuming type might change or be confirmed later

            item.FilePath = newPath;
            item.DisplayName = newName;
            // If MediaType is read-only, we test that it remains constant or can only be set via a specific method.
            // For simplicity, assuming direct property setting for now.
            item.MediaType = newType; 

            // Assert
            Assert.Equal(newPath, item.FilePath);
            Assert.Equal(newName, item.DisplayName);
            Assert.Equal(newType, item.MediaType);
        }
    }
}