using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManu : MonoBehaviour
{
    public string playerID;

    public void CheckPlayerID()
    {

    }

    #region SceneChange
    public void EnterGame()
    {
        SceneManager.LoadScene("Map1");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainManu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion

}
