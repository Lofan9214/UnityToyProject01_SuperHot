using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private readonly string verticalAxisName = "Vertical";
    private readonly string horizontalAxisName = "Horizontal";
    private readonly string fireAxisName = "Fire1";
    private readonly string jumpAxisName = "Jump";

    private readonly string mouseX = "Mouse X";
    private readonly string mouseY = "Mouse Y";

    public Vector2 Direction { get; private set; }

    public float AxisInput
    {
        get
        {
            return Direction.magnitude;
        }
    }

    public float RotateVertical { get; private set; }
    public float RotateHorizontal { get; private set; }

    public bool Fire { get; private set; }
    public bool Jump { get; private set; }

    private void Update()
    {
        Direction = new Vector2(Input.GetAxis(horizontalAxisName), Input.GetAxis(verticalAxisName));
        if (Direction.sqrMagnitude > 1f)
        {
            Direction.Normalize();
        }
        Fire = Input.GetButtonDown(fireAxisName);
        Jump = Input.GetButton(jumpAxisName);
        RotateHorizontal = Input.GetAxis(mouseX);
        RotateVertical = Input.GetAxis(mouseY);
    }
}
