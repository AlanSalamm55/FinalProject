using StarterAssets;
using System.Collections;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Sprite wsadsprite;
    [SerializeField] private Sprite TabSprite;

    [SerializeField] private Sprite NoteIcon;
    [SerializeField] private Sprite BellIcon;
    [SerializeField] private StartScreen startScreen;

    // Reference to the note object in the scene
    [SerializeField] private NoteController noteObject;
    [SerializeField] private EndOfMap bellObject;


    // Offset to adjust the position of the note sprite above the note object
    private Vector3 noteSpriteOffset = new Vector3(0, 0, -0.4f);
    private Vector3 bellSpriteOffset = new Vector3(0, 0.7f, 0);

    private GameObject noteSpriteObject;
    private GameObject bellSpriteObject;

    private Transform playerTransform;
    [SerializeField] PlayerBookComponent playerBookComponent;
    private bool isFirstTime = true;

    // Start is called before the first frame update
    void Start()
    {
        noteObject.onClosedEvent += NoteObject_onClosed;
        playerBookComponent.onBookOpen += WriteUrGuess;
        startScreen.onGameStart += StartScreen_onGameStart;
        playerTransform = FindObjectOfType<FirstPersonController>().transform; // Assuming FirstPersonController script exists
    }

    private void NoteObject_onClosed()
    {
        Invoke("NoteObject_onClosedDelay", 2f);
    }

    private void NoteObject_onClosedDelay()
    {
        PopUpText.Instance.ShowImage(TabSprite);
        PopUpText.Instance.ShowText("Use Tab to open book");
    }
    private void WriteUrGuess()
    {
        if (isFirstTime)
        {
            isFirstTime = !isFirstTime;
            Invoke("WriteUrGuessDelay", 3f);
        }
    }
    private void WriteUrGuessDelay()
    {
        PopUpText.Instance.ShowText("Write ur guess under the text");
        Invoke("MoveBellAnswer", 7f);
    }
    private void MoveBellAnswer()
    {
        Vector3 bellSpritePosition = bellObject.transform.position + bellSpriteOffset;

        bellSpriteObject = new GameObject("BellSprite");
        bellSpriteObject.transform.position = bellSpritePosition;

        SpriteRenderer spriteRenderer = bellSpriteObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = BellIcon;
        spriteRenderer.sortingOrder = 1; // Adjust as necessary

        // Halve the scale
        bellSpriteObject.transform.localScale *= 0.3f;

        // Show text
        PopUpText.Instance.ShowText("Move to the bell");
    }


    private void StartScreen_onGameStart()
    {
        StartCoroutine(ShowTutorialAndFunctionAfterDelay());
    }

    private IEnumerator ShowTutorialAndFunctionAfterDelay()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        PopUpText.Instance.ShowImage(wsadsprite);
        PopUpText.Instance.ShowText("Use WASD to move");

        yield return new WaitForSeconds(5f); // Wait for additional 5 seconds

        MoveToNote();
    }

    private void MoveToNote()
    {
        Vector3 noteSpritePosition = noteObject.transform.position + noteSpriteOffset;

        noteSpriteObject = new GameObject("NoteSprite");
        noteSpriteObject.transform.position = noteSpritePosition;

        SpriteRenderer spriteRenderer = noteSpriteObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = NoteIcon;
        spriteRenderer.sortingOrder = 1; // Adjust as necessary

        // Show text
        PopUpText.Instance.ShowText("Move to the note");
    }

    void Update()
    {
        if (noteSpriteObject != null && playerTransform != null)
        {
            noteSpriteObject.transform.LookAt(playerTransform);
        }
        if (bellSpriteObject != null && playerTransform != null)
        {
            bellSpriteObject.transform.LookAt(playerTransform);
        }
    }
}
