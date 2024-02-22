
using StarterAssets;
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
        var readableItem = hit.collider.GetComponent<NoteController>();
        if (readableItem != null)
        {
            interactable = readableItem;
            interactable.SetInteractor(player);
            crosshair.sprite = interactable.GetCrosshairImg();
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
            if (!interactable.IsOpenedOnce())
            {
                HandleWordInteractions();
            }
            interactable.ShowInteractable();
        }
    }

    private void HandleWordInteractions()
    {
        List<KurdishWord> words = interactable.GetWordsInInteractable();
        int pageIndex = interactable.GetPageIndex();
        if (words != null)
        {
            foreach (KurdishWord word in words)
            {
                bookComp.AddWordToKurdishVocabulary(word, pageIndex);
            }
            PopUpText.Instance.ShowText("new vocab added to ur book");

        }
    }



    private void ClearInteractable()
    {
        if (interactable != null)
        {
            SetCrossHairImg(crosshairBase);
            interactable = null;
        }
    }

    private void SetCrossHairImg(Sprite img) { crosshair.sprite = img; }



}