using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.AI;

public class PistolEnemy : MonoBehaviour
{
    private readonly int hashDeAim = Animator.StringToHash("DeAim");
    private readonly int hashAim = Animator.StringToHash("Aim");
    private readonly int hashMove = Animator.StringToHash("Move");
    private readonly int hashDeath = Animator.StringToHash("Die");

    public Transform muzzle;
    public Bullet bullet;
    private NavMeshAgent navMeshAgent;
    private GameManager gm;

    public Transform target;
    public Transform hip;

    private Animator animator;

    private Coroutine coUpdatePath;

    private float lastAttackTime;
    public float attackRate = 2f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void OnEnable()
    {
        animator.enabled = true;
        coUpdatePath = StartCoroutine(CoUpdatePath());
        lastAttackTime = 0f;

    }

    private void Start()
    {
        if (gm != null)
        {
            ++gm.enemyCount;
        }
    }

    private void OnDisable()
    {
        coUpdatePath = null;
        target = null;
    }

    private void Update()
    {
        Vector3 targetDirection = (target.position - hip.position).normalized;

        if (Physics.Raycast(hip.position, (target.position - hip.position).normalized, out RaycastHit hitInfo)
            && hitInfo.collider.CompareTag("Player")
            && !animator.GetCurrentAnimatorStateInfo(1).IsName("Aiming"))
        {
            Aim();
        }
        if (animator.GetCurrentAnimatorStateInfo(1).IsName("Aiming"))
        {
            float animTime = animator.GetCurrentAnimatorStateInfo(1).normalizedTime;
            if (animTime >= 1.0f
                && lastAttackTime + attackRate < Time.time)
            {
                lastAttackTime = Time.time;
                Fire();
            }
        }

        animator.SetFloat(hashMove, navMeshAgent.velocity.magnitude / navMeshAgent.speed);
    }

    public void Aim()
    {
        animator.SetTrigger(hashAim);
    }
    public void Search()
    {
        animator.SetTrigger(hashDeAim);
    }

    public void Fire()
    {
        Bullet bul = Instantiate(bullet);
        bul.Fire(muzzle.position, (target.position - muzzle.position).normalized);
    }

    public void Die()
    {
        StopCoroutine(coUpdatePath);
        animator.SetTrigger(hashDeath);
        enabled = false;

        if (gm != null)
        {
            --gm.enemyCount;
        }
    }

    private IEnumerator CoUpdatePath()
    {
        while (true)
        {
            navMeshAgent.SetDestination(target.position);

            yield return new WaitForSeconds(0.25f);
        }
    }
}
