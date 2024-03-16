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
    public HashSet<string> kurdishWordList = new HashSet<string>();

    public event Action onBookOpen;

    void Update()
    {
        if (Input.GetKeyDown(interactKey) && !lockState)
        {
            isOpen = !isOpen;
            OpenBook(isOpen);
            onBookOpen?.Invoke();

        }
    }

    public Book GetBook() { return book; }

    private void DisablePlayer(bool disable)
    {
        player.enabled = !disable;
        Cursor.visible = disable;
        Cursor.lockState = disable ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void AddWordToKurdishVocabulary(KurdishWord kurdishWord, int pageIndex)
    {

        if (!kurdishWordList.Contains(kurdishWord.RightAnswer()))
        {
            kurdishWordList.Add(kurdishWord.RightAnswer());
            book.AddWordToNoteBookVisual(kurdishWord, pageIndex);
            int dispIndex = pageIndex + 1;
            PopUpText.Instance.ShowText("New Kurdish word, check page: " + dispIndex);
        }
    }

    public void AddWordToEnglishImages(EnglishWord englishWord, int pageIndex)
    {

        book.AddImageToNoteBookVisual(englishWord, pageIndex);
    }


    // Method to lock/unlock book visual
    public void LockBookVisual(bool lockState)
    {
        this.lockState = lockState;
        book.SetNavigationEnabled(!lockState);
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

    public void HideButtons(bool hide)
    {
        book.HideButtons(hide);
        book.HideCloseText(hide);
    }
}
