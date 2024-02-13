using StarterAssets;
using System;
using System.Collections.Generic;
using UnityEngine;

public class EndOfMap : MonoBehaviour
{

    [SerializeField] private List<EnglishWord> englishWords;
    [SerializeField] private int pageIndex;

    private void OnTriggerEnter(Collider other)
    {
        // Find the book component on the player object
        FirstPersonController player = other.GetComponent<FirstPersonController>();
        PlayerBookComponent bookComponent = player.GetPlayerCameraRoot().GetComponent<PlayerBookComponent>();
        if (bookComponent != null)
        {
            foreach (EnglishWord word in englishWords)
            {
                bookComponent.AddWordToEnglishImages(word, pageIndex);
            }
        }
    }
}
