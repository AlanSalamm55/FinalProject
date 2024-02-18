using StarterAssets;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBookComponent : MonoBehaviour
{
    [SerializeField] private FirstPersonController player;

    private bool isOpen = false;

    [SerializeField] private Book book;
    private bool lockState = false;

    [Header("Input System")]
    [SerializeField] private KeyCode interactKey;

    private void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(interactKey) && !lockState)
        {
            isOpen = !isOpen;

            OpenBook(isOpen);
        }
    }

    private void DisablePlayer(bool disable)
    {
        player.enabled = !disable;
        Cursor.visible = disable;
        Cursor.lockState = disable ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void AddWordToKurdishVocabulary(KurdishWord kurdishWord, int pageIndex)
    {
        book.AddWordToNoteBookVisual(kurdishWord, pageIndex);
    }

    public void AddWordToEnglishImages(EnglishWord englishWord, int pageIndex)
    {

        book.AddImageToNoteBookVisual(englishWord, pageIndex);
    }


    // Method to lock/unlock book visual
    public void LockBookVisual(bool lockState)
    {
        this.lockState = lockState;
    }

    public bool IsOpen() { return isOpen; }

    internal void OpenBook(bool open)
    {
        DisablePlayer(open);
        book.ShowBookVisual(open);
    }
    internal void OpenBook(bool open, int page)
    {
        DisablePlayer(open);
        book.ShowPageAtIndex(page);
        book.ShowBookVisual(open);
    }
}
