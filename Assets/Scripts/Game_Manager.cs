using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//Manages the timer and tracks the game's current state. Handles any UI elements that need to be displayed when you lose, pause, and earn an upgrade. 

public class Game_Manager : MonoBehaviour
{

    public class ExpUpgrade
    {
        public string m_Title;
        public string m_Description;
        public int m_Level;
        public int m_MaxLevel;

        public ExpUpgrade(string title, string description, int level, int maxLevel )
        {
            m_Title = title;
            m_Description = description;
            m_Level = level;
            m_MaxLevel = maxLevel;
        }

        public void IncLevel()
        {
            m_Level++;
        }

    }

    public enum GameState
    {
        Menu,
        Playing,
        Paused,
        GameOver
    }

    public GameState currentGameState;
    public AudioSource audioSource; 
    public AudioClip gameOverAudio;
    private bool playedAudio = false;
    
    public Player player;
    public GameObject statsUpgradeScreen;
    public GameObject loseScreen;
    public GameObject pauseScreen;
    public bool isUpgrading = false;
    float elapsedTime;
    private const string Level1ScoreKey = "PlayerLv1Score";
    private const string Level2ScoreKey = "PlayerLv2Score";


    public Button leftButton;
    public Button rightButton;
    public Button centerButton;

    public TMP_Text leftTitle;
    public TMP_Text rightTitle;
    public TMP_Text centerTitle;

    public TMP_Text leftDesc;
    public TMP_Text rightDesc;
    public TMP_Text centerDesc;

    public TMP_Text timerText;


    int arrSize;
    int leftRandNumber;
    int rightRandNumber;
    int centerRandNumber;



    public ExpUpgrade[] StatsUpgradeArr = 
    { 
        new ExpUpgrade("Increase Tree Growth", "Trees will grow back quicker.", 1, 2), //0
        new ExpUpgrade("Increase Mushroom Drops", "Monsters are more likely to drop mushrooms.", 1, 5), //1
        new ExpUpgrade("Increase Meat Drops", "Monsters are more likely to drop Meat.", 1, 5), //2
        new ExpUpgrade("Increase Cabbage Drops", "Monsters are more likely to drop cabbage.", 1, 5), //3
        new ExpUpgrade("Increase Max Health", "Your max health will increase.", 1, 5), //4
        new ExpUpgrade("Increase Movement Speed", "You will move faster.", 1, 5), //5
        new ExpUpgrade("Increase Main Attack speed", "You will attack faster.", 1, 2), // 6
        new ExpUpgrade("Increase Projectile speed", "Projectiles move faster.", 1, 5) // 7

    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentGameState = GameState.Playing;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("escape"))
        {
           
            if (currentGameState == GameState.Playing)
            {

                currentGameState = GameState.Paused;
                pauseScreen.SetActive(true);

            }

        }

        if (currentGameState == GameState.Paused || currentGameState == GameState.GameOver)
        {
            Time.timeScale = 0f;
        }

        if (player.health <= 0)
        {
            currentGameState = GameState.GameOver;
            if(!playedAudio)
            {
                playedAudio = true;
                audioSource.PlayOneShot(gameOverAudio, .7f);
            }

            Scene currentScene = SceneManager.GetActiveScene();
            if(currentScene.name == "Game-Lv1")
            {
                if (elapsedTime > PlayerPrefs.GetFloat(Level1ScoreKey, 0))
                {
                    PlayerPrefs.SetFloat(Level1ScoreKey, elapsedTime);
                    PlayerPrefs.Save();
                }
            }
            else if (currentScene.name == "Game-Lv2")
            {

                if (elapsedTime > PlayerPrefs.GetFloat(Level2ScoreKey, 0))
                {
                    PlayerPrefs.SetFloat(Level2ScoreKey, elapsedTime);
                    PlayerPrefs.Save();
                }
            }


                loseScreen.SetActive(true);
        }

        if (currentGameState == GameState.Playing)
        {
            elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            if (player.currentExp >= player.nextLevelExp && currentGameState == GameState.Playing)
            {
                Time.timeScale = 0f;
                currentGameState = GameState.Menu;
                statsUpgradeScreen.SetActive(true);
            }

        }

        if (currentGameState == GameState.Menu)
        {
            // (TODO) check to see if all stats in the list are maxed out
            if (!isUpgrading)
            {
                isUpgrading = true;
                arrSize = StatsUpgradeArr.Length;
                leftRandNumber = Random.Range(0, arrSize);
                rightRandNumber = Random.Range(0, arrSize);
                centerRandNumber = Random.Range(0, arrSize);

                leftTitle.text = StatsUpgradeArr[leftRandNumber].m_Title;
                rightTitle.text = StatsUpgradeArr[rightRandNumber].m_Title;
                centerTitle.text = StatsUpgradeArr[centerRandNumber].m_Title;

                leftDesc.text = StatsUpgradeArr[leftRandNumber].m_Description;
                rightDesc.text = StatsUpgradeArr[rightRandNumber].m_Description;
                centerDesc.text = StatsUpgradeArr[centerRandNumber].m_Description;

                
                
               

                if (StatsUpgradeArr[leftRandNumber].m_Level >= StatsUpgradeArr[leftRandNumber].m_MaxLevel)
                {
                    leftButton.GetComponentInChildren<TMP_Text>().text = "Max";
                    leftButton.interactable = false;
                }
                else
                {

                    leftButton.GetComponentInChildren<TMP_Text>().text = "Upgrade";
                    leftButton.interactable = true;
                }

                if (StatsUpgradeArr[centerRandNumber].m_Level >= StatsUpgradeArr[centerRandNumber].m_MaxLevel)
                {

                    centerButton.GetComponentInChildren<TMP_Text>().text = "Max";
                    centerButton.interactable = false;
                }
                else
                {
                    centerButton.GetComponentInChildren<TMP_Text>().text = "Upgrade";
                    centerButton.interactable = true;
                }

                if (StatsUpgradeArr[rightRandNumber].m_Level >= StatsUpgradeArr[rightRandNumber].m_MaxLevel)
                {
                    rightButton.GetComponentInChildren<TMP_Text>().text = "Max";
                    rightButton.interactable = false;
                }
                else
                {
                    rightButton.GetComponentInChildren<TMP_Text>().text = "Upgrade";
                    rightButton.interactable = true;
                }


            }
        }
    }

    public void NoUpgradeButtonSelect()
    {
        Time.timeScale = 1f;
        currentGameState = GameState.Playing;
        statsUpgradeScreen.SetActive(false);
        isUpgrading = false;

        player.currentExp = 0;
        player.nextLevelExp += 100;
        player.health = player.maxHealth;
        player.ChangeSlider();
    }

    public void LeftButtonSelect()
    {
        Time.timeScale = 1f;
        currentGameState = GameState.Playing;
        statsUpgradeScreen.SetActive(false);
        isUpgrading = false;

        if (StatsUpgradeArr[leftRandNumber].m_Level < StatsUpgradeArr[leftRandNumber].m_MaxLevel)
        {
            StatsUpgradeArr[leftRandNumber].IncLevel();
            
        }

        player.currentExp = 0;
        player.nextLevelExp += 100;
        player.health = player.maxHealth;
        player.ChangeSlider();
    }
        
           

    public void RightButtonSelect()
    {
        Time.timeScale = 1f;
        currentGameState = GameState.Playing;
        statsUpgradeScreen.SetActive(false);
        isUpgrading = false;

        if (StatsUpgradeArr[rightRandNumber].m_Level < StatsUpgradeArr[rightRandNumber].m_MaxLevel)
        {
            StatsUpgradeArr[rightRandNumber].IncLevel();
            
            
        }

     

        player.currentExp = 0;
        player.nextLevelExp += 100;
        player.health = player.maxHealth;
        player.ChangeSlider();
    }

    public void CenterButtonSelect()
    {
        Time.timeScale = 1f;
        currentGameState = GameState.Playing;
        statsUpgradeScreen.SetActive(false);
        isUpgrading = false;
        

       

        if (StatsUpgradeArr[centerRandNumber].m_Level < StatsUpgradeArr[centerRandNumber].m_MaxLevel)
        {
            
            StatsUpgradeArr[centerRandNumber].IncLevel();
    
        }


        player.currentExp = 0;
        player.nextLevelExp += 100;
        player.health = player.maxHealth;
        player.ChangeSlider();
    }

    public void Unpause()
    {
        if (currentGameState == GameState.Paused)
        {
            currentGameState = GameState.Playing;
            pauseScreen.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
