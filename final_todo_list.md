# Media Viewer Application To-Do List

## Status Summary
All major development tasks are marked as Completed. The project architecture is finalized.

## Checklist
[x] Set up the .NET MAUI project structure for a media viewer application.
[x] Implement folder selection functionality allowing users to select local directories containing photos and videos.
[x] Develop a robust media scanning service that recursively reads selected folders, identifying image (JPEG, PNG, etc.) and video files.
[x] Define data models for media items, including file path, display name, MIME type, and thumbnail generation status.
[x] Design the main Gallery View UI: Display scanned media items in a grid/list format with thumbnails.
[x] Implement the logic to toggle between "View All" (gallery view) and "Select Individual Item" modes.
[x] Develop the Media Viewer component: This single-purpose view must handle both image display and video playback efficiently.
[x] Integrate an optimized image loading mechanism (e.g., using caching or dedicated libraries) to ensure fast UI performance when viewing photos.
[x] Select and integrate a cross-platform media player library for .NET MAUI, ensuring it supports basic video playback initialization.
[x] Abstract video playback logic into a service layer (IVideoService), defining methods like `PlayVideoAsync(string filePath)` and `StopVideoAsync()`.
[x] Define the platform abstraction layer: Create interfaces/abstract classes for native media player controls (e.g., `IPlatformMediaPlayer`).
[x] Implement Android-specific handlers using MAUI's dependency injection or platform services to manage video playback lifecycle.
[x] Implement iOS-specific handlers utilizing AVPlayer or similar frameworks within the abstract service layer.
[x] Implement Windows-specific handlers leveraging WinUI/MediaElement for robust media stream management.
[x] Update the Media Viewer ViewModel to manage video state (playing/paused/error) and interact with IVideoService.
[x] Add user feedback mechanisms and polish the overall UI/UX to meet "polished and fast" requirements.
[x] Write unit tests for media scanning, data model integrity, and core view logic.