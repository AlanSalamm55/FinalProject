using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KurdishWord : MonoBehaviour
{
    [SerializeField] private Image image;

    public Image Image
    {
        get { return image; }
        set { image = value; }
    }

}