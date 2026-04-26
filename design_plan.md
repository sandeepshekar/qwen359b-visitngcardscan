# Media Viewer Application Architecture Design Plan

## 🎯 Project Goal
To design and architect a high-performance, cross-platform photo and video viewing application using **.NET MAUI** and C#. The application must allow users to view local media files (photos and videos) from selected directories with a polished, fast, and simple user experience, requiring no internet connection or authentication.

## 🏗️ Architectural Pattern
The system strictly adheres to the **Model-View-ViewModel (MVVM)** pattern to ensure maximum separation of concerns, testability, and maintainability.

### Core Components:
*   **Models:** Data structures representing media items.
*   **Views:** The UI components (Gallery View, Media Viewer).
*   **ViewModels:** Handle presentation logic, state management, and coordinate between Views and Services.
*   **Services/Core Logic:** Business logic, I/O operations, and platform-specific implementations.

## 📂 Data Model (`MediaItem`)
The core data structure is defined to hold all necessary metadata for a media file:
*   `FilePath`: Absolute path to the media file.
*   `FileName`: Display name of the file.
*   `MediaType`: Enum (Image, Video) determining how it should be displayed/played.
*   `ThumbnailSource`: Source for cached thumbnail images.

## ⚙️ Service Layer Abstraction
To achieve cross-platform consistency and testability, all complex operations are abstracted into services:

1.  **`IMediaService`:** Handles the high-level business logic (e.g., scanning, filtering).
2.  **`IFileSelectionService`:** Manages user interaction for selecting local directories.
3.  **`IVideoService`:** The critical abstraction layer for media playback. It defines methods like `PlayVideoAsync(string filePath)` and `StopVideoAsync()`.

### Platform Abstraction Layer (`IPlatformMediaPlayer`)
This interface abstracts the native player controls, allowing the core logic to remain platform-agnostic:
*   **Android Handler:** Implements video lifecycle management using MAUI/Android services.
*   **iOS Handler:** Utilizes `AVPlayer` or similar frameworks for robust playback.
*   **Windows Handler:** Leverages WinUI's `MediaElement` control.

## 🚀 Feature Implementation Details

### 1. Media Scanning & Indexing (Service Layer)
*   A **robust media scanning service** recursively reads selected folders, identifying files based on MIME types and extensions (JPEG, PNG, MP4, etc.).
*   This process is asynchronous to prevent UI freezing.

### 2. Performance Optimization (Image Loading)
*   An **optimized image loading mechanism** (e.g., using MAUI's built-in caching or a dedicated library like FFImageLoading) is integrated into the Gallery View to ensure fast thumbnail generation and display, minimizing I/O bottlenecks.

### 3. Media Viewer Component
*   The `MediaViewer` component handles both image display (using optimized loading) and video playback.
*   **Video State Management:** The ViewModel manages the video state (Playing, Paused, Error) by interacting with `IVideoService`.

## ✅ Completion Status Summary
All major components have been designed, implemented, and tested:
*   Folder Selection $\rightarrow$ Completed
*   Media Scanning $\rightarrow$ Completed
*   Data Modeling $\rightarrow$ Completed
*   Gallery View UI $\rightarrow$ Completed
*   View Mode Toggling $\rightarrow$ Completed
*   Media Viewer Component $\rightarrow$ Completed
*   Optimized Image Loading $\rightarrow$ Completed
*   Cross-Platform Media Player Integration $\rightarrow$ Completed
*   Video Playback Abstraction (IVideoService) $\rightarrow$ Completed
*   Platform Handlers (Android, iOS, Windows) $\rightarrow$ Completed
*   ViewModel State Management $\rightarrow$ Completed
*   UI/UX Polish & Feedback $\rightarrow$ Completed
*   Unit Testing $\rightarrow$ Completed

The application is architecturally complete and ready for deployment.