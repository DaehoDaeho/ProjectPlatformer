using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StageClearUI : MonoBehaviour
{
    [SerializeField]
    private GameObject clearPanel;

    private void Start()
    {
        if(clearPanel != null)
        {
            clearPanel.SetActive(false);
        }
    }

    public void ShowGameClearUI()
    {
        if (clearPanel != null)
        {
            clearPanel.SetActive(true);
        }
    }

    public void OnClickRestartButton()
    {
        //SceneManager.LoadScene("GameScene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
