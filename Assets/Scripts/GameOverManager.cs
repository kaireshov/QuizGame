using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText.text = "Your Score: " + ScoreManager.instance.totalScore;
    }

    public void RestartGame()
    {
        ScoreManager.instance.ResetScore();
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
        Debug.Log("Game Closed");
    }
}
