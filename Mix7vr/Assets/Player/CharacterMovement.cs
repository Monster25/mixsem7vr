using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float lookSpeed = 8.0f;
    public float pushPower = 2.0f;
    private PlayerCamera playerCamera;
    private Transform playerCameraTransform, playerTransform;
    private float lookLeftRightValue, lookUpDownValue, rotX, rotY;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    private int jumps;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerTransform = GetComponent<Transform>();
        playerCamera= GetComponent<Transform>().GetChild(0).GetComponent<PlayerCamera>();
        playerCameraTransform = GetComponent<Transform>().GetChild(0).GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

        //Push objects around
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        //No rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        //No pushing objects underneath us
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        //Calculate push direciton from move direction
        Vector3 pushDir = hit.moveDirection;

        //Apply push
        body.velocity = pushDir * pushPower * speed;
    }
    // Update is called once per frame
    void Update()
    {
        //Look
        lookLeftRightValue = Input.GetAxis("Mouse X");
        lookUpDownValue = Input.GetAxis("Mouse Y");
        //Calculate Player ROtation
        rotX += lookLeftRightValue*lookSpeed;
        //Calculate camera rotation
        rotY += lookUpDownValue *lookSpeed;
        //Clamp Y
        rotY = Mathf.Clamp(rotY, -90f, 90f);

        //Rotate camera
        playerCameraTransform.localRotation = Quaternion.Euler(rotY*-1.0f,0f,0f);
        //Rotate player
        playerTransform.rotation = Quaternion.Euler(0f, rotX, 0f);

        //Movement
        if (controller.isGrounded)
        {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        //Change jump button to whatever
            if (Input.GetButtonDown("Fire1"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        //Moving during jump
        else
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), moveDirection.y, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection.x *= speed;
            moveDirection.z *= speed;
        }
        //Apply gravity and movement to controller
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
