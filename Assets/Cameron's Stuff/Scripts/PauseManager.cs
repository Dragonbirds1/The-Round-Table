using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public KeyCode pauseKey, resumeKey, quitKey;
    public bool paused;
    public GameObject pauseMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            paused = true;
        }

        if (paused)
        {
            pauseMenu.SetActive(true);
        }

        if (paused == true && Input.GetKeyDown(resumeKey))
        {
            pauseMenu.SetActive(false);
            paused = false;
        }

        if (paused == true && Input.GetKeyDown(quitKey))
        {
            SceneManager.LoadScene("Warren");
        }
    }
}
