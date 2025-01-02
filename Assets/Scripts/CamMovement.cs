using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    public Transform playerCamera;

    private void Update()
    {
        transform.position = playerCamera.position;
        transform.rotation = playerCamera.rotation;
    }
}
