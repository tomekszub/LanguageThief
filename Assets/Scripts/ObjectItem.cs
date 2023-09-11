using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectItem : MonoBehaviour
{
    [SerializeField] Item _Item;

    public string Name => _Item.Name;
}
