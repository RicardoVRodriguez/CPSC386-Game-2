using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

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

    public class Recipe
    {
        public string m_Title;
        public string m_Description;
        public int m_Level;
        public int m_MaxLevel;
        public int m_MeatNeeded;
        public int m_MushroomsNeeded;
        public int m_CabbageNeeded;

        public Recipe(string title, string description, int level, int maxLevel, int meatNeeded, int mushroomsNeeded, int cabbageNeeded)
        {
            m_Title = title;
            m_Description = description;
            m_Level = level;
            m_MaxLevel = maxLevel;
            m_MeatNeeded = meatNeeded;
            m_MushroomsNeeded = mushroomsNeeded;
            m_CabbageNeeded = cabbageNeeded;
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



    ExpUpgrade[] StatsUpgradeArr = 
    { 
        new ExpUpgrade("Increase Tree Growth", "Trees will grow back quicker.", 1, 5), 
        new ExpUpgrade("Increase Mushroom Drops", "Monsters are more likely to drop mushrooms.", 1, 5), 
        new ExpUpgrade("Increase Meat Drops", "Monsters are more likely to drop Meat.", 1, 5),
        new ExpUpgrade("Increase Cabbage Drops", "Monsters are more likely to drop cabbage.", 1, 5),
        new ExpUpgrade("Increase Max Health", "Your max health will increase but your current health will stay the same.", 1, 5),
        new ExpUpgrade("Increase Movement Speed", "You will move faster.",1,5),
        new ExpUpgrade("Increase Main Attack Damage", "You will move faster.",1,5)

    };

    Recipe[] recipesArr = 
    { 
        
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


            }
        }
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
