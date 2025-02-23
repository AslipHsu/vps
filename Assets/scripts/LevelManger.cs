using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManger : MonoBehaviour
{
    public static LevelManger instance;

    private void Awake()
    {
        instance = this;
    }

    private bool gameActive;
    public float timer;

    public float waitToShowEndScreen = 1f;
    // Start is called before the first frame update
    void Start()
    {
        gameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameActive) 
        {
            timer += Time.deltaTime;
            UIController.instance.UpdateTimer(timer);
        }

    }

    public void EndLevel()
    {
        gameActive = false;
        StartCoroutine(EndLevelCo());
    }

    IEnumerator EndLevelCo()
    {
        yield return new WaitForSeconds(waitToShowEndScreen);

        float minutes = Mathf.FloorToInt(timer / 60f);
        float seconds = Mathf.FloorToInt(timer % 60f);

        UIController.instance.endTimeText.text = minutes + "mins" + seconds.ToString("00") + "secs";
        UIController.instance.levelEndScreen.SetActive(true);
    }
}
