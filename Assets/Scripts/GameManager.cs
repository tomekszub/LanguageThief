using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SerializedMonoBehaviour
{
    [SerializeField] ItemPicker _ItemPicker;
    [SerializeField] ItemDefinitions _ItemDefinitions;

    List<string> _targetItemNames;
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

        _targetItemNames = new();

        foreach (var item in items)
            _targetItemNames.Add(item.Name);
    }

    void ItemPickedUp(string itemName) => _pickedUpItemNames.Add(itemName);

    public void CheckResults()
    {
        int targetScore = _targetItemNames.Count;
        int totalScore = 0;
        List<string> copiedItemNames = new(_pickedUpItemNames);

        foreach(var targetName in _targetItemNames)
        {
            if (copiedItemNames.Contains(targetName))
            {
                copiedItemNames.Remove(targetName);
                totalScore++;
                Debug.LogError($"Item {targetName} found!");
            }
            else
                Debug.LogError($"Item {targetName} is missing!");
        }

        Debug.LogError($"Your score: {totalScore}/{targetScore}");
    }
}
