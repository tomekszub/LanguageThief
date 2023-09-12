using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//[CreateAssetMenu(fileName = "ItemDefinitions", menuName = "Create ItemDefinitions", order = 0)]
public class ItemDefinitions : SerializedScriptableObject
{
    [SerializeField] Dictionary<string, Item> _Items;

    public IEnumerable<Item> GetRandomItems(int amount)
    {
        var ret = new List<Item>();

        for (int i = 0; i < amount; i++) 
        {
            ret.Add(_Items.Values.ElementAt(Random.Range(0, _Items.Count)));
        }

        return ret;
    }
}
