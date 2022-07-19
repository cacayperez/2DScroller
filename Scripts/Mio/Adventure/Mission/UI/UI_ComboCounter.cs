using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio.UI
{
    public class UI_ComboCounter : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshPro _text;
        [SerializeField] private Scene.ComboCounter _comboCounter;
       

        private void Awake()
        {
            _comboCounter = FindObjectOfType<Scene.ComboCounter>();
            _text.SetText("" + _comboCounter.HitCount);
            if(_comboCounter != null)
                _comboCounter.EventDispatcher.Subscribe(HandleComboUpdate);
        }

        private void HandleComboUpdate(int value)
        {
            _text.SetText("" + _comboCounter.HitCount);
        }
    }

}