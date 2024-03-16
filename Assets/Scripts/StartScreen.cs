using System;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private FirstPersonController playerController;
    [SerializeField] private Image fadeImage; // Image component for the fade effect
    [SerializeField] private float fadeDuration = 0.5f; // Duration for the fade effect
    [SerializeField] private float playerMoveTime = 1.0f;

    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private PauseScreen pauseScreen;

    public event Action onGameStart;

    private void Start()
    {
        canvas.gameObject.SetActive(true);
        // Enable cursor and unlock it
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseScreen.CanPause(false);
        playerController.enabled = false;
        playerController.GetPlayerCameraRoot().GetComponent<PlayerBookComponent>().LockBookVisual(true);
        playerController.GetPlayerCameraRoot().GetComponent<PlayerRaycast>().ShowCrossHair(false);

        playerController.transform.position = transform.position;
        playerController.transform.rotation = transform.rotation;

        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
        startButton.Select();

        fadeImage.gameObject.SetActive(true);
        FadeInStartScreen();
    }

    private void FadeInStartScreen()
    {
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            Color startColor = fadeImage.color;
            Color endColor = new Color(0f, 0f, 0f, 0f); // Fully transparent color

            LeanTween.value(fadeImage.gameObject, startColor, endColor, fadeDuration)
                .setOnUpdate((Color value) =>
                {
                    fadeImage.color = value;
                })
                .setOnComplete(() =>
                {
                    fadeImage.gameObject.SetActive(false);
                });
        }
    }

    public void StartGame()
    {
        canvas.gameObject.SetActive(false);
        pauseScreen.CanPause(true);
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
