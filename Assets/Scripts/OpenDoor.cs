using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private EndOfMap endOfMap;
    void Start()
    {
        endOfMap.onConfirmClick += EndOfMap_onConfirmClick;
    }

    private void EndOfMap_onConfirmClick()
    {

        LeanTween.rotate(gameObject, new Vector3(0f, -90f, 0f), 1f)
                 .setEase(LeanTweenType.easeOutQuad); // Adjust the duration and easing as needed


    }


}
