using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject creditsPanel;

    public void OpenPanel(GameObject panel)
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(false);

        panel.SetActive(true);
    }

    public void PlayLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}