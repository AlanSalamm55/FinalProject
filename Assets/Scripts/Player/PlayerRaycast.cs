
using UnityEngine;
using UnityEngine.UI;


public class PlayerRaycast : MonoBehaviour
{
    [Header("Raycast")]
    [SerializeField] private float rayLenght = 5f;
    private Camera cam;

    [Header("Raycast")]
    [SerializeField] private Image corsshair;


    //TODO: make an interface called Interactable. in it everything the player can interact with implements said interface
    // Notecontroller is one of them but we will also have pickup objects and inspect objects they all inheret from this interface

    // private NoteController noteController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
