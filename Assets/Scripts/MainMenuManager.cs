using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        ScoreManager.instance.ResetScore();
        SceneManager.LoadScene("Level1");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Closed"); // в редакторе Unity не закроется, только в сборке
    }
}
