using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SCS.Mio
{
    public class MissionSelect : MonoBehaviour
    {
        #region private variables
        [SerializeField] private MagneticScrollView.MagneticScrollRect _scrollable;
        [SerializeField] private Vector3 _minimizedScale = new Vector3(0.6f, 0.6f, 0.6f);
        private Transform _selected;
        private Transform _previous;
        #endregion private variables
        
        #region  public variables
        #endregion public variables
        
        #region private functions
        private void Awake()
        {
            _scrollable?.onSelectionChange.AddListener(OnSelect);
            
        }

        private void Start()
        {
            SetDefaultScale();
            SetDefaultSelection();
        }

        private void SetDefaultSelection()
        {
            _selected = _scrollable.CurrentSelectedObject.transform;
            _selected.localScale = Vector3.one;
            var item = _scrollable.CurrentSelectedObject.GetComponent<MissionSelectItem>();
            item?.Select();
        }

        private void SetDefaultScale()
        {
            for (int i = 0; i < _scrollable.Elements.Length; i++)
            {
                if(i != _scrollable.CurrentSelectedIndex)
                {
                    _scrollable.Elements[i].localScale = _minimizedScale;
                    var item = _scrollable.Elements[i].GetComponent<MissionSelectItem>();
                    item?.Deselect();
                }
            }
        }

        #endregion private functions

        #region public functions
        public void OnSelect(GameObject obj)
        {
            _previous = _selected;
            _selected = obj.transform;

            if(_previous != _selected)
            {
                if (DOTween.IsTweening(_previous))
                    _previous.DOKill();
                _previous.localScale = _minimizedScale;

                _selected.DOScale(1, 0.2f);
                var item = _selected.GetComponent<MissionSelectItem>();
                item?.Select();
                SetDefaultScale();
            }
        }
        #endregion public functions
    }
}