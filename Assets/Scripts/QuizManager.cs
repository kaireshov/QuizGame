using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public Button[] optionButtons;
    public TextMeshProUGUI feedbackText;
    public Button nextButton;
    public TextMeshProUGUI scoreText;
    public Image background;
    public AudioSource audioSource;
    public AudioClip correctSound;
    public AudioClip wrongSound;


    public List<Question> questions;
    private int currentQuestionIndex = 0;
    private bool answered = false;

    public int pointsPerCorrectAnswer = 10; // Уровень 1: 10, Уровень 2: 20, Уровень 3: 30
    public string nextLevelSceneName = "Level2"; // Название сцены следующего уровня

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

        Question q = questions[currentQuestionIndex];
        questionText.text = q.questionText;

        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = q.options[i];
            optionButtons[i].interactable = true;
            optionButtons[i].GetComponent<Image>().color = Color.white; // сбрасываем цвет
            int index = i;
            optionButtons[i].onClick.RemoveAllListeners();
            optionButtons[i].onClick.AddListener(() => OnOptionSelected(index));
        }

        UpdateScoreText();
    }

    void OnOptionSelected(int index)
    {
        if (answered) return;

        answered = true;
        Question q = questions[currentQuestionIndex];

        if (index == q.correctOptionIndex)
        {
            feedbackText.text = "Correct! +10";
            audioSource.PlayOneShot(correctSound);
            background.color = Color.green;
            ScoreManager.instance.AddScore(pointsPerCorrectAnswer);
            StartCoroutine(NextQuestionAfterDelay());
        }
        else
        {
            feedbackText.text = "Wrong!";
            audioSource.PlayOneShot(wrongSound);
            background.color = Color.red;
            HighlightCorrectAnswer(q.correctOptionIndex);
            StartCoroutine(NextQuestionAfterDelay());
        }

        foreach (var button in optionButtons)
        {
            button.interactable = false;
        }
    }

    void HighlightCorrectAnswer(int correctIndex)
    {
        optionButtons[correctIndex].GetComponent<Image>().color = Color.green;
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
            // Все вопросы закончились, включаем кнопку Next для перехода на следующий уровень
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
