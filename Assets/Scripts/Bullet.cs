using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float m_bulletSpeed = 11.0f;
    Rigidbody2D m_rigidBody;
    PlayerMovement m_playerMovement;
    float m_horizontalSpeed = 0.0f;

    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_playerMovement = FindObjectOfType<PlayerMovement>();
        m_horizontalSpeed = m_bulletSpeed * m_playerMovement.transform.localScale.x;
    }

    void Update()
    {
        m_rigidBody.velocity = new Vector2(m_horizontalSpeed, 0.0f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemies")
            Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
