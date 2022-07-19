using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SCS.Mio
{
    public class MissionSelectItem : MonoBehaviour
    {
        #region private variables
        [SerializeField] private MissionData _missionData;
        [SerializeField] private Color _selectedTint = new Color(1,1,1,1);
        [SerializeField] private Color _unSelectedTint = new Color(0.5f,0.5f,0.5f,1);
        [SerializeField] private Image _image;
        #endregion private variables
        
        #region  public variables
        public MissionData Data { get { return _missionData; } }
        #endregion public variables
        
        #region private functions
        private void Awake()
        {
            _image ??= GetComponent<Image>();
            Deselect();
        }
        #endregion private functions

        #region public functions
        public void Select()
        {
            if (_image != null)_image.color = _selectedTint;
            if (GameController.Instance)
            {
                GameController.Instance.ActiveMission = _missionData;
            }
        }

        public void Deselect()
        {
            if (_image!=null) _image.color = _unSelectedTint;
        }

        #endregion public functions
    }
}