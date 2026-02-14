using System.Collections;
using TMPro;
using UnityEngine;

namespace Tesseract.Toast
{
    /// <summary>
    /// Visual toast message component with auto-fade using AnimationCurve.
    /// </summary>
    public class ToastView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Vector2 _padding = new Vector2(10f, 5f);
        [SerializeField] private float _fadeDelay = 1.5f;
        [SerializeField] private float _fadeDuration = 0.5f;
        [SerializeField] private AnimationCurve _fadeCurve = AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);

        public bool IsShowing { get; private set; }

        private Coroutine _fadeCoroutine;

        private void Awake()
        {
            if (_canvasGroup == null)
                _canvasGroup = GetComponent<CanvasGroup>();
            if (_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();

            Hide();
        }

        /// <summary>
        /// Show toast with text. Auto-hides after delay + fade.
        /// </summary>
        public void Show(string message)
        {
            gameObject.SetActive(true);

            if (_fadeCoroutine != null)
                StopCoroutine(_fadeCoroutine);

            _text.text = message;
            _text.ForceMeshUpdate();
            UpdateSize();

            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = false;
            IsShowing = true;

            _fadeCoroutine = StartCoroutine(FadeAndHide());
        }

        /// <summary>
        /// Immediately hide the toast.
        /// </summary>
        public void Hide()
        {
            if (_fadeCoroutine != null)
            {
                StopCoroutine(_fadeCoroutine);
                _fadeCoroutine = null;
            }

            if (_canvasGroup != null)
                _canvasGroup.alpha = 0f;
            IsShowing = false;
            gameObject.SetActive(false);
        }

        private void UpdateSize()
        {
            Vector2 textSize = _text.GetPreferredValues(_text.text);
            _rectTransform.sizeDelta = textSize + _padding;
        }

        private IEnumerator FadeAndHide()
        {
            // Wait before fading
            yield return new WaitForSecondsRealtime(_fadeDelay);

            // Fade out using AnimationCurve
            float elapsed = 0f;
            while (elapsed < _fadeDuration)
            {
                elapsed += Time.unscaledDeltaTime;
                float t = Mathf.Clamp01(elapsed / _fadeDuration);
                _canvasGroup.alpha = _fadeCurve.Evaluate(t);
                yield return null;
            }

            _canvasGroup.alpha = 0f;
            IsShowing = false;
            _fadeCoroutine = null;
            gameObject.SetActive(false);
        }
    }
}
