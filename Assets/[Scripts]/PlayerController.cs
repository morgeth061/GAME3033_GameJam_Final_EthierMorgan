using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public GameObject gameController;

    private PlayerController playerController;
    Rigidbody rigidbody;
    Animator playerAnimator;

    //references
    Vector2 inputVector = Vector2.zero;
    Vector3 moveDirection = Vector3.zero;

    public float currentSpeed;

    public readonly int movementHash = Animator.StringToHash("IsMoving");

    void Awake()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerAnimator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        
    }
    public void OnMovement(InputValue value)
    {
        //position
        if (!gameController.GetComponent<GameController>().pauseVisible && gameController.GetComponent<GameController>().gameStarted)
        {
            inputVector = value.Get<Vector2>();

            if (inputVector.x != 0 || inputVector.y != 0)
            {
                playerAnimator.SetBool(movementHash, true);
            }
            else
            {
                playerAnimator.SetBool(movementHash, false);
            }

            float heading = Mathf.Atan2(inputVector.x, inputVector.y) * Mathf.Rad2Deg;

            if (inputVector.x != 0 || inputVector.y != 0)
            {
               transform.GetChild(0).rotation = Quaternion.Euler(0, heading, 0);

            }
        }


    }

    public void OnPause(InputValue value)
    {
        gameController.GetComponent<GameController>().Pause();
    }

    void Update()
    {
        if (!(inputVector.magnitude > 0)) moveDirection = Vector3.zero;

        moveDirection = transform.forward * inputVector.y + transform.right * inputVector.x;

        Vector3 movementDirection = moveDirection * (currentSpeed * Time.deltaTime);

        transform.position += movementDirection;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            gameController.GetComponent<GameController>().PickupCollected();
        }
    }
}
