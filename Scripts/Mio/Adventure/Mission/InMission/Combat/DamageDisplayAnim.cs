using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SCS.Mio
{

    public class DamageDisplayAnim : MonoBehaviour
    {
        #region private variables
        [SerializeField] private Vector3 _endPosition;
        [SerializeField] private float _duration;
        #endregion private variables
        
        #region  public variables
        #endregion public variables
        
        #region private functions
        private void Start()
        {
            transform.DOBlendableMoveBy(_endPosition, _duration);
            
        }
        #endregion private functions
        
        #region public functions
        #endregion public functions
    }
}