using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public float movementSpeed = 5.0f;
    public float rotationSpeed = 0.5f;

    public float vertical = 0f;
    public float horizontal = 0f;

    public float currentHeight = 0f;
    public float jumpHeight = 5f;
    public float runningSpeed = 5.0f;

    public float sensitivity = 1f;
    public float verticalRange = 360.0f;
    public float mouseVertical = 0.0f;
    public float mouseHorizontal = 0.0f;

    public InGameMenu inGameMenu;


    void Start()
    {
        CharacterController controller = GetComponent<CharacterController>();
        inGameMenu = FindObjectOfType<InGameMenu>();
    }

    void Update()
    {
        if (inGameMenu.paused == false)
        { 
        KeyboardController();
        MouseController();
        }
    }

    void KeyboardController()
    {
        vertical = Input.GetAxis("Vertical") * movementSpeed;
        horizontal = Input.GetAxis("Horizontal") * movementSpeed;

        if (Input.GetButton("Jump") && characterController.isGrounded)
        {
            currentHeight = jumpHeight;
        }
        else if (!characterController.isGrounded)
        {
            currentHeight += Physics.gravity.y * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            movementSpeed += runningSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            movementSpeed -= runningSpeed;
        }

        Vector3 move = new Vector3(horizontal, currentHeight, vertical);
        move = transform.rotation * move;
        characterController.Move(move * Time.deltaTime);
    }

    void MouseController()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        transform.Rotate(0, mouseX, 0);

        mouseVertical -= mouseY;
        mouseVertical = Mathf.Clamp(mouseVertical, -verticalRange, verticalRange);

        Camera.main.transform.localRotation = Quaternion.Euler(mouseVertical, 0, 0);
    }

    public float pushPower = 2.0f;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic)
        {
            return;
        }

        if (hit.moveDirection.y < -0.3f)
        {
            return;
        }

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPower;
    }
}
