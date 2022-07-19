using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio.UI
{
    public interface IUI_QuickInventoryTabButton
    {

    }

    public class UI_QuickInventoryTabButton : MonoBehaviour, IUI_QuickInventoryTabButton
    {
        [SerializeField] private Data.ItemCategory _category;

        public void OnSelectTab()
        {

        }
    }
}
