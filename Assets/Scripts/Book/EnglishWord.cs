using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnglishWord : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;

    public TextMeshProUGUI TextMesh
    {
        get { return textMesh; }
        set { textMesh = value; }
    }

}
