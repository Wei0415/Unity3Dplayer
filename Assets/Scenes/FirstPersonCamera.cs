using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float sensitivity = 1.5f; // 滑鼠靈敏度
    public float zoomSensitivity = 0.5f; // 變焦時的滑鼠靈敏度
    public float zoomFOV = 30f; // 變焦後的視野
    public float verticalRotationLimit = 80f; // 垂直旋轉的限制角度

    private float verticalRotation = 0f;

    void Update()
    {
        // 獲取滑鼠移動
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // 如果按下 Z 鍵，使用變焦時的滑鼠靈敏度和視野
        if (Input.GetKey(KeyCode.Z))
        {
            mouseX *= zoomSensitivity;
            mouseY *= zoomSensitivity;
            Camera.main.fieldOfView = zoomFOV;
        }
        else
        {
            // 恢復正常視野
            Camera.main.fieldOfView = 60f;
        }

        // 旋轉水平方向
        transform.Rotate(Vector3.up * mouseX);

        // 旋轉垂直方向
        verticalRotation -= mouseY;
        // 限制垂直旋轉在指定範圍內
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);

        // 將垂直旋轉應用到視角
        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}
