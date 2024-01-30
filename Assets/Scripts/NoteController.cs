
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using StarterAssets;
using System;

public class NoteController : MonoBehaviour
{
    [SerializeField] private Sprite crosshairImg;
    private FirstPersonController player;
    private bool isOpen = false;

    public event Action onClosed;

    [Header("Input")]
    [SerializeField] private KeyCode closeKey;

    [Header("UI")]
    [SerializeField] private GameObject noteCanvas;
    [SerializeField] TextMeshProUGUI noteTextArea;
    [Space(10)][SerializeField][TextArea] private string noteText;

    private void Start()
    {
        noteCanvas.SetActive(false);
    }
    public void ShowNote()
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
}