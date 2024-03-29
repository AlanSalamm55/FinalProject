using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface Interactable
{
    public bool IsOpenedOnce();
    public void ShowInteractable();
    public void SetInteractor(FirstPersonController player);
    public List<Word> GetWordsInInteractable();
    public Sprite GetCrosshairImg();
    public int GetPageIndex();

    public bool IsOpen();

    public void Close();

}
