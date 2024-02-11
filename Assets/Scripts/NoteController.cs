using UnityEngine;
using TMPro;
using StarterAssets;
using System;
using System.Collections.Generic;

public class NoteController : MonoBehaviour, Interactable
{
    [SerializeField] private Sprite crosshairImg;
    private FirstPersonController player;
    private bool isOpen = false;
    private bool isOpenedOnce = false;
    private PlayerBookComponent playerBookComponent; // Reference to PlayerBookComponent

    public event Action onClosed;
    [SerializeField] private List<KurdishWord> wordsInNote;

    [Header("Input")]
    [SerializeField] private KeyCode closeKey;

    [Header("UI")]
    [SerializeField] private GameObject noteCanvas;
    [SerializeField] private int pageIndex;

    private void Start()
    {
        noteCanvas.SetActive(false);
    }

    public void ShowInteractable()
    {
        if (!isOpenedOnce) { isOpenedOnce = true; }
        noteCanvas.SetActive(true);
        DisablePlayer(false);
        isOpen = true;
        playerBookComponent.LockBookVisual(true); // Lock the book visual when the note is open
    }

    public void CloseNote()
    {
        noteCanvas.SetActive(false);
        DisablePlayer(true);
        isOpen = false;
        playerBookComponent.LockBookVisual(false); // Unlock the book visual when the note is closed
    }

    private void DisablePlayer(bool disable)
    {
        player.enabled = disable;
    }

    public Sprite GetCrosshairImg()
    {
        return crosshairImg;
    }

    private void Update()
    {
        if (isOpen)
        {
            if (Input.GetKeyDown(closeKey))
            {
                CloseNote();
            }
        }
    }

    public void SetInteractor(FirstPersonController player)
    {
        this.player = player;
        playerBookComponent = player.GetPlayerCameraRoot().GetComponent<PlayerBookComponent>();
    }

    public List<KurdishWord> GetWordsInInteractable()
    {
        return wordsInNote;
    }

    public bool IsOpenedOnce()
    {
        return isOpenedOnce;
    }

    public int GetPageIndex() { return pageIndex; }


}
