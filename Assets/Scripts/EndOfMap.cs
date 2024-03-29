using StarterAssets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndOfMap : MonoBehaviour, Interactable
{
    [SerializeField] private List<Word> englishWords;
    [SerializeField] private int pageIndex;
    [SerializeField] private Button backBtn;
    [SerializeField] private Button confirm;
    private PlayerBookComponent bookComponent;
    private bool isOpenedOnce = false;
    private int maxAttempts = 3;
    private int remainingAttempts;
    [SerializeField] private Collider collider;
    private bool isButtonConfirmClickable = true; // Flag to control button clickability
    [SerializeField] private Sprite crosshairImg;

    public event Action onConfirmClick;

    private void Start()
    {
        // Hide buttons initially
        if (backBtn != null)
        {
            backBtn.gameObject.SetActive(false);
        }

        if (confirm != null)
        {
            confirm.gameObject.SetActive(false);
        }

        // Get the collider component
        collider = GetComponent<Collider>();

        // Initialize remaining attempts
        remainingAttempts = maxAttempts;
    }



    private void OnBackButtonClicked()
    {
        Close();
    }

    public void Close()
    {
        // Unlock the book and close it
        if (bookComponent != null)
        {
            bookComponent.HideButtons(false);
            bookComponent.LockBookVisual(false);
            bookComponent.OpenBook(false);
            confirm.gameObject.SetActive(false);
            backBtn.gameObject.SetActive(false); // Hide back button when clicked
        }
    }

    private void OnConfirmButtonClicked()
    {
        // If button is not clickable, return
        if (!isButtonConfirmClickable)
        {
            return;
        }

        StartCoroutine(EnableButtonAfterDelay()); // Start the timer coroutine

        Book book = bookComponent.GetBook();

        if (!book.AreWordCountsEqual(pageIndex))
        {
            PopUpText.Instance.ShowText("Turn back and find all the Kurdish words in this map.");
            return;
        }

        int validation = book.GetPageByIndex(pageIndex).OnConfirmButtonClicked();

        switch (validation)
        {
            case 0: // All correct
                collider.enabled = false;
                PopUpText.Instance.ShowText("Congratulations! You answered correctly!");
                Close(); // Close the book
                onConfirmClick?.Invoke();

                break;
            case 1: // At least one mistake
                remainingAttempts--;
                string message = "Incorrect answer. Please try again. Attempts left: " + remainingAttempts;
                PopUpText.Instance.ShowText(message);

                if (remainingAttempts <= 0)
                {
                    collider.enabled = false;
                    PopUpText.Instance.ShowText("You have reached the maximum attempts.");
                    Close(); // Close the book
                    PauseScreen.Instance.LoseState();
                }
                break;
            case 2: // Not all points used
                PopUpText.Instance.ShowText("Use all words.");
                break;
            default:
                break;
        }
    }

    private IEnumerator EnableButtonAfterDelay()
    {
        // Disable button clickability
        isButtonConfirmClickable = false;
        // Wait for 1 second
        yield return new WaitForSeconds(1f);
        // Enable button clickability
        isButtonConfirmClickable = true;
    }

    public bool IsOpenedOnce()
    {
        return isOpenedOnce;
    }

    public void ShowInteractable()
    {
        if (!isOpenedOnce) { isOpenedOnce = true; }

        if (bookComponent != null)
        {

            // Show buttons and lock book
            if (backBtn != null)
            {
                backBtn.gameObject.SetActive(true);
                backBtn.onClick.AddListener(OnBackButtonClicked);
            }

            if (confirm != null)
            {
                confirm.gameObject.SetActive(true);
                confirm.onClick.AddListener(OnConfirmButtonClicked);
            }

            Book book = bookComponent.GetBook();
            bookComponent.OpenBook(true, pageIndex);
            bookComponent.HideButtons(true);
            // Lock the book
            bookComponent.LockBookVisual(true);

            if (!book.AreWordCountsEqual(pageIndex))
            {
                PopUpText.Instance.ShowText("Turn back and find all the Kurdish words in this map.");
                return;
            }
            PopUpText.Instance.ShowText("match words to illustrations: ");
        }
    }

    public void SetInteractor(FirstPersonController player)
    {
        // Find the book component on the player object
        bookComponent = player.GetPlayerCameraRoot().GetComponent<PlayerBookComponent>();
    }

    public List<Word> GetWordsInInteractable()
    {
        return englishWords;
    }

    public Sprite GetCrosshairImg()
    {
        return crosshairImg;
    }

    public int GetPageIndex()
    {
        return pageIndex;
    }

    public bool IsOpen()
    {
        if (bookComponent)
            return bookComponent.IsOpen();
        else
            return false;
    }

}
