using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpText : MonoBehaviour
{
    public static PopUpText Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI textPrefab;
    [SerializeField] private float displayTime = 5f;
    [SerializeField] private float fadeDuration = 1f;

    private void Awake()
    {
        // Ensure there is only one instance of PopUpText
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Method to display text on screen
    public void ShowText(string message)
    {
        if (textPrefab != null)
        {
            TextMeshProUGUI newText = Instantiate(textPrefab, transform);
            newText.text = message;
            StartCoroutine(FadeAndRemove(newText));
        }
        else
        {
            Debug.LogError("Text Prefab is not assigned.");
        }
    }

    private IEnumerator FadeAndRemove(TextMeshProUGUI text)
    {
        yield return new WaitForSeconds(displayTime);

        float elapsedTime = 0f;
        Color startColor = text.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            text.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            yield return null;
        }

        Destroy(text.gameObject);
    }
}
