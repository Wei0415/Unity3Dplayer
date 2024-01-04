using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public GameObject firstPersonCamera;
    public GameObject thirdPersonCamera;
    public TPCharacterMove thirdPersonCharacterMove; // 引用 TPCharacterMove 腳本
    public FPCharacterMove firstPersonCharacterMove;

    void Start()
    {
        // 初始化時啟用第三人稱相機，禁用第一人稱相機
        firstPersonCamera.SetActive(false);
        thirdPersonCamera.SetActive(true);

        // 啟用第三人稱角色腳本，禁用第一人稱角色腳本
        if (thirdPersonCharacterMove != null)
        {
            thirdPersonCharacterMove.enabled = true;
        }

        if (firstPersonCharacterMove != null)
        {
            firstPersonCharacterMove.enabled = false;
        }
    }

    void Update()
    {
        // 檢測切換按鈕，例如 Tab 鍵
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchCamera();
        }
    }

    void SwitchCamera()
    {
        // 切換相機的啟用狀態
        firstPersonCamera.SetActive(!firstPersonCamera.activeSelf);
        thirdPersonCamera.SetActive(!thirdPersonCamera.activeSelf);

        // 切換角色腳本的啟用狀態
        if (thirdPersonCharacterMove != null)
        {
            thirdPersonCharacterMove.enabled = !thirdPersonCharacterMove.enabled;
        }

        if (firstPersonCharacterMove != null)
        {
            firstPersonCharacterMove.enabled = !firstPersonCharacterMove.enabled;
        }
    }
}
