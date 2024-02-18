using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class KurdishWord : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
    public event Action DragEnded;

    [SerializeField] private Image image;
    [SerializeField] private TMP_InputField guessInputField;
    [SerializeField] private string rightAnswer;

    private bool isDragging = false;
    private Vector2 pointerOffset;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            guessInputField.gameObject.SetActive(true);
            guessInputField.Select();
            guessInputField.ActivateInputField();
        }
        else if (eventData.button == PointerEventData.InputButton.Left)
        {
            isDragging = true;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out pointerOffset);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPointerPosition);
            rectTransform.localPosition = localPointerPosition - pointerOffset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        bool collidedWithEnglishWord = Physics2D.OverlapCircle(transform.position, 0.1f, LayerMask.GetMask("EnglishWord"));

        if (collidedWithEnglishWord)
        {
            // Make the KurdishWord a child of the EnglishWord
            Transform englishWordTransform = eventData.pointerCurrentRaycast.gameObject.transform;
            transform.SetParent(englishWordTransform);

            // Set position with a slight offset
            Vector3 offset = new Vector3(0f, -100f, 0f); // Adjust the offset as needed
            transform.localPosition = Vector3.zero + offset;
            transform.localScale = Vector3.one;
        }
        else
        {
            DragEnded?.Invoke();
        }
    }

    public void ShowGuessText()
    {
        guessInputField.gameObject.SetActive(true);
    }
}
