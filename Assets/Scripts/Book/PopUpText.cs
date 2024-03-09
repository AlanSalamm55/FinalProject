using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpText : MonoBehaviour
{
    public static PopUpText Instance { get; private set; }

    [SerializeField] private ExampleText textPrefab;
    [SerializeField] private float displayTime = 5f;
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private Image imagePrefab;

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
            ExampleText newText = Instantiate(textPrefab, transform);
            newText.SetText(message);
            StartCoroutine(FadeAndRemove(newText));
        }
        else
        {
            Debug.LogError("Text Prefab is not assigned.");
        }
    }

    public void ShowText(string message, int fadeDurationInSeconds)
    {
        if (textPrefab != null)
        {
            ExampleText newText = Instantiate(textPrefab, transform);
            newText.SetText(message);
            StartCoroutine(FadeAndRemove(newText, fadeDurationInSeconds));
        }
        else
        {
            Debug.LogError("Text Prefab is not assigned.");
        }
    }

    private IEnumerator FadeAndRemove(ExampleText exampleText)
    {
        yield return new WaitForSeconds(displayTime);

        exampleText.Fade(fadeDuration);

        // Wait for fade duration before destroying the object
        yield return new WaitForSeconds(fadeDuration);

        Destroy(exampleText.gameObject);
    }

    private IEnumerator FadeAndRemove(ExampleText exampleText, int fadeDurationInSeconds)
    {
        yield return new WaitForSeconds(displayTime);

        exampleText.Fade(fadeDurationInSeconds);

        // Wait for fade duration before destroying the object
        yield return new WaitForSeconds(fadeDurationInSeconds);

        Destroy(exampleText.gameObject);
    }


    public void ShowImage(Sprite image, int fadeDurationInSeconds)
    {
        if (imagePrefab != null)
        {
            Image newImage = Instantiate(imagePrefab, transform);
            newImage.sprite = image;
            StartCoroutine(FadeAndRemove(newImage, fadeDurationInSeconds));
        }
        else
        {
            Debug.LogError("Image Prefab is not assigned.");
        }
    }

    private IEnumerator FadeAndRemove(Image image, int fadeDurationInSeconds)
    {
        yield return new WaitForSeconds(displayTime);

        float elapsedTime = 0f;
        Color startColor = image.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        float fadeDuration = fadeDurationInSeconds;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            image.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            yield return null;
        }

        Destroy(image.gameObject);
    }
    private IEnumerator FadeAndRemove(Image image)
    {
        yield return new WaitForSeconds(displayTime);

        float elapsedTime = 0f;
        Color startColor = image.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);


        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            image.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            yield return null;
        }

        Destroy(image.gameObject);
    }

    // Method to display image on screen
    public void ShowImage(Sprite image)
    {
        if (imagePrefab != null)
        {
            Image newImage = Instantiate(imagePrefab, transform);
            newImage.sprite = image;
            StartCoroutine(FadeAndRemove(newImage));
        }
        else
        {
            Debug.LogError("Image Prefab is not assigned.");
        }
    }
}
