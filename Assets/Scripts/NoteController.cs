
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using StarterAssets;
using UnityEngine.Rendering;

public class NoteController : MonoBehaviour
{
    [SerializeField] Sprite crosshairImg;
    [SerializeField] FirstPersonController player;
    private bool isOpen = false;
    //  [SerializeField] private UnityEvent openEvent;

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
        //  openEvent.Invoke();
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
}