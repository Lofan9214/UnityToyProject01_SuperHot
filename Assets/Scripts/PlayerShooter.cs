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

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (input.Fire)
        {
            Bullet bul = Instantiate(bullet);
            bul.Fire(firePosition.position + firePosition.forward * 0.9f, firePosition.forward);
            animator.SetTrigger(hashAttack);
        }
    }
}
