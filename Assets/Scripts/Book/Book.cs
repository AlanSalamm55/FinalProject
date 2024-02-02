
using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField] private GameObject BookVisual;
    private void Start()
    {
        BookVisual.SetActive(false);
    }

    public void ShowBookVisual(bool show)
    {

        BookVisual.SetActive(show);

    }


}
