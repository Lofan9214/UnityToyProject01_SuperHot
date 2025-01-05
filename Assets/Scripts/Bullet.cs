using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;

    public GameObject bullet;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Fire(Vector3 position, Vector3 direction)
    {
        transform.position = position;
        transform.forward = direction;
        rb.AddForce(direction * speed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.collider.tag)
        {
            case "Player":
                GameObject.FindGameObjectWithTag("GameController")?.GetComponent<GameManager>()?.PlayerDie();
                break;
            case "Enemy":
                collision.collider.GetComponent<PistolEnemy>().Die();
                break;
        }
        rb.velocity = Vector3.zero;
        bullet.SetActive(false);
        Destroy(gameObject, 0.5f);
    }
}
