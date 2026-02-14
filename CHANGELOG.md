# Changelog

## [1.0.0] - 2026-02-14
### Added
- ToastView: Toast notification with auto-hide, AnimationCurve fade, configurable delay/duration
- ToastManager: Singleton manager with Show/Hide API and OnToastShown event
- Coroutine-based fade (no external dependency)
- Uses Time.unscaledDeltaTime for pause-safe fade
