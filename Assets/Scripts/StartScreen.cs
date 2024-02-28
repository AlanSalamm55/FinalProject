using StarterAssets;
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

    private void Start()
    {
        canvas.gameObject.SetActive(true);

        // Enable cursor and unlock it
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        playerController.enabled = false;

        playerController.transform.position = transform.position;
        playerController.transform.rotation = transform.rotation;

        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
        startButton.Select();
    }

    public void StartGame()
    {
        canvas.gameObject.SetActive(false);

        // Tween player movement from current position to start position
        LeanTween.move(playerController.gameObject, startPosition.position, playerMoveTime).setEaseInOutQuad().setOnComplete(() =>
        {
            // Hide cursor and lock it
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            playerController.enabled = true;
        });
        LeanTween.rotate(playerController.gameObject, startPosition.eulerAngles, playerMoveTime).setEaseInOutQuad();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
