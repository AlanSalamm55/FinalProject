using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinState : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PauseScreen.Instance.WinState();
    }
}
