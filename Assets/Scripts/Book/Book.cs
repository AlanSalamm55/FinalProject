using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField] private List<Page> pages;
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private GameObject BookVisual;
    [SerializeField] private GameObject closeText;

    private bool navigationEnabled = true; // Boolean to control navigation with arrow keys

    private int currentIndex = 0;

    private void Start()
    {
        BookVisual.SetActive(false);
        InitialState();
    }

    private void Update()
    {
        // Check for keyboard input if navigation is enabled
        if (navigationEnabled)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ForwardButtonActions();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                BackButtonActions();
            }
        }
    }

    // Setter for navigationEnabled
    public void SetNavigationEnabled(bool enabled)
    {
        navigationEnabled = enabled;
    }

    public void ShowBookVisual(bool show)
    {
        BookVisual.SetActive(show);
    }

    public void AddWordToNoteBookVisual(KurdishWord word, int pageIndex)
    {
        if (pageIndex >= 0 && pageIndex < pages.Count)
        {
            pages[pageIndex].AddKurdishWordToPage(word);
        }
        else
        {
            Debug.Log("Invalid pageIndex: " + pageIndex);
        }
    }

    public void AddImageToNoteBookVisual(EnglishWord word, int pageIndex)
    {
        if (pageIndex >= 0 && pageIndex < pages.Count)
        {
            pages[pageIndex].AddIllustrationToPage(word);
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
            pages[i].gameObject.SetActive(false); // Initially hide all pages
        }
        currentIndex = 0; // Reset index to the first page
        ShowPageAtIndex(currentIndex);
        backButton.SetActive(false);
        nextButton.SetActive(pages.Count > 1); // Ensure next button is active only if there are more than one page
    }

    public void ForwardButtonActions()
    {
        int nextPageIndex = currentIndex + 1;
        if (nextPageIndex < pages.Count)
        {
            // Hide current page
            pages[currentIndex].gameObject.SetActive(false);
            // Update current index to the next page
            currentIndex = nextPageIndex;
            // Show the next page
            pages[currentIndex].gameObject.SetActive(true);
            // Update button visibility
            backButton.SetActive(true);
            nextButton.SetActive(currentIndex < pages.Count - 1);
        }
    }

    public void BackButtonActions()
    {
        int previousPageIndex = currentIndex - 1;
        if (previousPageIndex >= 0)
        {
            // Hide current page
            pages[currentIndex].gameObject.SetActive(false);
            // Update current index to the previous page
            currentIndex = previousPageIndex;
            // Show the previous page
            pages[currentIndex].gameObject.SetActive(true);
            // Update button visibility
            backButton.SetActive(currentIndex > 0);
            nextButton.SetActive(true);
        }
    }

    public void ShowPageAtIndex(int pageIndex)
    {
        if (pageIndex >= 0 && pageIndex < pages.Count)
        {
            // Hide current page
            pages[currentIndex].gameObject.SetActive(false);
            // Update current index to the target page
            currentIndex = pageIndex;
            // Show the target page
            pages[currentIndex].gameObject.SetActive(true);
            // Update button visibility
            backButton.SetActive(currentIndex > 0);
            nextButton.SetActive(currentIndex < pages.Count - 1);
        }
        else
        {
            Debug.Log("Invalid pageIndex: " + pageIndex);
        }
    }
    public void HideButtons(bool show)
    {
        backButton.SetActive(!show);
        nextButton.SetActive(!show);
    }

    public Page GetPageByIndex(int index) { return pages[index]; }

    public bool AreWordCountsEqual(int index)
    {
        return pages[index].KurdishWords.Count == pages[index].EnglishWords.Count;
    }

    public void HideCloseText(bool show) { closeText.gameObject.SetActive(!show); }
}
