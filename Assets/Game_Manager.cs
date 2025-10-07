using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game_Manager : MonoBehaviour
{

    public class StatsUpgrade
    {
        public string m_Title;
        public string m_Description;
        public int m_Level;
        public int m_MaxLevel;

        public StatsUpgrade(string title, string description, int level, int maxLevel )
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

    public class WeaponsUpgrade
    { 
    
    
    }

    public Player player;
    public GameObject StatsUpgradeScreen;
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



    StatsUpgrade[] StatsUpgradeArr = 
    { 
        new StatsUpgrade("Increase Tree Growth", "Trees will grow back quicker.", 1, 5), 
        new StatsUpgrade("Increase Mushroom Drops", "Monsters are more likely to drop mushrooms.", 1, 5), 
        new StatsUpgrade("Increase Meat Drops", "Monsters are more likely to drop Meat.", 1, 5),
        new StatsUpgrade("Increase cabbage Drops", "Monsters are more likely to drop cabbage.", 1, 5),
       
    };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}",minutes,seconds);

        // check to see if all stats in the list are maxed out
        if (player.currentExp >= player.nextLevelExp && !StatsUpgradeScreen.activeSelf)
        {
            StatsUpgradeScreen.SetActive(true); 
        }

        if(StatsUpgradeScreen.activeSelf && !isUpgrading)
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

    public void LeftButtonSelect()
    {
        StatsUpgradeScreen.SetActive(false);
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
        StatsUpgradeScreen.SetActive(false);
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
        StatsUpgradeScreen.SetActive(false);
        isUpgrading = false;

        if (StatsUpgradeArr[centerRandNumber].m_Level < StatsUpgradeArr[centerRandNumber].m_MaxLevel)
        {
            StatsUpgradeArr[centerRandNumber].IncLevel();
        }

        player.currentExp = 0;
        player.nextLevelExp += 100;
        player.ChangeSlider();
    }
}
