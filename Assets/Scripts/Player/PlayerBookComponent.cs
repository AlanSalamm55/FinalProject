using StarterAssets;
using UnityEngine;

public class PlayerBookComponent : MonoBehaviour
{
    [SerializeField] private FirstPersonController player;
    private bool isOpen = false;

    //make a book class later
    [SerializeField] RectTransform Book;

    [Header("Input System")]
    [SerializeField] private KeyCode interactKey;

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            isOpen = !isOpen;
            DisablePlayer(isOpen);
            Book.gameObject.SetActive(isOpen);
        }
    }

    private void DisablePlayer(bool disable)
    {
        player.enabled = !disable;
        // Toggle cursor visibility and lock state based on the player's ability to move
        Cursor.visible = disable;
        Cursor.lockState = disable ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
