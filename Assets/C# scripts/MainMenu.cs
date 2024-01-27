using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{   
    public void NewGame()
    {
        ScoreManager.ResetScore();
        SceneManager.LoadScene(1);
    }
    public void LoadRecords()
    {
        SceneManager.LoadScene(3);
    }
    public void MenuBack()
    {
        SceneManager.LoadScene(0);
    }
}
