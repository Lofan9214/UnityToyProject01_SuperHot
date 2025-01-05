using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    private readonly int hashAttack = Animator.StringToHash("Attack");

    public Bullet bullet;
    public Transform firePosition;
    public Animator animator;
    private PlayerInput input;

    public float fireRate = 0.5f;
    private float lastFireTime;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        lastFireTime = 0f;
    }

    private void Update()
    {
        if (input.Fire
            && lastFireTime + fireRate < Time.time)
        {
            lastFireTime = Time.time;
            Bullet bul = Instantiate(bullet);
            bul.Fire(firePosition.position + firePosition.forward * 0.9f, firePosition.forward);
            animator.SetTrigger(hashAttack);
        }
    }
}
