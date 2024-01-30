
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;


public class PlayerRaycast : MonoBehaviour
{

    [Header("Raycast")]
    [SerializeField] private FirstPersonController player;
    [SerializeField] private float rayLength = 5f;

    [Header("Raycast")]
    private Image crosshair;
    [SerializeField] private Sprite crosshairBase;

    [Header("Input System")]
    [SerializeField] private KeyCode interactKey;


    //TODO: make an interface called Interactable. in it everything the player can interact with implements said interface
    // Notecontroller is one of them but we will also have pickup objects and inspect objects they all inheret from this interface
    private NoteController noteController;

    void Start()
    {
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
                noteController = readableItem;
                noteController.SetInteractor(player);
                crosshair.sprite = noteController.GetCrosshairImg();
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
        if (noteController != null)
        {
            if (Input.GetKeyDown(interactKey))
            {
                //later in the code u use the interactable interface and call the interact function
                //instead of this shownote the interactable will have a interact function that they all implement and that will be called here 
                //another important thing is u can change the camera position and lock it with the cameraBase transfroms for certain puzzles 
                //that too will be a interactable type u press "E" and it puts the camera somewhere and locks it 
                noteController.ShowNote();
            }
        }
    }
    private void ClearInteractable()
    {
        if (noteController != null)
        {
            SetCrossHairImg(crosshairBase);
            noteController = null;
        }
    }
    private void SetCrossHairImg(Sprite img) { crosshair.sprite = img; }



}