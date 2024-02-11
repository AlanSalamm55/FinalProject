using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KurdishWord : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_InputField guessInputField; // Reference to the TMP_InputField
    [SerializeField] private string rightAnswer;

    private bool isDragging = false;
    private Vector2 pointerOffset;
    private RectTransform rectTransform;
    private Vector2 originalPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // Enable the input field for entering the guess
            guessInputField.gameObject.SetActive(true);
            // Focus the input field so the user can start typing immediately
            guessInputField.Select();
            guessInputField.ActivateInputField();
        }
        else if (eventData.button == PointerEventData.InputButton.Left)
        {
            // Start dragging the object
            isDragging = true;
            // Calculate the pointer offset to maintain the position of the object while dragging
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out pointerOffset);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            // Calculate the new position of the UI element based on the pointer position and offset
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPointerPosition);
            rectTransform.localPosition = localPointerPosition - pointerOffset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
    }

    // Method to show the guess text
    public void ShowGuessText()
    {
        guessInputField.gameObject.SetActive(true);
    }
}
