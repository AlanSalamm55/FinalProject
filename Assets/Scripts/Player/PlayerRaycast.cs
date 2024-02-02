
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;


public class PlayerRaycast : MonoBehaviour
{

    [Header("Raycast")]
    [SerializeField] private FirstPersonController player;
    private PlayerBookComponent bookComp;
    [SerializeField] private float rayLength = 5f;

    [Header("Raycast")]
    private Image crosshair;
    [SerializeField] private Sprite crosshairBase;

    [Header("Input System")]
    [SerializeField] private KeyCode interactKey;


    private Interactable interactable;

    void Start()
    {
        bookComp = this.GetComponent<PlayerBookComponent>();
        crosshair = GetComponentInChildren<Image>();

        if (crosshair != null)
        {
            crosshair.sprite = crosshairBase;
        }


    }

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, rayLength))
        {
            var readableItem = hit.collider.GetComponent<NoteController>();
            if (readableItem != null)
            {
                interactable = readableItem;
                interactable.SetInteractor(player);

                //List<Word> words = interactable.GetWordsInInteractable();

                //foreach (Word word in words)
                //{
                //    bookComp.AddWordToKurdishVocabulary(word.KurdishWord);
                //}

                crosshair.sprite = interactable.GetCrosshairImg();
            }
            else
            {
                ClearInteractable();
            }

        }
        else
        {
            ClearInteractable();
        }
        if (interactable != null)
        {
            if (Input.GetKeyDown(interactKey))
            {
                interactable.ShowInteractable();
            }
        }
    }
    private void ClearInteractable()
    {
        if (interactable != null)
        {
            SetCrossHairImg(crosshairBase);
            interactable = null;
        }
    }

    private void SetCrossHairImg(Sprite img) { crosshair.sprite = img; }



}