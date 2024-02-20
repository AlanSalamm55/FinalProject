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
            }

            bookComponent.OpenBook(true, pageIndex);
            bookComponent.HideButtons(true);
            // Lock the book
            bookComponent.LockBookVisual(true);
        }
    }

    private void OnBackButtonClicked()
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
}
