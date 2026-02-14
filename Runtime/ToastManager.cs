using System;
using Tesseract.Core;
using UnityEngine;

namespace Tesseract.Toast
{
    /// <summary>
    /// Singleton toast notification manager.
    /// </summary>
    public class ToastManager : Singleton<ToastManager>
    {
        [SerializeField] private ToastView _toastView;

        /// <summary>
        /// Fired when a toast is shown. Can be used to play sound effects.
        /// </summary>
        public event Action<string> OnToastShown;

        /// <summary>
        /// Show a toast notification message.
        /// </summary>
        public void Show(string message)
        {
            if (_toastView == null)
            {
                Debug.LogWarning("[ToastManager] ToastView is not assigned.");
                return;
            }

            _toastView.Show(message);
            OnToastShown?.Invoke(message);
        }

        /// <summary>
        /// Hide the current toast immediately.
        /// </summary>
        public void Hide()
        {
            if (_toastView != null)
                _toastView.Hide();
        }

        /// <summary>
        /// Check if a toast is currently showing.
        /// </summary>
        public bool IsShowing => _toastView != null && _toastView.IsShowing;
    }
}
