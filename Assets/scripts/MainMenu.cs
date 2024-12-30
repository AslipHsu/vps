using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string firstLevelName;
    // Start is called before the first frame update
    public void StartGame()
    {
        SceneManager.LoadScene(firstLevelName);
    }

    // Update is called once per frame
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit~");
    }
}
