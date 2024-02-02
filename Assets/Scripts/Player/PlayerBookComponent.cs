using StarterAssets;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBookComponent : MonoBehaviour
{
    [SerializeField] private FirstPersonController player;

    //make this sprites later 
    private List<string> KurdishWords;

    private List<string> EnglishWords;
    private bool isOpen = false;

    [SerializeField] Book book;

    [Header("Input System")]
    [SerializeField] private KeyCode interactKey;

    private void Start()
    {
        KurdishWords = new List<string>();
        EnglishWords = new List<string>();
    }
    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            isOpen = !isOpen;
            DisablePlayer(isOpen);
            book.ShowBookVisual(isOpen);
        }
    }

    private void DisablePlayer(bool disable)
    {
        player.enabled = !disable;
        // Toggle cursor visibility and lock state based on the player's ability to move
        Cursor.visible = disable;
        Cursor.lockState = disable ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void AddWordToKurdishVocabulary(string kurdishWord) { KurdishWords.Add(kurdishWord); }
}
