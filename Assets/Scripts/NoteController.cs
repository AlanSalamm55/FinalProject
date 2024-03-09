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
    [SerializeField] private List<Word> wordsInNote;

    [Header("UI")]
    [SerializeField] private GameObject noteCanvas;
    [SerializeField] private int pageIndex;

    public event Action onClosedEvent;

    private void Start()
    {
        noteCanvas.SetActive(false);
    }

    public bool IsOpen() { return isOpen; }

    public void ShowInteractable()
    {
        foreach (KurdishWord word in wordsInNote) { word.RefreshGuess(word); }
        if (!isOpenedOnce)
        {
            isOpenedOnce = true;
        }
        noteCanvas.SetActive(true);
        DisablePlayer(false);
        isOpen = true;
        playerBookComponent.LockBookVisual(true); // Lock the book visual when the note is open
    }

    public void Close()
    {
        onClosedEvent?.Invoke();

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



    public void SetInteractor(FirstPersonController player)
    {
        this.player = player;
        playerBookComponent = player.GetPlayerCameraRoot().GetComponent<PlayerBookComponent>();
    }

    public List<Word> GetWordsInInteractable()
    {
        return wordsInNote;
    }

    public bool IsOpenedOnce()
    {
        return isOpenedOnce;
    }

    public int GetPageIndex() { return pageIndex; }

    List<Word> Interactable.GetWordsInInteractable()
    {

        return wordsInNote;
    }
}
