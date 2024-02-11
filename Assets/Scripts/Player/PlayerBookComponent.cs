using StarterAssets;
using System.Collections.Generic;
using UnityEngine;

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
            DisablePlayer(isOpen);
            book.ShowBookVisual(isOpen);
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

    // Method to lock/unlock book visual
    public void LockBookVisual(bool lockState)
    {
        this.lockState = lockState;
    }

    public bool IsOpen() { return isOpen; }
}
