using UnityEngine;

public class HeadlightController:MonoBehaviour
{
    public GameObject headlightObject;
    public float highBeamAngle = 15.0f;
    private Quaternion headlightRotation;
    private bool isHighBeam = false;

    public void HighBeam()
    {
        if (!isHighBeam)
        {
            headlightRotation = headlightObject.transform.rotation;
            headlightObject.transform.rotation = headlightRotation * Quaternion.Euler(highBeamAngle,0,0);
            isHighBeam = true;
        }
    }

    public void LowBeam()
    {
        if (isHighBeam)
        {
            headlightRotation = headlightObject.transform.rotation;
            headlightObject.transform.rotation = headlightRotation * Quaternion.Euler(-highBeamAngle,0,0);
            isHighBeam = false;
        }
    }

    public void ToggleBeamConverter()
    {
        if (isHighBeam)
            LowBeam();
        else if (!isHighBeam)
            HighBeam();
    }
}