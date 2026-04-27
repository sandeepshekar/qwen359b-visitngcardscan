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
            // Arrange - The current MediaItem model doesn't have a constructor with these parameters
            // This test needs to be updated to match the actual model structure
            var item = new MediaItem
            {
                FilePath = "C:\\path\\to\\test.jpg",
                FileName = "Test Image",
                MediaType = MediaType.Image,
                ThumbnailSource = string.Empty
            };

            // Assert
            Assert.Equal("C:\\path\\to\\test.jpg", item.FilePath);
            Assert.Equal("Test Image", item.FileName);
            Assert.Equal(MediaType.Image, item.MediaType);
        }

        [Fact]
        public void MediaItem_ShouldHandleNullOrEmptyInputsGracefully()
        {
            // Arrange
            var item1 = new MediaItem
            {
                FilePath = null,
                FileName = string.Empty,
                MediaType = MediaType.Unknown,
                ThumbnailSource = string.Empty
            };

            // Assert
            Assert.Null(item1.FilePath);
            Assert.Empty(item1.FileName);
        }

        [Fact]
        public void MediaItem_ShouldUpdatePropertiesCorrectly()
        {
            // Arrange
            var item = new MediaItem
            {
                FilePath = "old/path.jpg",
                FileName = "Old Name",
                MediaType = MediaType.Image,
                ThumbnailSource = string.Empty
            };

            // Act
            item.FilePath = "new/updated/path.png";
            item.FileName = "New Awesome Photo";
            item.MediaType = MediaType.Video;

            // Assert
            Assert.Equal("new/updated/path.png", item.FilePath);
            Assert.Equal("New Awesome Photo", item.FileName);
            Assert.Equal(MediaType.Video, item.MediaType);
        }
    }
}