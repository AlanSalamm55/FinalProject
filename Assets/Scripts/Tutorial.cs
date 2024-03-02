using System.Collections;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Sprite wsadsprite;
    [SerializeField] private StartScreen startScreen;

    // Start is called before the first frame update
    void Start()
    {
        startScreen.onGameStart += StartScreen_onGameStart;
    }

    private void StartScreen_onGameStart()
    {
        StartCoroutine(ShowTutorialAndFunctionAfterDelay());
    }

    private IEnumerator ShowTutorialAndFunctionAfterDelay()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds

        // Show the image and text after the delay
        PopUpText.Instance.ShowImage(wsadsprite);
        PopUpText.Instance.ShowText("Use WASD to move");

        yield return new WaitForSeconds(5f); // Wait for additional 5 seconds

        // Call another function after the additional delay
        AnotherFunction();
    }

    private void AnotherFunction()
    {
        // Your code for the function to be called after the delay
        Debug.Log("Another function called after 5 seconds.");
    }
}
