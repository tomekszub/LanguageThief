using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SerializedMonoBehaviour
{
    [SerializeField] ItemPicker _ItemPicker;
    [SerializeField] ItemDefinitions _ItemDefinitions;
    [SerializeField] ObjectiveController _ObjectiveController;
    [SerializeField] PopupController _PopupController;

    List<string> _pickedUpItemNames;

    void Awake()
    {
        _ItemPicker.OnItemPickedUp -= ItemPickedUp;
        _ItemPicker.OnItemPickedUp += ItemPickedUp;

        _pickedUpItemNames = new();
    }


    void Start()
    {
        var items = _ItemDefinitions.GetRandomItems(4);

        _ObjectiveController.Init(items);
    }

    public void FinishStage()
    {
        var result = _ObjectiveController.GetResult(_pickedUpItemNames);
        _PopupController.ShowStageFinishPopup(result);
    }

    void ItemPickedUp(string itemName) => _pickedUpItemNames.Add(itemName);
}
