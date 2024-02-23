using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndOfMap : MonoBehaviour
{
    [SerializeField] private List<EnglishWord> englishWords;
    [SerializeField] private int pageIndex;
    [SerializeField] private Button backBtn;
    [SerializeField] private Button confirm;
    private PlayerBookComponent bookComponent;
    private bool imagesAdded = false;
    private int maxAttempts = 3;
    private int remainingAttempts;
    private bool isAnswersCorrect = false;
    [SerializeField] private Collider collider;
    private bool isButtonConfirmClickable = true; // Flag to control button clickability

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

    private void OnTriggerEnter(Collider other)
    {
        // Find the book component on the player object
        FirstPersonController player = other.GetComponent<FirstPersonController>();
        bookComponent = player.GetPlayerCameraRoot().GetComponent<PlayerBookComponent>();

        if (bookComponent != null)
        {
            if (!imagesAdded)
            {
                // Add words to the book
                foreach (EnglishWord word in englishWords)
                {
                    bookComponent.AddWordToEnglishImages(word, pageIndex);
                }
                imagesAdded = true; // Set to true once images are added
            }
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

            bookComponent.OpenBook(true, pageIndex);
            bookComponent.HideButtons(true);
            // Lock the book
            bookComponent.LockBookVisual(true);
        }
    }

    private void OnBackButtonClicked()
    {
        CloseBookAndReset();
    }

    private void CloseBookAndReset()
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

    private IEnumerator WaitForButtonClick()
    {
        // Disable button clickability
        isButtonConfirmClickable = false;
        // Wait for 1 second
        yield return new WaitForSeconds(1f);
        // Enable button clickability
        isButtonConfirmClickable = true;
    }

    private void OnConfirmButtonClicked()
    {
        // If button is not clickable, return
        if (!isButtonConfirmClickable)
        {
            return;
        }

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
                CloseBookAndReset(); // Close the book
                break;
            case 1: // At least one mistake
                remainingAttempts--;
                string message = "Incorrect answer. Please try again. Attempts left: " + remainingAttempts;
                PopUpText.Instance.ShowText(message);

                if (remainingAttempts <= 0)
                {
                    collider.enabled = false;
                    PopUpText.Instance.ShowText("You have reached the maximum attempts.");
                    CloseBookAndReset(); // Close the book
                }
                break;
            case 2: // Not all points used
                PopUpText.Instance.ShowText("Use all points.");
                break;
            default:
                break;
        }
    }

}
