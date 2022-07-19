using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SCS.Mio.Data;

namespace SCS.Mio.Idle
{
    public interface IQuickInventoryItem
    {
        public FItem ItemData { get; set; }
        
    }

    public class QuickInventoryItem : MonoBehaviour, IQuickInventoryItem
    {
        private FItem _itemData;

        public FItem ItemData { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        private void SetItemData(FItem item)
        {
            // do initialization
        }
    }

}
