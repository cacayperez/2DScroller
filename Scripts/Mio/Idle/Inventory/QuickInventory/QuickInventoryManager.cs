using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SCS.Mio.Inventory;
using SCS.Mio.Data;

namespace SCS.Mio.Idle
{
    public interface IQuickInventoryManager
    {
        public IInventoryManager InventoryHandler { get; }
    }

    public class QuickInventoryManager : MonoBehaviour, IQuickInventoryManager
    {
        private IInventoryManager _inventoryHandler;
        public IInventoryManager InventoryHandler { get { return _inventoryHandler; } }

        public void Awake()
        {
            _inventoryHandler = gameObject.GetComponent<InventoryManager>();

            for (int i = 0; i < 3; i++)
            {
                FItem item = DatabaseLoader.Instance.Items[i];
                _inventoryHandler.AddItem(item);
            }
            
        }

    }
}