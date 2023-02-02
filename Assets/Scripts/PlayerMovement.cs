using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float m_runSpeed = 10.0f;
    [SerializeField] float m_jumpSpeed = 23.0f;
    [SerializeField] float m_climbSpeed = 5.0f;    
    Vector2 m_moveInput;
    Rigidbody2D m_rigidBody;
    Collider2D m_collider;
    Animator m_animator;
    float m_gravityOnStart;

    void Start()
    {
      m_rigidBody = GetComponent<Rigidbody2D>();
      m_gravityOnStart = m_rigidBody.gravityScale;
      m_collider = GetComponent<CapsuleCollider2D>();
      m_animator = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
        ClimbLadder();
        FlipSprite();
    }

    void OnMove(InputValue value)
    {
        m_moveInput = value.Get<Vector2>();
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
        if (m_collider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
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
        if (m_collider.IsTouchingLayers(LayerMask.GetMask("Ground")))
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
}
