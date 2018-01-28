using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour 
{
    [SerializeField]
    private string levelName;    
    [SerializeField]
    private RectTransform mainPanel;
    [SerializeField]
    private RectTransform creditsPanel;

    public void OnStartLevel()
    {
        SceneManager.LoadScene(levelName);
    }

    public void ShowCredits()
    {
        creditsPanel.gameObject.SetActive(true);
        mainPanel.gameObject.SetActive(false);
    }

    public void BackToMainMenu()
    {
        creditsPanel.gameObject.SetActive(false);
        mainPanel.gameObject.SetActive(true);
    }
}
