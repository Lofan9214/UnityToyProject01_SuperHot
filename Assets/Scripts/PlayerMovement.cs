using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpAccelation = 5f;
    public Transform cameraTransform;

    private PlayerInput input;
    private Rigidbody rb;

    public bool isGrounded { get; private set; }

    public float sqrCurrentSpeed
    {
        get
        {
            return rb.velocity.sqrMagnitude;
        }
    }

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.MoveRotation(Quaternion.Euler(0f, input.RotateHorizontal, 0f));

        if (isGrounded)
        {
            var velocity = rb.velocity;
            velocity.z = Mathf.Cos(Mathf.Deg2Rad * input.RotateHorizontal) * input.Direction.y * moveSpeed;
            velocity.z -= Mathf.Sin(Mathf.Deg2Rad * input.RotateHorizontal) * input.Direction.x * moveSpeed;
            velocity.x = Mathf.Sin(Mathf.Deg2Rad * input.RotateHorizontal) * input.Direction.y * moveSpeed;
            velocity.x += Mathf.Cos(Mathf.Deg2Rad * input.RotateHorizontal) * input.Direction.x * moveSpeed;
            rb.velocity = velocity;

            if (input.Jump)
            {
                isGrounded = false;
                rb.AddForce(0f, jumpAccelation, 0f, ForceMode.Impulse);
            }
        }
    }

    private void Update()
    {
        cameraTransform.rotation = rb.rotation * Quaternion.Euler(-input.RotateVertical, 0f, 0f);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.contacts[0].normal.y > 0f)
        {
            isGrounded = true;
        }
    }
}
