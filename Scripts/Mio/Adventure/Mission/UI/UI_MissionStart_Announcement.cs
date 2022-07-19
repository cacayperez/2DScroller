using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio.UI
{
    public class UI_MissionStart_Announcement : UI_Announcement
    {
        [SerializeField] private SpriteRenderer _sprite1;
        [SerializeField] private SpriteRenderer _sprite2;
        [SerializeField] private SpriteRenderer _overlay;

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