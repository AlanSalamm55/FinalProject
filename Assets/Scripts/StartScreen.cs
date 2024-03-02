using StarterAssets;
using System;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private FirstPersonController playerController;

    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    [SerializeField] RectTransform canvas;

    [SerializeField] private float playerMoveTime = 1.0f;

    public event Action onGameStart;

    private void Start()
    {
        canvas.gameObject.SetActive(true);

        // Enable cursor and unlock it
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        playerController.enabled = false;
        playerController.GetPlayerCameraRoot().GetComponent<PlayerBookComponent>().LockBookVisual(true);
        playerController.GetPlayerCameraRoot().GetComponent<PlayerRaycast>().ShowCrossHair(false);

        playerController.transform.position = transform.position;
        playerController.transform.rotation = transform.rotation;

        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
        startButton.Select();
    }

    public void StartGame()
    {
        canvas.gameObject.SetActive(false);
        playerController.GetPlayerCameraRoot().GetComponent<PlayerBookComponent>().LockBookVisual(false);
        playerController.GetPlayerCameraRoot().GetComponent<PlayerRaycast>().ShowCrossHair(true);

        LeanTween.move(playerController.gameObject, startPosition.position, playerMoveTime).setEaseInOutQuad().setOnComplete(() =>
        {
            // Hide cursor and lock it
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            playerController.enabled = true;
        });
        LeanTween.rotate(playerController.gameObject, startPosition.eulerAngles, playerMoveTime).setEaseInOutQuad();

        onGameStart?.Invoke();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
