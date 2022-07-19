using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace SCS.Mio
{
    public class PanelOverlay : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _opacity;

        private void Awake()
        {
        }

        public void Show()
        {
            enabled = true;
            _canvasGroup.alpha = _opacity;
            _canvasGroup.blocksRaycasts = true;
        }

        public void Hide()
        {
            enabled = false;
            _canvasGroup.alpha = 0f;
            _canvasGroup.blocksRaycasts = false;
        }
    }

}
