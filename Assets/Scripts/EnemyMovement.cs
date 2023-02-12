using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float m_enemySpeed = 1.0f;
    Rigidbody2D m_rigidBody;

    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        m_rigidBody.velocity = new Vector2(m_enemySpeed, 0);
    }

     private void OnTriggerExit2D(Collider2D other)
    {
        m_enemySpeed = -m_enemySpeed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(Mathf.Sign(m_enemySpeed), 1);
    }
}
