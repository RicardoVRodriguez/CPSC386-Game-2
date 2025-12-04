using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
//Handles how the scene transitions by loading a new scene or quitting when a button is clicked.
//It also handles the playerprefs for their high score and the UI elements needed to display the score.
public class Scene_Manager : MonoBehaviour
{

    private const string Level1ScoreKey = "PlayerLv1Score";
    private const string Level2ScoreKey = "PlayerLv2Score";
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float loadedLv1Score;
        float loadedLv2Score;
        loadedLv1Score = PlayerPrefs.GetFloat(Level1ScoreKey, 0);
        loadedLv2Score = PlayerPrefs.GetFloat(Level2ScoreKey, 0);
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "LevelSelect")
        {
            TMP_Text bestLv1TimeText = GameObject.Find("BestLv1TimeText").GetComponent<TMP_Text>();
            int minutes = Mathf.FloorToInt(loadedLv1Score / 60);
            int seconds = Mathf.FloorToInt(loadedLv1Score % 60);
            bestLv1TimeText.text = string.Format("Best Time: {0:00}:{1:00}", minutes, seconds);

            TMP_Text bestLv2TimeText = GameObject.Find("BestLv2TimeText").GetComponent<TMP_Text>();
            minutes = Mathf.FloorToInt(loadedLv2Score / 60);
            seconds = Mathf.FloorToInt(loadedLv2Score % 60);
            bestLv2TimeText.text = string.Format("Best Time: {0:00}:{1:00}", minutes, seconds);
        }
    }

    public void LoadLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadSettings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void LoadHowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Game-Lv1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Game-Lv2");
    }

}
