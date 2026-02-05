using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    private bool _isPaused;
    public KeyCode pauseKey;


    [SerializeField] private GameObject _pauseMenu;

    private void Update()
    {
        if(Input.GetKeyDown(pauseKey))
        {
            _isPaused = !_isPaused;
            _pauseMenu.SetActive(_isPaused);
            Time.timeScale = _isPaused ? 0 : 1;
        }
    }
    //public void Paused(InputAction.CallbackContext ctx)
    //{
    //_isPaused = true;
    //_pauseMenu.SetActive(true);

    // Time.timeScale = 0;
    //}

    //public void Resume(InputAction.CallbackContext ctx)
    //{
    //_isPaused = false;
    // _pauseMenu.SetActive(false);
    //   Time.timeScale = 1;
    //}

    // public void Toggle(InputAction.CallbackContext ctx) 
    //{
    //_isPaused = !_isPaused;
    // _pauseMenu.SetActive(_isPaused);

    // Time.timeScale = _isPaused ? 0 : 1;
    //}

    // public void Quit()
    // {

    //}
}