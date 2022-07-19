using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio
{
    public class PanelContent : MonoBehaviour
    {
        [SerializeField] private PanelContext _context;
        [SerializeField] private CanvasGroup _canvasGroup;

        public PanelContext Context { get { return _context; } }
        private void OnEnable()
        {
            if(_context != null)
            {
                _context.LoadContext();
            }
        }

        private void OnDisable()
        {
            if (_context != null)
            {
                _context.UnloadContext();
            }
        }

        public void Show()
        {
            enabled = true;
            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            enabled = false;
        }

    }
}

