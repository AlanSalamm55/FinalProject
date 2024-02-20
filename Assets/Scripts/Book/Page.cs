using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Page : MonoBehaviour
{
    private List<KurdishWord> kurdishWords = new List<KurdishWord>();
    private List<EnglishWord> englishWords = new List<EnglishWord>();
    [SerializeField] private List<Image> wordIllustrations;
    [SerializeField] private RectTransform kurdishWordContainer;
    [SerializeField] private RectTransform illustrationContainer;


    public List<KurdishWord> KurdishWords
    {
        get { return kurdishWords; }
        private set { kurdishWords = value; }
    }
    public List<EnglishWord> EnglishWords
    {
        get { return englishWords; }
        private set { englishWords = value; }
    }



    public void AddKurdishWordToPage(KurdishWord word)
    {

        KurdishWord newWordText = Instantiate(word, kurdishWordContainer);
        newWordText.ShowGuessText();

        kurdishWords.Add(newWordText);
        // Somewhere in your code where you have access to the KurdishWord instance
        newWordText.DragEnded += RefreshGridLayout;

    }

    public void AddIllustrationToPage(EnglishWord word)
    {

        EnglishWord newWordText = Instantiate(word, illustrationContainer);
        englishWords.Add(newWordText);
    }

    // Method to refresh the grid layout after drag
    public void RefreshGridLayout()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(kurdishWordContainer);
        LayoutRebuilder.ForceRebuildLayoutImmediate(illustrationContainer);
    }

    public void OnConfirmButtonClicked()
    {
        // Check all English words for valid Kurdish translations
        foreach (KurdishWord word in kurdishWords)
        {
            bool isValid = word.IsAnswerValid();
            if (isValid)
                Debug.Log("English Word: " + word.RightAnswer() + ", Kurdish Word Valid: " + isValid);
        }
    }

}

