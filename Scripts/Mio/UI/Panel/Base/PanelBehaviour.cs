using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio
{
    public class PanelBehaviour : MonoBehaviour
    {
        private PanelContent _content;
        private PanelOverlay _overlay;

        public virtual void InitializeBehaviour(PanelContent Content, PanelOverlay Overlay = null)
        {
            _content = Content;
            _overlay = Overlay;
            HideAll();

        }
        public virtual void ShowBehaviour()
        {
            ShowAll();
        }

        public virtual void HideBehaviour()
        {
            HideAll();
        }

        protected void ShowAll()
        {

            _content?.Show();
            _overlay?.Show();
        }

        protected void HideAll()
        {
            _content?.Hide();
            _overlay?.Hide();
        }
    }

}