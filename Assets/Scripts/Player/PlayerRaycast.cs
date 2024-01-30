
using UnityEngine;
using UnityEngine.UI;


public class PlayerRaycast : MonoBehaviour
{
    [Header("Raycast")]
    [SerializeField] private float rayLength = 5f;
    [SerializeField] Transform cameraBase;
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
        else
        {
            Debug.LogError("Image component not found on the GameObject or its children.");
        }


    }

    void Update()
    {
        if (Physics.Raycast(cameraBase.transform.position, cameraBase.transform.TransformDirection(Vector3.forward), out RaycastHit hit, rayLength))
        {
            Debug.Log("raycast did happen");

            var readableItem = hit.collider.GetComponent<NoteController>();
            if (readableItem != null)
            {
                noteController = readableItem;
                Debug.Log("shhirr");
                crosshair.sprite = noteController.GetCrosshairImg();
            }
            else
            {
                ClearNote();
            }

        }
        else
        {
            ClearNote();
        }
        if (noteController != null)
        {
            if (Input.GetKeyDown(interactKey))
            {
                //later in the code u use the interactable interface and call the interact function
                //instead of this shownote the interactable will have a interact function that they all implement and that will be called here 
                noteController.ShowNote();
            }
        }
    }
    private void ClearNote()
    {
        if (noteController != null)
        {
            SetCrossHairImg(crosshairBase);
            noteController = null;
        }
    }
    private void SetCrossHairImg(Sprite img) { crosshair.sprite = img; }



}