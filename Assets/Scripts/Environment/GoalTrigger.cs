using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    [SerializeField]
    private StageClearUI clearUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") == true)
        {
            if(clearUI != null)
            {
                clearUI.ShowGameClearUI();
            }
        }
    }
}
