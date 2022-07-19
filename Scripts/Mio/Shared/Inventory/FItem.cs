using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio.Data
{
    public enum ItemCategory
    {
        Empty,
        Food,
        Toy,
        Book,
        Misc
    }

    public interface IInventoryIndexable
    {
        public int Index { get; set; }
    }

    [System.Serializable]
    public struct FItem : IInventoryIndexable
    {
        private int _inventoryIndex;
        public string ID;
        public string Name;
        public string Description;
        public ItemCategory Category;

        public int Index { get { return _inventoryIndex; } set { _inventoryIndex = value; } }
    }


    [System.Serializable]
    public class Item
    {
        public string ID;
        public string Name;
        public string Description;
        public string Category;
    }


    [System.Serializable]
    public class ItemDataList : SCS.IDataList
    {
        public Item[] Items;
    }

}