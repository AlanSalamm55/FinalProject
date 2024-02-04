using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    private int index = -1;
    private bool rotate = false;

    [SerializeField] private float pageSpeed = 0.5f;
    [SerializeField] private List<Page> pages;
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject nextButton;

    [SerializeField] private GameObject BookVisual;

    private void Start()
    {
        BookVisual.SetActive(false);

        InitialState();
    }

    public void ShowBookVisual(bool show)
    {

        BookVisual.SetActive(show);

    }

    public void AddWordToNoteBookVisual(KurdishWord word, int pageIndex)
    {
        if (pageIndex >= 0 && pageIndex < pages.Count)
        {
            pages[pageIndex].AddWordToPage(word);
        }
        else
        {
            Debug.Log("Invalid pageIndex: " + pageIndex);
        }
    }


    public void InitialState()
    {
        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].transform.rotation = Quaternion.identity;
        }
        pages[0].transform.SetAsLastSibling();
        backButton.SetActive(false);

    }

    public void RotateForward()
    {
        if (rotate == true) { return; }
        index++;
        float angle = 180; //in order to rotate the page forward, you need to set the rotation by 180 degrees around the y axis
        ForwardButtonActions();
        pages[index].transform.SetAsLastSibling();
        StartCoroutine(Rotate(angle, true));

    }

    public void ForwardButtonActions()
    {
        if (backButton.activeInHierarchy == false)
        {
            backButton.SetActive(true); //every time we turn the page forward, the back button should be activated
        }
        if (index == pages.Count - 1)
        {
            nextButton.SetActive(false); //if the page is last then we turn off the forward button
        }
    }

    public void RotateBack()
    {
        if (rotate == true) { return; }
        float angle = 0; //in order to rotate the page back, you need to set the rotation to 0 degrees around the y axis
        pages[index].transform.SetAsLastSibling();
        BackButtonActions();
        StartCoroutine(Rotate(angle, false));
    }

    public void BackButtonActions()
    {
        if (nextButton.activeInHierarchy == false)
        {
            nextButton.SetActive(true); //every time we turn the page back, the forward button should be activated
        }
        if (index - 1 == -1)
        {
            backButton.SetActive(false); //if the page is first then we turn off the back button
        }
    }

    IEnumerator Rotate(float angle, bool forward)
    {
        float value = 0f;
        while (true)
        {
            rotate = true;
            Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
            value += Time.deltaTime * pageSpeed;
            pages[index].transform.rotation = Quaternion.Slerp(pages[index].transform.rotation, targetRotation, value); //smoothly turn the page
            float angle1 = Quaternion.Angle(pages[index].transform.rotation, targetRotation); //calculate the angle between the given angle of rotation and the current angle of rotation
            if (angle1 < 0.1f)
            {
                if (forward == false)
                {
                    index--;
                }
                rotate = false;
                break;

            }
            yield return null;

        }
    }



}
