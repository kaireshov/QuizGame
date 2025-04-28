using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class WordPuzzleManager : MonoBehaviour
{
    public TextMeshProUGUI scrambledWordText;
    public TMP_InputField answerInput;
    public TextMeshProUGUI feedbackText;
    public Button submitButton;
    public Button nextButton;
    public TextMeshProUGUI scoreText;
    public Image background;

    public List<WordQuestion> questions;
    private int currentQuestionIndex = 0;

    public int pointsPerCorrectAnswer = 30;
    public string gameOverSceneName = "GameOver";

    private void Start()
    {
        nextButton.gameObject.SetActive(false);
        ShowQuestion();
    }

    void ShowQuestion()
    {
        feedbackText.text = "";
        background.color = Color.white;
        answerInput.text = "";
        answerInput.interactable = true;
        submitButton.interactable = true;

        WordQuestion q = questions[currentQuestionIndex];
        scrambledWordText.text = q.scrambledWord;
        UpdateScoreText();
    }

    public void OnSubmitAnswer()
    {
        WordQuestion q = questions[currentQuestionIndex];
        string playerAnswer = answerInput.text.ToLower().Trim();
        string correctAnswer = q.correctWord.ToLower().Trim();

        answerInput.interactable = false;
        submitButton.interactable = false;

        if (playerAnswer == correctAnswer)
        {
            feedbackText.text = "Correct! +30";
            background.color = Color.green;
            ScoreManager.instance.AddScore(pointsPerCorrectAnswer);
        }
        else
        {
            feedbackText.text = $"Wrong! Correct answer: {q.correctWord}";
            background.color = Color.red;
        }

        StartCoroutine(NextQuestionAfterDelay());
    }

    IEnumerator NextQuestionAfterDelay()
    {
        yield return new WaitForSeconds(3f);

        currentQuestionIndex++;

        if (currentQuestionIndex < questions.Count)
        {
            ShowQuestion();
        }
        else
        {
            feedbackText.text = "Level Complete!";
            nextButton.gameObject.SetActive(true);
        }
    }

    public void OnNext()
    {
        SceneManager.LoadScene(gameOverSceneName);
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + ScoreManager.instance.totalScore;
    }
}
