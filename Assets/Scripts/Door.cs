using StarterAssets;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, Interactable
{
    [SerializeField] private Sprite crosshairImg;
    private FirstPersonController player;
    public event Action onOpen;

    private bool isOpen = false;
    private float openAngle = -90f; // Angle to open the door (in degrees)
    private float closeAngle = 0f; // Angle to close the door (in degrees)
    private float duration = 1f; // Duration of the door animation

    public void Close()
    {

        LeanTween.rotateY(gameObject, closeAngle, 1f).setEase(LeanTweenType.easeOutQuad);
        isOpen = false;
        onOpen?.Invoke();

    }

    public Sprite GetCrosshairImg()
    {
        return crosshairImg;
    }

    public int GetPageIndex()
    {
        throw new NotImplementedException();
    }

    public List<Word> GetWordsInInteractable()
    {
        throw new NotImplementedException();
    }

    public bool IsOpen()
    {
        return isOpen;
    }

    public bool IsOpenedOnce()
    {
        return true;
    }

    public void SetInteractor(FirstPersonController player)
    {
        this.player = player;
    }

    public void ShowInteractable()
    {

        LeanTween.rotateY(gameObject, openAngle, 1f).setEase(LeanTweenType.easeOutQuad);
        isOpen = true;
        onOpen?.Invoke();


    }

}
