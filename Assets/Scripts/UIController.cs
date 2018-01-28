using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour 
{
    [SerializeField]
    private string levelName;
    
    public void OnStartLevel()
    {
        SceneManager.LoadScene(levelName);
    }
}
