using UnityEngine;

public class ObjectItem : MonoBehaviour
{
    [SerializeField] Item _Item;

    public string Name => _Item.Name;
}
