using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio.Mission.Morale
{
    public class NoteRing : MonoBehaviour, INoteVisual
    {
        #region private variables
        private CanvasGroup _canvasGroup;
        private SpriteRenderer _spriteRenderer;
        private float _targetScale;
        private float _modifier = 0.0f;

        public float Modifier { get { return _modifier; } set => SetModifier(value); }
        #endregion private variables

        #region  public variables
        #endregion public variables

        #region private functions
        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            SetModifier(0);
        }

        private void SetModifier(float value)
        {
            _modifier = value;
            
            if(_spriteRenderer)
            {
                var col = _spriteRenderer.color;
                col.a = _modifier;

                _spriteRenderer.color = col;
            }
        }
        


        private void Update()
        {

        }
        #endregion private functions
        
        #region public functions


        public void Begin()
        {
            SetModifier(0);
            gameObject.SetActive(true);
        }

        public void End()
        {
            gameObject.SetActive(false);
            SetModifier(0);
        }
        #endregion public functions
    }
}