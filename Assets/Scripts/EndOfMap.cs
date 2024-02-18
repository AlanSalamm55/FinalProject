using StarterAssets;
using System;
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
    private void OnTriggerEnter(Collider other)
    {
        // Find the book component on the player object
        FirstPersonController player = other.GetComponent<FirstPersonController>();
        PlayerBookComponent bookComponent = player.GetPlayerCameraRoot().GetComponent<PlayerBookComponent>();

        if (bookComponent != null)
        {
            // Add words to the book
            foreach (EnglishWord word in englishWords)
            {
                bookComponent.AddWordToEnglishImages(word, pageIndex);
            }
            // Show buttons and lock book
            if (backBtn != null)
            {
                backBtn.gameObject.SetActive(true);
            }
            if (confirm != null)
            {
                confirm.gameObject.SetActive(true);
            }

            bookComponent.OpenBook(true, pageIndex);

            // Lock the book
            bookComponent.LockBookVisual(true);
        }
    }
    private void OnBackButtonClicked()
    {
        // Unlock the book and close it
        if (bookComponent != null)
        {
            bookComponent.LockBookVisual(false);
            bookComponent.OpenBook(false);
        }
    }

}
