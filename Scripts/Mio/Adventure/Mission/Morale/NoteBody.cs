using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;
using DG.Tweening;

namespace SCS.Mio.Mission.Morale
{
    public class NoteBody : MonoBehaviour, INoteVisual
    {
        [SerializeField] private NoteBase _noteBase;
        [SerializeField] private Sprite _intro;
        [SerializeField] private Sprite _wait;
        [SerializeField] private Sprite _success;
        [SerializeField] private Sprite _miss;
        private Sequence _sequence;

        private SpriteRenderer _spriteRenderer;
        private Color _defaultColor;
        private float _modifier;
        public float Modifier { get { return _modifier; } set => SetModifier(value); }


        private void AnimateIn()
        {
            
        }

        private void AnimateOut()
        {

        }

        private void SetModifier(float value)
        {
            _modifier = value;
            if (_spriteRenderer)
            {
                var col = _spriteRenderer.color;
                var alpha =  _modifier * 2.2f ;

                col.a = alpha;
                _spriteRenderer.color = col;
            }
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _defaultColor = _spriteRenderer.material.color;
            SetModifier(0);



    
        }

        private void Hide()
        {
            Debug.Log("Hide");
            gameObject.SetActive(false);
            transform.localScale = Vector3.one;
            _spriteRenderer.material.color = _defaultColor;
        }
        public void Begin()
        {
            _spriteRenderer.sprite = _intro;
            AnimateIn();
            SetModifier(0);
            gameObject.SetActive(true);
            
        }

        public void End()
        {
            if (_spriteRenderer == null) return;
            _spriteRenderer.sprite = _success;
            gameObject.SetActive(false);
            //SetModifier(0);
        }        
        
        public void Select()
        {
            if (_noteBase != null) _noteBase.TakeHit();
            _sequence = DOTween.Sequence();

            Color color = _spriteRenderer.color;
            _sequence.Append(transform.DOScale(1.8f, 0.3f))
            .Insert(0,
               _spriteRenderer.material.DOFade(0, 0.2f)
             )
            .OnComplete(Hide);
        }


    }
}