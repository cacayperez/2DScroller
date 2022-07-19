using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio.UI
{
    public class UI_321Fight_Announcement : UI_Announcement
    {
        private void Awake()
        {
            Hide();
           
        }
        public override void Hide()
        {
            gameObject.SetActive(false);
        }

        public override void Show()
        {
            gameObject.SetActive(true);
            
        }
    }
}
