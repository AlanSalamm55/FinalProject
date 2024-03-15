using UnityEngine;

public class GreenBtn : MonoBehaviour
{
    public Door[] doors; // Array of Door objects
    [SerializeField] private bool[] doorStates; // Serialized array of booleans

    private Renderer buttonRenderer; // Reference to the button's renderer

    void Start()
    {
        buttonRenderer = GetComponent<Renderer>(); // Get the renderer component
        foreach (Door door in doors)
        {
            door.onOpen += HandleDoorOpen;
        }
    }

    void HandleDoorOpen()
    {
        // Check if the arrays have the same length
        if (doors.Length == doorStates.Length)
        {
            bool arraysMatch = true;

            // Check if the states match for each door
            for (int i = 0; i < doors.Length; i++)
            {
                if (doors[i].IsOpen() != doorStates[i])
                {
                    arraysMatch = false;
                    Debug.Log("wrong.");
                    break;
                }
            }

            // Change the button's color if arrays match
            if (arraysMatch)
            {
                // Access the custom material property "baseColor" and set it to green
                buttonRenderer.material.SetColor("_basecolor", Color.green);
                buttonRenderer.material.SetColor("_secondcolor", Color.green);

                Debug.Log("right.");
            }
            else
            {
                // Change the color back to its default color (e.g., white)
                buttonRenderer.material.SetColor("_basecolor", Color.red);
                buttonRenderer.material.SetColor("_secondcolor", Color.red);
            }
        }
        else
        {
            Debug.LogWarning("Arrays have different lengths.");
        }
    }
}
