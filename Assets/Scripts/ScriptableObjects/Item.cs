using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Create Item", order = 0)]
public class Item : SerializedScriptableObject
{
    [SerializeField] string _Name;

    public string Name => _Name;
}
