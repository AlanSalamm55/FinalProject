using UnityEngine;

public class Word : ScriptableObject
{
    //make sprite later maybe
    [SerializeField] private string kurdishWord;
    [SerializeField] private string englishWord;

    // getter for Kurdish word
    public string KurdishWord
    {
        get { return kurdishWord; }
    }

    // getter for Englishword
    public string EnglishWord
    {
        get { return englishWord; }
    }
}
