using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int totalScore = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // если вдруг будет дубликат
        }
    }

    public void AddScore(int amount)
    {
        totalScore += amount;
    }

    public void ResetScore()
    {
        totalScore = 0;
    }
}
