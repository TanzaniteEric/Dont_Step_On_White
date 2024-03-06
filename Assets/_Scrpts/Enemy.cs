using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [Space(10)]
    [Header("Enemy Parameter")]
    public float EnemyChasingSpeed = 2;
    private PlayerController player;
    private PlayerFarestPos endDetect;

    private Rigidbody2D m_rigidbody;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        gameObject.tag = "Enemy";
        m_rigidbody = GetComponent<Rigidbody2D>();      
    }

    private void Start()
    {
        endDetect = FindObjectOfType<PlayerFarestPos>();
    }

    private void FixedUpdate()
    {
        if (!endDetect.gameEnd)
        {
            Vector3 velocitydirection = (player.transform.position - transform.position).normalized * EnemyChasingSpeed;

            if (m_rigidbody.velocity.magnitude < EnemyChasingSpeed || Vector2.Dot(m_rigidbody.velocity, velocitydirection) < 0) m_rigidbody.AddForce(velocitydirection.normalized, ForceMode2D.Force);
            else m_rigidbody.velocity = velocitydirection;
        }
        else
        {
            m_rigidbody.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            player.PlayerCanBeHit = false;
        }
    }
}


