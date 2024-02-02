using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface Interactable
{
    public void ShowInteractable();
    public void SetInteractor(FirstPersonController player);
    public List<Word> GetWordsInInteractable();
    public Sprite GetCrosshairImg();
}
