using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
public class TrueFalseQuestion
{
    public string questionText;
    public bool isTrue;
}

public class TrueFalseQuizManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Button trueButton;
    public Button falseButton;
    public TextMeshProUGUI feedbackText;
    public Button nextButton;
    public TextMeshProUGUI scoreText;
    public Image background;

    public List<TrueFalseQuestion> questions;
    private int currentQuestionIndex = 0;
    private bool answered = false;

    public int pointsPerCorrectAnswer = 20;
    public string nextLevelSceneName = "Level3";

    void Start()
    {
        nextButton.gameObject.SetActive(false);
        ShowQuestion();
    }

    void ShowQuestion()
    {
        answered = false;
        feedbackText.text = "";
        background.color = Color.white;

        TrueFalseQuestion q = questions[currentQuestionIndex];
        questionText.text = q.questionText;

        trueButton.interactable = true;
        falseButton.interactable = true;

        trueButton.GetComponent<Image>().color = Color.white;
        falseButton.GetComponent<Image>().color = Color.white;

        trueButton.onClick.RemoveAllListeners();
        falseButton.onClick.RemoveAllListeners();

        trueButton.onClick.AddListener(() => OnAnswerSelected(true));
        falseButton.onClick.AddListener(() => OnAnswerSelected(false));

        UpdateScoreText();
    }

    void OnAnswerSelected(bool answer)
    {
        if (answered) return;

        answered = true;
        TrueFalseQuestion q = questions[currentQuestionIndex];

        if (answer == q.isTrue)
        {
            feedbackText.text = "Correct! +20";
            background.color = Color.green;
            ScoreManager.instance.AddScore(pointsPerCorrectAnswer);
            StartCoroutine(NextQuestionAfterDelay());
        }
        else
        {
            feedbackText.text = "Wrong!";
            background.color = Color.red;
            HighlightCorrectAnswer(q.isTrue);
            StartCoroutine(NextQuestionAfterDelay());
        }

        trueButton.interactable = false;
        falseButton.interactable = false;
    }

    void HighlightCorrectAnswer(bool correctAnswer)
    {
        if (correctAnswer)
        {
            trueButton.GetComponent<Image>().color = Color.green;
        }
        else
        {
            falseButton.GetComponent<Image>().color = Color.green;
        }
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

    public void OnNextLevel()
    {
        SceneManager.LoadScene(nextLevelSceneName);
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + ScoreManager.instance.totalScore;
    }
}
