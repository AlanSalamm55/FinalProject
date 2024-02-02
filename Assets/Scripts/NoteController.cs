
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using StarterAssets;
using System;
using System.Collections.Generic;

public class NoteController : MonoBehaviour, Interactable
{
    [SerializeField] private Sprite crosshairImg;
    private FirstPersonController player;
    private bool isOpen = false;

    public event Action onClosed;
    [SerializeField] private List<Word> wordsInNote;

    [Header("Input")]
    [SerializeField] private KeyCode closeKey;

    [Header("UI")]
    [SerializeField] private GameObject noteCanvas;
    [SerializeField] TextMeshProUGUI noteTextArea;
    [Space(10)][SerializeField][TextArea] private string noteText;



    private void Start()
    {
        noteCanvas.SetActive(false);
        wordsInNote = new List<Word>();
    }
    public void ShowInteractable()
    {
        noteTextArea.text = noteText;
        noteCanvas.SetActive(true);
        DisablePlayer(false);
        isOpen = true;
    }

    public void CloseNote()
    {
        noteTextArea.text = null;
        noteCanvas.SetActive(false);
        DisablePlayer(true);
        isOpen = false;
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
    }

    public List<Word> GetWordsInInteractable() { return wordsInNote; }
}