
using StarterAssets;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerRaycast : MonoBehaviour
{

    [Header("Raycast")]
    [SerializeField] private FirstPersonController player;
    private PlayerBookComponent bookComp;
    [SerializeField] private float rayLength = 5f;

    [Header("Raycast")]
    private Image crosshair;
    [SerializeField] private Sprite crosshairBase;
    [SerializeField] private Image eKey;

    [Header("Input System")]
    [SerializeField] private KeyCode interactKey;


    private Interactable interactable;

    void Start()
    {
        bookComp = this.GetComponent<PlayerBookComponent>();
        crosshair = GetComponentInChildren<Image>();

        if (crosshair != null)
        {
            crosshair.sprite = crosshairBase;
        }

        eKey.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, rayLength))
        {
            HandleRaycastHit(hit);
        }
        else
        {
            ClearInteractable();
        }

        TryInteract();
    }

    private void HandleRaycastHit(RaycastHit hit)
    {
        var readableItem = hit.collider.GetComponent<Interactable>();
        if (readableItem != null)
        {
            interactable = readableItem;
            interactable.SetInteractor(player);
            crosshair.sprite = interactable.GetCrosshairImg();
            eKey.gameObject.SetActive(true);
        }
        else
        {
            ClearInteractable();
        }
    }

    private void TryInteract()
    {
        if (interactable != null && Input.GetKeyDown(interactKey) && !bookComp.IsOpen())
        {
            if (interactable.IsOpen())
            {
                interactable.Close();
            }
            else
            {
                if (!interactable.IsOpenedOnce())
                {
                    HandleWordInteractions();
                }
                interactable.ShowInteractable();
            }
        }
    }


    private void HandleWordInteractions()
    {
        List<Word> words = interactable.GetWordsInInteractable();
        int pageIndex = interactable.GetPageIndex();

        if (words == null || words.Count == 0)
            return;

        switch (words[0])
        {
            case KurdishWord:
                // All words in the list are Kurdish words
                foreach (KurdishWord kurdishWord in words)
                {
                    bookComp.AddWordToKurdishVocabulary(kurdishWord, pageIndex);
                }
                break;

            case EnglishWord:
                // All words in the list are English words
                foreach (EnglishWord englishWord in words)
                {
                    bookComp.AddWordToEnglishImages(englishWord, pageIndex);
                }
                PopUpText.Instance.ShowText("match words to illustrations: ");
                break;

            default:
                // Handle other types of words if needed
                break;
        }
    }




    private void ClearInteractable()
    {
        if (interactable != null)
        {
            SetCrossHairImg(crosshairBase);
            eKey.gameObject.SetActive(false);
            interactable = null;
        }
    }

    private void SetCrossHairImg(Sprite img) { crosshair.sprite = img; }

    internal void ShowCrossHair(bool hide)
    {
        crosshair.gameObject.SetActive(hide);
    }
}