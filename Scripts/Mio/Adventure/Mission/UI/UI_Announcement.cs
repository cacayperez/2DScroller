using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio.UI
{
    public interface IUI_Announcement
    {
        public void Show();
        public void Hide();
    }

    public abstract class UI_Announcement : MonoBehaviour, IUI_Announcement
    {
        public abstract void Hide();
        public abstract void Show();
    }

}