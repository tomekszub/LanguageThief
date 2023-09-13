using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageFinishedPopup : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _ObjectivesText;
    [SerializeField] TextMeshProUGUI _TitleText;
    [SerializeField] List<GameObject> _Stars; 

    public void Show(StageResult stageResult)
    {
        _ObjectivesText.text = stageResult.Details;

        _TitleText.text = stageResult.Score == stageResult.TargetScore ? "Perfect!" : "Nice try!";

        float successPercentage = stageResult.Score / (float) stageResult.TargetScore;

        _Stars[0].SetActive(successPercentage > 0);
        _Stars[1].SetActive(successPercentage > .33f);
        _Stars[2].SetActive(successPercentage > .66f);

        gameObject.SetActive(true);
    }

    public void Hide() => gameObject.SetActive(false);
}
