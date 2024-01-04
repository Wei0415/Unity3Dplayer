using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Camera c;
    private float mouseScroll;
    private int maxView = 70;
    private int minView = 10;
    private float slideSpeed = 50f;

    private void Start()
    {
        c = GetComponent<Camera>();
    }

    private void Update()
    {
        mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        if (mouseScroll < 0)
        {
            if (c.fieldOfView < maxView)
            {
                c.fieldOfView += Mathf.Abs(mouseScroll) * slideSpeed;
            }
        }
        else if (mouseScroll > 0)
        {
            if (c.fieldOfView > minView)
            {
                c.fieldOfView -= Mathf.Abs(mouseScroll) * slideSpeed;
            }
        }
        c.fieldOfView = Mathf.Clamp(c.fieldOfView, minView, maxView);
    }
}
