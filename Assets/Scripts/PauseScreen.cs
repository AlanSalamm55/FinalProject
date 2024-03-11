using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private FirstPersonController playerController;
    [SerializeField] RectTransform canvas;

    private bool isPaused = false;

    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            UnpauseGame();
        }
    }

    private void PauseGame()
    {
        canvas.gameObject.SetActive(true);

        // Enable cursor and unlock it
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        playerController.enabled = false;
        playerController.GetPlayerCameraRoot().GetComponent<PlayerBookComponent>().LockBookVisual(true);
        playerController.GetPlayerCameraRoot().GetComponent<PlayerRaycast>().ShowCrossHair(false);
    }

    private void UnpauseGame()
    {
        canvas.gameObject.SetActive(false);

        // Disable cursor and lock it back
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        playerController.enabled = true;
        playerController.GetPlayerCameraRoot().GetComponent<PlayerBookComponent>().LockBookVisual(false);
        playerController.GetPlayerCameraRoot().GetComponent<PlayerRaycast>().ShowCrossHair(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
}
