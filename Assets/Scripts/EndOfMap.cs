using StarterAssets;
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

    private void OnConfirmButtonClicked()
    {
        if (isAnswersCorrect || remainingAttempts <= 0)
        {
            // If answers are correct or no attempts left, remove the collider
            collider.enabled = false;
            PopUpText.Instance.ShowText(isAnswersCorrect ? "Congratulations! You answered correctly!" : "You have reached the maximum attempts.");
            CloseBookAndReset(); // Close the book
            return;
        }

        Book book = bookComponent.GetBook();
        isAnswersCorrect = book.GetPageByIndex(pageIndex).OnConfirmButtonClicked();

        if (!isAnswersCorrect)
        {
            remainingAttempts--;
            string message = "Incorrect answer. Please try again. Attempts left: " + remainingAttempts;
            PopUpText.Instance.ShowText(message);

            if (remainingAttempts <= 0)
            {
                // If no attempts left, remove the collider
                collider.enabled = false;
                PopUpText.Instance.ShowText("You have reached the maximum attempts.");
                CloseBookAndReset(); // Close the book
            }
        }
    }
}
