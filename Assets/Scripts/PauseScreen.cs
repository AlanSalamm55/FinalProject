using StarterAssets;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour
{
    private static PauseScreen instance; // Singleton instance
    [SerializeField] private FirstPersonController playerController;
    [SerializeField] RectTransform canvas;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button restartButton; // New restart button
    [SerializeField] private Image fadeImage; // Image component for the fade effect
    private bool canPause = false;
    private bool isPaused = false;
    private bool isFading = false;
    private float fadeDuration = 1f; // Duration for the fade effect

    [SerializeField] private Image endOfGameimg;
    [SerializeField] private TextMeshProUGUI endOfGameText;

    public static PauseScreen Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PauseScreen>();
                if (instance == null)
                {
                    Debug.LogError("PauseScreen instance not found in the scene.");
                }
            }
            return instance;
        }
    }

    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        canvas.gameObject.SetActive(false);

        endOfGameimg.gameObject.SetActive(false);
        endOfGameText.gameObject.SetActive(false);

        continueButton.onClick.AddListener(TogglePause);
        quitButton.onClick.AddListener(QuitGame);
        restartButton.onClick.AddListener(StartRestartFade); // Attach listener for restart button
        continueButton.Select();
    }

    private void TogglePause()
    {
        if (!canPause) { return; }
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
        playerController.GetPlayerCameraRoot().GetComponent<PlayerRaycast>().TurnOffRaycast();
    }

    private void UnpauseGame()
    {
        canvas.gameObject.SetActive(false);

        playerController.GetPlayerCameraRoot().GetComponent<PlayerBookComponent>().LockBookVisual(false);
        playerController.GetPlayerCameraRoot().GetComponent<PlayerRaycast>().ShowCrossHair(true);
        playerController.GetPlayerCameraRoot().GetComponent<PlayerRaycast>().TurnOnRaycast();

        if (!playerController.GetPlayerCameraRoot().GetComponent<PlayerBookComponent>().IsOpen())
        {
            playerController.enabled = true;
            // Disable cursor and lock it back
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void StartRestartFade()
    {
        continueButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        if (!isFading)
        {
            StartCoroutine(FadeToBlackAndRestart());
        }
    }

    private IEnumerator FadeToBlackAndRestart()
    {
        isFading = true;
        float timer = 0f;
        Color startColor = fadeImage.color;
        Color endColor = new Color(0f, 0f, 0f, 1f); // Fade to black color

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeImage.color = Color.Lerp(startColor, endColor, timer / fadeDuration);
            yield return null;
        }

        // After fading, restart the game
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    public bool IsPaused() { return isPaused; }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void LoseState()
    {
        canvas.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(false);
        // Enable cursor and unlock it
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        playerController.enabled = false;
        playerController.GetPlayerCameraRoot().GetComponent<PlayerBookComponent>().LockBookVisual(true);
        playerController.GetPlayerCameraRoot().GetComponent<PlayerRaycast>().ShowCrossHair(false);
        playerController.GetPlayerCameraRoot().GetComponent<PlayerRaycast>().TurnOffRaycast();

        endOfGameimg.gameObject.SetActive(true);
        endOfGameText.gameObject.SetActive(true);
        canPause = false;
        endOfGameText.text = "Out of tries You Lost";
    }

    public void WinState()
    {
        canvas.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(false);
        // Enable cursor and unlock it
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        playerController.enabled = false;
        playerController.GetPlayerCameraRoot().GetComponent<PlayerBookComponent>().LockBookVisual(true);
        playerController.GetPlayerCameraRoot().GetComponent<PlayerRaycast>().ShowCrossHair(false);
        playerController.GetPlayerCameraRoot().GetComponent<PlayerRaycast>().TurnOffRaycast();

        endOfGameimg.gameObject.SetActive(true);
        endOfGameText.gameObject.SetActive(true);
        canPause = false;
        endOfGameText.text = "you Won";
    }

    public bool CanPause() { return canPause; }
    public void CanPause(bool canPause) { this.canPause = canPause; }

}
