using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerMovement move;
    private PlayerInput input;

    private float initialFixedDT;
    private bool shot;

    private void Awake()
    {
        initialFixedDT = Time.fixedDeltaTime;
        var player = GameObject.FindGameObjectWithTag("Player");
        move = player.GetComponent<PlayerMovement>();
        input = player.GetComponent<PlayerInput>();
        shot = false;
    }

    private void Start()
    {
        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = initialFixedDT * Time.timeScale;
    }

    private void FixedUpdate()
    {
        if (move.Grounded && !shot && input.AxisInput < 1e-5)
        {
            Time.timeScale = 0.1f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        Time.fixedDeltaTime = initialFixedDT * Time.timeScale;
    }

    public void Fire()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        shot = true;

        yield return new WaitForSeconds(0.1f);

        shot = false;
    }
}
