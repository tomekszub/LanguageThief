using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SerializedMonoBehaviour
{
    [SerializeField] List<string> _TargetItemNames;
    [SerializeField] ItemPicker _ItemPicker;

    List<string> _pickedUpItemNames;

    void Awake()
    {
        _ItemPicker.OnItemPickedUp -= ItemPickedUp;
        _ItemPicker.OnItemPickedUp += ItemPickedUp;

        _pickedUpItemNames = new();
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.P))
            CheckResults();
    }

    void ItemPickedUp(string itemName)
    {
        _pickedUpItemNames.Add(itemName);
    }

    public void CheckResults()
    {
        int targetScore = _TargetItemNames.Count;

        int totalScore = 0;

        foreach(var targetName in _TargetItemNames)
        {
            if (_pickedUpItemNames.Contains(targetName))
            {
                _pickedUpItemNames.Remove(targetName);
                totalScore++;
                Debug.LogError($"Item {targetName} found!");
            }
            else
                Debug.LogError($"Item {targetName} is missing!");
        }

        Debug.LogError($"Your score: {totalScore}/{targetScore}");
    }
}
