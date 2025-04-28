using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LevelIntro : MonoBehaviour
{
    public CanvasGroup introCanvasGroup;
    public TextMeshProUGUI levelText;
    public float fadeDuration = 1f;
    public float displayDuration = 2f;

    void Start()
    {
        StartCoroutine(ShowLevelIntro());
    }

    IEnumerator ShowLevelIntro()
    {
        // Показываем панель
        introCanvasGroup.alpha = 1f;
        yield return new WaitForSeconds(displayDuration);

        // Плавное исчезновение
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            introCanvasGroup.alpha = 1f - Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        introCanvasGroup.alpha = 0f;
        Destroy(gameObject); // Удаляем панель после исчезновения
    }
}
