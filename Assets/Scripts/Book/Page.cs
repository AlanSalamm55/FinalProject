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
        newWordText.transform.Rotate(0, 180, 0);
        newWordText.ShowGuessText();

        kurdishWords.Add(newWordText);

    }

    public void AddIllustrationToPage(EnglishWord word)
    {

        EnglishWord newWordText = Instantiate(word, illustrationContainer);
        newWordText.transform.Rotate(0, 180, 0);
        englishWords.Add(newWordText);
    }
}
