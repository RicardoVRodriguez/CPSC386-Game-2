using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Scene_Manager : MonoBehaviour
{

    private const string ScoreKey = "PlayerScore";
    float loadedScore;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        loadedScore = PlayerPrefs.GetFloat(ScoreKey, 0);
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "MainMenu")
        {
            TMP_Text bestTimeText = GameObject.Find("BestTimeText").GetComponent<TMP_Text>();
            int minutes = Mathf.FloorToInt(loadedScore / 60);
            int seconds = Mathf.FloorToInt(loadedScore % 60);
            bestTimeText.text = string.Format("Best Time: {0:00}:{1:00}", minutes, seconds);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
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


}
