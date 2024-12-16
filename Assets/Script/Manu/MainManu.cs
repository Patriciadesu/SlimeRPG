using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    public string playerID;

    private void Start()
    {
        CheckPlayerID();
    }

    public void CheckPlayerID()
    {

    }

    #region SceneChange
    public void EnterGame()
    {
        SceneManager.LoadScene("MainMap");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion

}
