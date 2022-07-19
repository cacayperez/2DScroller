using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SCS.Mio.UIMorale
{
    public class UI_MoraleMeter : UIBase
    {
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            Scene.MoraleEventDispatcher dispatcher = FindObjectOfType<Scene.MoraleEventDispatcher>();
            if(dispatcher != null)
            {
                dispatcher.Subscribe(Scene.MoraleEventState.MoraleChanged, HandleOnMoraleChanged);
            }
            else
            {
                Debug.Log("dispatcher is null");
            }
        }

        private void HandleOnMoraleChanged(int val)
        {
            float percent = val / 100.0f;

            _slider.value = percent;
        }
    }
}
