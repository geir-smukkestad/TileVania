using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float m_runSpeed = 10.0f;
    [SerializeField] float m_jumpSpeed = 23.0f;
    [SerializeField] float m_climbSpeed = 5.0f;    
    [SerializeField] Vector2 m_deathKick = new Vector2(10.0f, 20.0f);
    [SerializeField] GameObject m_bullet;
    [SerializeField] Transform m_gunTransform;
    Vector2 m_moveInput;
    Rigidbody2D m_rigidBody;
    BoxCollider2D m_feetCollider;
    CapsuleCollider2D m_bodyCollider;
    Animator m_animator;
    float m_gravityOnStart;
    bool m_isAlive = true;
    SpriteRenderer m_spriteRenderer;

    void Start()
    {
      m_rigidBody = GetComponent<Rigidbody2D>();
      m_gravityOnStart = m_rigidBody.gravityScale;
      m_feetCollider = GetComponent<BoxCollider2D>();
      m_bodyCollider = GetComponent<CapsuleCollider2D>();
      m_animator = GetComponent<Animator>();
      m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!m_isAlive)
            return;

        Run();
        ClimbLadder();
        FlipSprite();
        CheckAliveState();
    }

    void OnMove(InputValue value)
    {
        m_moveInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value)
    {
        // GetComponentInChildren<Gun>();
        Instantiate(m_bullet, m_gunTransform.position, m_gunTransform.rotation);
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(m_moveInput.x * m_runSpeed, m_rigidBody.velocity.y);
        m_rigidBody.velocity = playerVelocity;

        bool isRunning = Mathf.Abs(playerVelocity.x) > Mathf.Epsilon;
        m_animator.SetBool("isRunning", isRunning);
    }

    void ClimbLadder()
    {
        if (m_bodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            float verticalVelocity = m_moveInput.y * m_climbSpeed;
            Vector2 climbVelocity = new Vector2(m_rigidBody.velocity.x, verticalVelocity);
            m_rigidBody.velocity = climbVelocity;
            m_rigidBody.gravityScale = 0;

            // float m_moveInput.y * m_climbSpeed
            bool isMovingVertically = Mathf.Abs(verticalVelocity) > Mathf.Epsilon;
            m_animator.SetBool("isClimbing", isMovingVertically);
        }
        else
        {
            m_rigidBody.gravityScale = m_gravityOnStart;
            m_animator.SetBool("isClimbing", false);
        }
    }

    void OnJump(InputValue value)
    {
        if (m_feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if (value.isPressed)
            {
                m_rigidBody.velocity += new Vector2(0, m_jumpSpeed);
            }
        }
    }

    void FlipSprite()
    {
        bool hasHorizontalSpeed = Mathf.Abs(m_moveInput.x) > Mathf.Epsilon;
        if (hasHorizontalSpeed)
            transform.localScale = new Vector2(Mathf.Sign(m_moveInput.x), 1.0f);
    }

    void CheckAliveState()
    {
        m_isAlive = !m_rigidBody.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards"));
        if (!m_isAlive)
        {
            m_animator.SetTrigger("Dying");
            m_rigidBody.velocity = new Vector2(-Mathf.Sign(m_rigidBody.velocity.x) * m_deathKick.x, m_deathKick.y);

            FindObjectOfType<GameManager>().ProcessPlayerDeath();
        }
    }
}
