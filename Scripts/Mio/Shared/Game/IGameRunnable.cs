using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio
{
    public interface IGameRunnable
    {
        public void OnPause();
        public void OnResume();
        public void OnExit();
    }
}