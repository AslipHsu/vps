using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    private void Awake() { 
        instance = this; 
    }


    public Slider expLvSlider;
    public TMP_Text expLvText;

    public LevelUpSelectionButton[] levelUpButton;

    public GameObject LevelUpPanel;

    public TMP_Text coinText;

    public PlayerStatUpgradeDisplay moveSpeedUpgradeDisplay, healthUpgradeDisplay, pickupRangeUpgradeDisplay, maxWeaponUpgradeDisplay;

    public TMP_Text timeText;

    public GameObject levelEndScreen;
    public TMP_Text endTimeText;

    public string mainMenuText;

    public GameObject pauseScreen;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) 
        { 
            PauseUnpause();
        }
    }

    public void UpdateExperience(int currentExp,int levelExp ,int currentLvl)
    {
        expLvSlider.maxValue = levelExp;
        expLvSlider.value = currentExp;

        expLvText.text = "Level: " + currentLvl;
    }

    public void SkipLevelUp()
    {
        LevelUpPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void UpdateCoins()
    {
        coinText.text = "Coins: " + CoinController.instance.currentCoins;
    }
    public void PurechaseMoveSpeed()
    {
        PlayerStatsController.instance.PurechaseMoveSpeed();
        SkipLevelUp();
    }
    public void PurchaseHealth()
    {
        PlayerStatsController.instance.PurchaseHealth();
        SkipLevelUp();
    }
    public void PurchasePickupRange()
    {
        PlayerStatsController.instance.PurchasePickupRange();
        SkipLevelUp();
    }
    public void PurchaseMaxWeapons()
    {
        PlayerStatsController.instance.PurchaseMaxWeapons();
        SkipLevelUp();
    }

    public void UpdateTimer(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60f);
        float seconds = Mathf.FloorToInt(time % 60f);
        timeText.text = "Time: " + minutes + ":" + seconds.ToString("00");
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(mainMenuText);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        var Scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(Scene.name);
        Debug.Log("scene name:" + Scene.name);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseUnpause()
    {
        if (!pauseScreen.activeSelf)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseScreen.SetActive(false);
            if (!pauseScreen.activeSelf)
            {
                Time.timeScale = 1f;
            }

        }
    }

}

