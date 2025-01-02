using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Bullet bullet;
    public Transform firePosition;
    private PlayerInput input;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (input.Fire)
        {
            Bullet bul = Instantiate(bullet);
            bul.Fire(transform.position + firePosition.forward * 0.6f, firePosition.forward);
        }
    }
}
