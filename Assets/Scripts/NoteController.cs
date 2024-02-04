
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

    public event Action onClosed;
    [SerializeField] private List<string> wordsInNote = new List<string>();

    [Header("Input")]
    [SerializeField] private KeyCode closeKey;

    [Header("UI")]
    [SerializeField] private GameObject noteCanvas;
    [SerializeField] private int pageIndex;
    [SerializeField] TextMeshProUGUI noteTextArea;
    [Space(10)][SerializeField][TextArea] private string noteText;




    private void Start()
    {
        noteCanvas.SetActive(false);
    }
    public void ShowInteractable()
    {
        if (!isOpenedOnce) { isOpenedOnce = true; }
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

    public List<string> GetWordsInInteractable()
    {
        return wordsInNote;
    }

    public bool IsOpenedOnce()
    {
        return isOpenedOnce;
    }

    public int GetPageIndex() { return pageIndex; }
}
