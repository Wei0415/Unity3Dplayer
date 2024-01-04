using UnityEngine;

public class CompassController : MonoBehaviour
{
    public Transform target; // 你的目標物體，可以是玩家或其他目標
    public RectTransform needle; // 指針的 RectTransform
    public Camera uiCamera; // UI 的攝影機，可以在 Canvas 的 Render Camera 中設置

    void Update()
    {
        // 如果有目標物體
        if (target != null)
        {
            // 獲取目標物體在 2D 畫面中的位置
            Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(uiCamera, target.position);

            // 計算指針的旋轉角度
            Vector3 dir = screenPos - needle.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            // 旋轉指針
            needle.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}
