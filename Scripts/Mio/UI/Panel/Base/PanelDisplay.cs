using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SCS.Mio
{
    public class PanelDisplay : MonoBehaviour
    {
        [SerializeField] private Button _showButton;
        [SerializeField] private Button _hideButton;

        [SerializeField] private PanelContent _content;
        [SerializeField] private PanelOverlay _overlay;
        [SerializeField] private PanelBehaviour _panelBehaviour;


        public UnityEvent OnShow;
        public UnityEvent OnHide;

        private void Awake()
        {
            OnShow = new UnityEvent();
            OnHide = new UnityEvent();
            InitButtons();
            InitBehaviour();

        }

        private void InitBehaviour()
        {
            // create default if null
            _panelBehaviour ??= gameObject.AddComponent(typeof(PanelBehaviour)) as PanelBehaviour;
            _panelBehaviour?.InitializeBehaviour(_content, _overlay);
            OnShow.AddListener(_panelBehaviour.ShowBehaviour);
            OnHide.AddListener(_panelBehaviour.HideBehaviour);
        }

        public PanelContent GetContents()
        {
            return _content;
        }
        private void InitButtons()
        {
            _showButton?.onClick.AddListener(Show);
            _hideButton?.onClick.AddListener(Hide);
        }

        public virtual void Show()
        {            
            OnShow?.Invoke();
        }

        public virtual void Hide()
        {
            OnHide?.Invoke();
        }

        private void OnDestroy()
        {
            OnShow.RemoveAllListeners();
            OnHide.RemoveAllListeners();
        }
    }
}

