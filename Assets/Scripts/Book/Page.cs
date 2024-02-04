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
    [SerializeField] private KurdishWord kurdishWordPrefab;


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



    public void AddWordToPage(string word)
    {

        KurdishWord newWordText = Instantiate(kurdishWordPrefab, kurdishWordContainer);

        newWordText.TextMesh.text = word;

        kurdishWords.Add(newWordText);
    }
}
