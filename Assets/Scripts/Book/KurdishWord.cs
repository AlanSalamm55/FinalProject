using TMPro;
using UnityEngine;

public class KurdishWord : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;

    public TextMeshProUGUI TextMesh
    {
        get { return textMesh; }
        set { textMesh = value; }
    }

}