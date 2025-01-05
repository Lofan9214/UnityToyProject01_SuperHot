using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolEnemy : MonoBehaviour
{
    private readonly int hashFire = Animator.StringToHash("Fire");
    private readonly int hashAim = Animator.StringToHash("Aim");

    public Transform muzzlePos;
    public GameObject bullet;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        animator.enabled = true;
    }

    private void Update()
    {
        

    }

    public void Aim()
    {
        animator.SetTrigger(hashAim);
    }

    public void Fire()
    {
        animator.SetTrigger(hashFire);
    }

    public void Die()
    {
        animator.enabled = false;
    }
}
