using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fog : MonoBehaviour
{

    [SerializeField] private FirstPersonController controller;
    private Vector3 offset;

    private void Start()
    {
        // Calculate the initial offset between the fog and the player
        offset = transform.position - controller.transform.position;
    }

    private void Update()
    {
        // Update the fog's position to maintain the distance from the player
        transform.position = controller.transform.position + offset;
    }
}

