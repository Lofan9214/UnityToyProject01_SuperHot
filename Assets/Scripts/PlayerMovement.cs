using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;
    public float jumpAccelation = 5f;
    public Transform cameraTransform;

    private PlayerInput input;
    private Rigidbody rb;

    private float rotationH;
    private float rotationV;

    public bool Grounded { get; private set; }

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

        rotationH = 0f;
        rotationV = 0f;
    }

    private void FixedUpdate()
    {
        var rot = transform.rotation.eulerAngles.y;

        rotationH = rotationH + input.RotateHorizontal * rotationSpeed * Time.fixedUnscaledDeltaTime;
        rotationV = Mathf.Clamp(rotationV + input.RotateVertical * rotationSpeed * Time.fixedUnscaledDeltaTime, -90f, 90f);
        rb.MoveRotation(Quaternion.Euler(0f, rotationH, 0f));
        cameraTransform.rotation = Quaternion.Euler(-rotationV, rotationH, 0f);

        if (Grounded)
        {
            var velocity = rb.velocity;
            velocity.z = Mathf.Cos(Mathf.Deg2Rad * rot) * input.Direction.y * moveSpeed;
            velocity.z -= Mathf.Sin(Mathf.Deg2Rad * rot) * input.Direction.x * moveSpeed;
            velocity.x = Mathf.Sin(Mathf.Deg2Rad * rot) * input.Direction.y * moveSpeed;
            velocity.x += Mathf.Cos(Mathf.Deg2Rad * rot) * input.Direction.x * moveSpeed;
            rb.velocity = velocity;

            if (input.Jump)
            {
                Grounded = false;
                rb.AddForce(0f, jumpAccelation, 0f, ForceMode.Impulse);
            }
        }
    }

    private void Update()
    {

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Grounded = true;
        }
    }
}
