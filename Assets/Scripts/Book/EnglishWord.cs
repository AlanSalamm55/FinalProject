using UnityEngine;
using UnityEngine.UI;

public class EnglishWord : MonoBehaviour
{
    [SerializeField] private Image illustration;
    [SerializeField] private string rightAnswer;

    public Image Illustration
    {
        get { return illustration; }

    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the KurdishWord script attached
        KurdishWord kurdishWord = other.GetComponent<KurdishWord>();

        if (kurdishWord != null)
        {
            // Set Kurdish word as a child of this English word
            kurdishWord.transform.parent = transform;
        }
    }

    public string RightAnswer() { return rightAnswer; }


}
