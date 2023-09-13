using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
    List<Item> _targetItemNames;

    public StageResult GetResult(List<string> pickedUpItemNames)
    {
        int targetScore = _targetItemNames.Count;
        int totalScore = 0;
        List<string> copiedItemNames = new(pickedUpItemNames);
        StringBuilder stringBuilder = new StringBuilder();

        foreach (var targetItem in _targetItemNames)
        {
            if (copiedItemNames.Contains(targetItem.Name))
            {
                copiedItemNames.Remove(targetItem.Name);
                totalScore++;
                stringBuilder.AppendLine($"Item {targetItem.Name} found!");
            }
            else
                stringBuilder.AppendLine($"Item {targetItem.Name} is missing!");
        }

        return new StageResult(totalScore, targetScore, stringBuilder.ToString());
    }

    public void Init(IEnumerable<Item> objectiveItems)
    {
        _targetItemNames = new();

        foreach (var item in objectiveItems)
            _targetItemNames.Add(item);
    }
}

public class StageResult
{
    public int Score;
    public int TargetScore;
    public string Details;

    public StageResult(int score, int targetScore, string details)
    {
        Score = score;
        TargetScore = targetScore;
        Details = details;
    }
}
