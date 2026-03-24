using UnityEngine;

public class FinishLineTrigger : MonoBehaviour
{
    [Header("Level Complete UI")]
    public LevelCompleteUI levelCompleteUI;

    private bool isFinished = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isFinished) return;

        if (other.CompareTag("Player"))
        {
            if (levelCompleteUI == null)
            {
                Debug.LogError("FinishLineTrigger -> levelCompleteUI atanmadý!");
                return;
            }

            isFinished = true;
            Debug.Log("Level tamamlandý!");
            levelCompleteUI.ShowLevelComplete();
        }
    }
}