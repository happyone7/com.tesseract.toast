# Changelog

## [1.1.0] - 2026-02-14
### Fixed
- ToastView.Awake: Added null fallback for _canvasGroup and _rectTransform
- ToastView.Hide: Now deactivates gameObject (matching Show() activation)
- ToastView.Show: SetActive(true) moved before StopCoroutine to avoid inactive GO issue
- ToastManager.OnDestroy: Clears OnToastShown event subscribers to prevent leaks

## [1.0.0] - 2026-02-14
### Added
- ToastView: Toast notification with auto-hide, AnimationCurve fade, configurable delay/duration
- ToastManager: Singleton manager with Show/Hide API and OnToastShown event
- Coroutine-based fade (no external dependency)
- Uses Time.unscaledDeltaTime for pause-safe fade
