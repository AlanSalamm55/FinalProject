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
    [SerializeField] private EnglishWord englishWord;
    [SerializeField] private Image checkImg;
    [SerializeField] Sprite[] checkState;
    private bool isDragging = false;
    private Vector2 pointerOffset;
    private RectTransform rectTransform;
    private Transform parent;
    private bool isDraggingAllowed = true;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Start()
    {
        parent = transform.parent;
        checkImg.gameObject.SetActive(false);
        isDraggingAllowed = true;
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
        if (isDragging && isDraggingAllowed) // Check if dragging is allowed
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectTransform.parent as RectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPointerPosition);
            rectTransform.localPosition = localPointerPosition - pointerOffset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDraggingAllowed) { return; }
        transform.SetParent(parent);
        englishWord = null;

        isDragging = false;

        bool collidedWithEnglishWord = Physics2D.OverlapBox(transform.position, new Vector2(0.1f, 0.1f), 0f, LayerMask.GetMask("EnglishWord"));
        bool collidedWithKurdishContainer = Physics2D.OverlapBox(transform.position, new Vector2(0.1f, 0.1f), 0f, LayerMask.GetMask("KurdishContainer"));

        if (collidedWithEnglishWord)
        {
            // Make the KurdishWord a child of the EnglishWord
            Transform englishWordTransform = eventData.pointerCurrentRaycast.gameObject.transform;
            EnglishWord collideEngWord = englishWordTransform.GetComponentInParent<EnglishWord>();
            transform.SetParent(englishWordTransform);
            if (collideEngWord != null)
            {
                Debug.Log("this is called");
                englishWord = collideEngWord;
            }
            // Set position with a slight offset
            Vector3 offset = new Vector3(0f, -150f, 0f); // Adjust the offset as needed
            transform.localPosition = Vector3.zero + offset;
            transform.localScale = Vector3.one;
        }

        DragEnded?.Invoke();

    }


    public EnglishWord GetEnglishhWord() { return englishWord; }

    public bool IsAnswerValid()
    {
        bool isValid = false;
        if (englishWord)
        {
            isValid = rightAnswer == englishWord.RightAnswer();
            if (isValid)
            {
                isDraggingAllowed = false; // Lock dragging only if the answer is correct
            }
            checkImg.sprite = isValid ? checkState[0] : checkState[1];
        }
        else
        {
            Debug.Log("no English word for this");
            checkImg.sprite = checkState[2]; // Index 2 for no English word
        }
        checkImg.gameObject.SetActive(true); // Show checkImg
        return isValid;
    }


    public void ShowGuessText()
    {
        guessInputField.gameObject.SetActive(true);
    }
    public string RightAnswer() { return rightAnswer; }


}
