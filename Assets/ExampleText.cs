using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExampleText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private Image image;

    // Function to set the text of the TextMeshProUGUI
    public void SetText(string newText)
    {
        if (textMesh != null)
        {
            textMesh.text = newText;
        }
        else
        {
            Debug.LogWarning("TextMeshProUGUI is not assigned. Please assign it in the Inspector.");
        }
    }

    // Function to set the sprite of the Image
    public void SetImage(Sprite newImage)
    {
        if (image != null)
        {
            image.sprite = newImage;
        }
        else
        {
            Debug.LogWarning("Image is not assigned. Please assign it in the Inspector.");
        }
    }

    // Function to fade both text and image
    public void Fade(float duration)
    {
        if (textMesh != null)
        {
            StartCoroutine(FadeText(duration));
        }

        if (image != null)
        {
            StartCoroutine(FadeImage(duration));
        }
    }

    private IEnumerator FadeText(float duration)
    {
        float startTime = Time.time;
        Color startColor = textMesh.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (Time.time < startTime + duration)
        {
            float elapsedTime = Time.time - startTime;
            float t = elapsedTime / duration;
            textMesh.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        textMesh.color = endColor;
    }

    private IEnumerator FadeImage(float duration)
    {
        float startTime = Time.time;
        Color startColor = image.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (Time.time < startTime + duration)
        {
            float elapsedTime = Time.time - startTime;
            float t = elapsedTime / duration;
            image.color = Color.Lerp(startColor, endColor, t);
            yield return null;
        }

        image.color = endColor;
    }
}
