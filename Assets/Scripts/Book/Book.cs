
using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField] private GameObject BookVisual;
    private void Start()
    {
        BookVisual.SetActive(false);
    }
    // Method to show the book visual
    public void ShowBookVisual(bool show)
    {

        BookVisual.SetActive(show);

    }


}
