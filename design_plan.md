# Project Architecture Design Plan: Visiting Card Scanner

## 1. Overview
The application is designed as a cross-platform media viewer and scanner for visiting cards. It follows the Model-View-ViewModel (MVVM) pattern to ensure separation of concerns, making it maintainable and testable across different platforms (Android, iOS, Windows).

## 2. Architecture Diagram
The system workflow can be visualized using the following Mermaid diagram:

```mermaid
graph TD
    A[Directory Paths] --> B(MediaScannerService);
    B --> C{Scan & Discover Media};
    C --> D[List<MediaItem>];
    D --> E(GalleryView / GalleryViewModel);
    E --> F[User Selects Item];
    F --> G{Determine MediaType (Image/Video)};
    G -- Image --> H[IImageLoadingService];
    G -- Video --> I[VideoService];
    H --> J(MediaViewerView);
    I --> K[IPlatformMediaPlayer];
    K --> L(Native OS Player);

    subgraph Core Services
        B
        E
        I
    end
```

## 3. Component Breakdown and Responsibilities

### A. Models
*   **`MediaItem`**: The core data model representing any discovered media file, containing `FilePath`, `FileName`, and `MediaType` (Image/Video).

### B. Services Layer (Business Logic)
*   **`IMediaService` / `MediaScannerService`**: Responsible for the discovery phase. It recursively scans provided directories to find all files matching known image and video extensions, generating a list of `MediaItem`s.
*   **`IImageLoadingService`**: Handles loading and caching of static images from file paths.
*   **`IVideoService` / `VideoService`**: Manages the playback lifecycle for videos. It acts as an abstraction layer over platform-specific players, providing methods like `PlayVideoAsync`, `StopVideoAsync`.
*   **`IPlatformMediaPlayer` (and concrete handlers)**: This is the crucial cross-platform abstraction. It defines the contract (`InitializeAsync`, `Play`, `Stop`) that allows the `VideoService` to interact with native media players without knowing the underlying OS implementation details.

### C. ViewModels Layer (Presentation Logic)
*   **`MediaViewerViewModel`**: The central coordinator for viewing a selected item. It observes changes in the selected `MediaItem` and determines whether to load an image or prepare the video player, coordinating calls between `IImageLoadingService` and `IVideoService`.

### D. Views Layer (UI/UX)
*   **`GalleryView`**: Displays the list of discovered media items (`MediaItem`) in a grid format.
*   **`MediaViewerView`**: The dedicated viewing screen. It uses XAML to present the selected item, dynamically showing either an `ImagePresenter` or a `VideoPresenter` (using `CustomVideoPlayerControl`).

## 4. Data Flow Summary
1.  **Scan:** User provides root directories $\rightarrow$ `MediaScannerService` scans files $\rightarrow$ Populates list of `MediaItem`.
2.  **Display List:** `GalleryView` binds to the list and displays thumbnails/names.
3.  **Select Item:** User taps an item in `GalleryView` $\rightarrow$ `MediaViewerViewModel` updates its selected item property.
4.  **View Media:** The view observes the change:
    *   If Image: `MediaViewerViewModel` calls `IImageLoadingService` to load and display the image in `MediaViewerView`.
    *   If Video: `MediaViewerViewModel` calls `VideoService.PlayVideoAsync(filePath)` $\rightarrow$ `VideoService` uses the appropriate platform handler (`AndroidMediaPlayerHandler`, etc.) to initialize and play the video stream on the native player control.