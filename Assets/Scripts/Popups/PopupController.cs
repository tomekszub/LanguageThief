using UnityEngine;

public class PopupController : MonoBehaviour
{
    [SerializeField] StageFinishedPopup _StageFinishedPopup;

    public void ShowStageFinishPopup(StageResult stageResult)
    {
        _StageFinishedPopup.Show(stageResult);
    }
}
