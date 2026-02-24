using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private float gameOverMenuDelay = 3f;
    [SerializeField] private float gameWinMenuDelay = 3f;
    
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject gameWin;

    public bool paused;
    
    private PlayerSightHandler sightController;

    private void Start()
    {
        sightController = GameObject.FindWithTag("Player").GetComponent<PlayerSightHandler>();
    }

    public void gameMenuCall()
    {
        if (Time.timeScale != 0f || paused)
        {
            if (!paused)
            {
                gameMenu.GetComponent<MenuStats>().updateChars();
                OpenMenu();
                Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
                paused = true;
            }
            else
            {
                CloseMenu();
                paused = false;
            }
        }
    }

    public void gameOverShow()
    {
        Time.timeScale = 1f;
        StopAllCoroutines();
        gameOver.transform.position = new Vector3(gameOver.transform.position.x, gameOver.transform.position.y+Screen.height * 2, 0);
        gameOver.SetActive(true);
        gameOver.LeanMove(transform.position, 1f).delay = gameOverMenuDelay;
    }

    public void OpenMenu()
    {
        gameMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseMenu()
    {
        sightController.resetCursor();
        gameMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    
    public void restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
    
    public void backToMainMenu()
    {
        Time.timeScale = 1f;
        PlayerPrefs.DeleteKey("SelectedCharacter");
        PlayerPrefs.DeleteKey("SelectedStage");
        PlayerPrefs.DeleteAll();
        sightController.reset();
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
    
    public void exit()
    {
        Time.timeScale = 1f;
        sightController.reset();
        PlayerPrefs.DeleteKey("SelectedCharacter");
        PlayerPrefs.DeleteKey("SelectedStage");
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }

    public void GameWin()
    {
        paused = true;
        gameWin.transform.position = new Vector3(gameWin.transform.position.x, gameWin.transform.position.y+Screen.height * 2, 0);
        gameWin.SetActive(true);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.ForceSoftware);
        gameWin.LeanMove(transform.position, 1f).delay = gameWinMenuDelay;
    }
}
