using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;

namespace SCS.Mio
{
    public abstract class PanelContext: MonoBehaviour
    {
        public abstract void LoadContext();
        public abstract void UnloadContext();
    }

}