using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Page : MonoBehaviour
{
    private List<TextMeshProUGUI> kurdishWords = new List<TextMeshProUGUI>();
    [SerializeField] private List<TextMeshProUGUI> englishWords;
    [SerializeField] private List<Image> wordIllustrations;
    [SerializeField] private RectTransform kurdishWordContainer;
    [SerializeField] private TextMeshProUGUI TextPrefab;


    public List<TextMeshProUGUI> KurdishWords
    {
        get { return kurdishWords; }
        private set { kurdishWords = value; }
    }

    public void AddWordToPage(string word)
    {

        TextMeshProUGUI newWordText = Instantiate(TextPrefab, kurdishWordContainer);

        newWordText.text = word;

        kurdishWords.Add(newWordText);
    }
}
