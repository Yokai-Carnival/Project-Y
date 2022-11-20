using ScriptableObjectEvents;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseScreen;
    [SerializeField] private BoolEvent _gamePaused;

    private void Start()
    {
        HideCursor();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        if(Input.GetKeyDown(KeyCode.R)) 
        {
            Restart();
        }
    }

    private void Restart()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    private void PauseGame()
    {
        if(Time.timeScale == 0)
        {
            _pauseScreen.SetActive(false);
            Time.timeScale = 1;
            HideCursor();
            //using oposite here because I want to activate or enable something when game is unpaused 
            _gamePaused.Raise(!false);
        }
        else
        {
            _pauseScreen.SetActive(true);
            Time.timeScale = 0;
            ShowCursor();
            //using oposite here because I want to deactivate or disable something when game is paused 
            _gamePaused.Raise(!true);
        }
    }

    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
