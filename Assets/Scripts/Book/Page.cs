using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Page : MonoBehaviour
{
    private List<KurdishWord> kurdishWords = new List<KurdishWord>();
    [SerializeField] private List<EnglishWord> englishWords;
    [SerializeField] private List<Image> wordIllustrations;
    [SerializeField] private RectTransform kurdishWordContainer;


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



    public void AddWordToPage(KurdishWord word)
    {

        KurdishWord newWordText = Instantiate(word, kurdishWordContainer);

        kurdishWords.Add(newWordText);
    }
}
