using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public enum RotationAxis
    {
        X,
        Y,
        Z
    }

    [SerializeField] private RotationAxis rotationAxis = RotationAxis.Y;
    [SerializeField] private int closeAngle = 0;
    [SerializeField] private int openAngle = -90;
    [SerializeField] private EndOfMap endOfMap;

    private void Start()
    {
        endOfMap.onConfirmClick += EndOfMap_onConfirmClick;
    }

    private void EndOfMap_onConfirmClick()
    {
        Vector3 targetRotation = Vector3.zero;

        // Set the target rotation based on the selected axis
        switch (rotationAxis)
        {
            case RotationAxis.X:
                targetRotation = new Vector3(openAngle, transform.eulerAngles.y, transform.eulerAngles.z);
                break;
            case RotationAxis.Y:
                targetRotation = new Vector3(transform.eulerAngles.x, openAngle, transform.eulerAngles.z);
                break;
            case RotationAxis.Z:
                targetRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, openAngle);
                break;
        }

        LeanTween.rotate(gameObject, targetRotation, 1f)
                 .setEase(LeanTweenType.easeOutQuad); // Adjust the duration and easing as needed
    }
}
