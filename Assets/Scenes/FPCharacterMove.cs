using UnityEngine;
using System.Collections;


public class FPCharacterMove : MonoBehaviour
{
    float speed = 2f;
    float runSpeed = 3.5f;
    float turnSpeed = 2.5f;  
    float jumpForce = 4f;
    bool isGround = true;
    bool isWalking = false;
    Rigidbody rb;
    Animator animator;

    private float verticalRotation = 0f;
    private float horizontalRotation = 0f;

    void Start()
{
    rb = GetComponent<Rigidbody>();
    rb.interpolation = RigidbodyInterpolation.Interpolate;
    animator = GetComponent<Animator>();
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
}


    void Update()
    {
        HandleMouseLook();
        HandleMovementInput();
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * turnSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * turnSpeed;

        horizontalRotation += mouseX;
        verticalRotation -= mouseY;
        
        transform.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    void HandleMovementInput()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : 2f;

        // 將輸入方向轉換為相對於角色的方向
        Vector3 relativeDirection = transform.TransformDirection(direction);
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && !Mathf.Approximately(relativeDirection.magnitude, 0f))
        {
            animator.SetTrigger("runTrigger");
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Mathf.Approximately(relativeDirection.magnitude, 0f))
        {
            animator.ResetTrigger("runTrigger");

            if (!Mathf.Approximately(relativeDirection.magnitude, 0f))
            {
                isWalking = true;
                animator.SetTrigger("walkTrigger");
            }
            else
            {
                isWalking = false;
                animator.SetTrigger("idleTrigger");
            }
        }

        Vector3 velocity = relativeDirection.normalized * Time.deltaTime * speed;
        rb.MovePosition(rb.position + velocity);

        if (!Mathf.Approximately(relativeDirection.magnitude, 0f))
        {
            if (!isWalking)
            {
                isWalking = true;
                animator.SetTrigger("walkTrigger");
                animator.ResetTrigger("idleTrigger");
                animator.ResetTrigger("runTrigger");
            }
        }
        else
        {
            if (isWalking)
            {
                isWalking = false;
                animator.SetTrigger("idleTrigger");
                animator.ResetTrigger("walkTrigger");
                animator.ResetTrigger("runTrigger");
            }
        }

        if (isGround && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = true;
        }
    }
}
