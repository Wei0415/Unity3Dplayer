using UnityEngine;
using System.Collections;


public class TPCharacterMove : MonoBehaviour
{
    float baseSpeed = 2f;
    float runSpeed = 3.5f;
    float turnSpeed = 10f;
    float jumpForce = 4f;
    bool isGround = true;
    bool isWalking = false;
    Rigidbody rb;
    Animator animator;
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
        HandleMovementInput();
    }

    void HandleMovementInput()
    {
        // 控制角色的水平旋轉
        float horizontalInput = Input.GetAxis("Horizontal");
        horizontalRotation += horizontalInput * turnSpeed;
        transform.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : baseSpeed;

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

    public void SetHorizontalRotation(float rotation)
    {
        horizontalRotation = rotation;
    }
}
