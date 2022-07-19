using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SCS.Mio.Data;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace SCS.Mio
{
    [CreateAssetMenu(fileName = "Item", menuName = "Mio/Item/ItemData")]
    public class InventoryItemObject : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _iD;
        [SerializeField] private string _description;
        [SerializeField] private string _name;
        [SerializeField] private ItemCategory _category;
    }

}