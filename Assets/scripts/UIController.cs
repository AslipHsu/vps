using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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
}
