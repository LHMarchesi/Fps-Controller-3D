using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject menuPause;
    private bool isPause;

    public bool IsPause { get => isPause; set => isPause = value; }

    public void Awake()
    {
        IsPause = false;
    }

    public void OpenPanel(GameObject panel)
    {
        menuPause.SetActive(false);

        panel.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!IsPause)
            {
                menuPause.SetActive(true);
                IsPause = true;

                Time.timeScale = 0f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {

                Resume();
            }
        }
    }

    public void Resume()
    {
        menuPause.SetActive(false);
        IsPause = false;

        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GoToMenu(string menu)
    {
        SceneManager.LoadScene(menu);
        Time.timeScale = 1f;
        menuPause.SetActive(false);
        IsPause = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Resume();
    }
}