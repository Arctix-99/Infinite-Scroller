using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField]
    private InputActionAsset m_inputAction;
    [SerializeField]
    private float m_moveSpeed = 10f;
    [SerializeField]
    private float m_jumpForce = 5f;
    [SerializeField] private float m_jumpMultiplier;
    private InputAction m_jumpAction;
    private InputAction m_moveAction;
    private Rigidbody2D m_body;
    private bool m_isOnGround;
    private Vector2 m_moveAmt;
    public InputAction m_sceneChanger;
    private Animator m_animator;
    private SpriteRenderer m_spriteRenderer;
    private int m_doubleJump = 0;
    private float m_speedPerSecond = 0.25f;
    private float m_maxSpeed = 20f;
    private float m_elapsedTime = 0f;
    private float m_currentSpeed;
    private int m_coinAmt;
    void Start()
    {
        m_body = GetComponent<Rigidbody2D>();
        m_moveAction = m_inputAction.FindAction("Move");
        m_jumpAction = m_inputAction.FindAction("Jump");
        m_sceneChanger = m_inputAction.FindAction("SceneChanger");
        m_animator = GetComponent<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_currentSpeed = m_moveSpeed;
    }


    void Update()
    {
        m_elapsedTime += Time.deltaTime;
        m_currentSpeed = Mathf.Min(m_moveSpeed + (m_elapsedTime * m_speedPerSecond), m_maxSpeed);
        Debug.Log(m_currentSpeed);
        Move();
        Jump();
        if (m_body.linearVelocity.y >= Mathf.Epsilon)
        {
            gameObject.layer = LayerMask.NameToLayer("PlayerNoCollision");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
        
    }

    private void Jump()
    {
        if (m_jumpAction.WasPressedThisFrame() && m_doubleJump < 2)
        {
            m_body.AddForce(Vector2.up * m_jumpForce, ForceMode2D.Impulse);
            m_isOnGround = false;
            m_doubleJump++;
            if (m_isOnGround == true)
            {
                m_doubleJump = 0;
            }
        }



        //      *ANIMATOR*
        if (m_isOnGround == false)
        {
            m_animator.SetBool("isRunning", true);
        }
        else if (m_moveAmt.x != 0)
        {
            m_animator.SetBool("isRunning", false);
        }
    }
    private void Move()
    {
        Vector2 m_moveAmt = m_moveAction.ReadValue<Vector2>();
        Vector2 calculatedVelocity = m_body.linearVelocity;
        calculatedVelocity.x = m_moveAmt.x * m_currentSpeed;
        m_body.linearVelocity = calculatedVelocity;

        if (m_moveAmt.x != 0 && m_isOnGround)
        {
            m_animator.SetBool("isRunning", true);
        }
        else
        {
            m_animator.SetBool("isRunning", false);
        }

        if (m_moveAmt.x > 0)
        {
            m_spriteRenderer.flipX = false;
        }
        if (m_moveAmt.x < 0)
        {
            m_spriteRenderer.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor")
            || collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            m_doubleJump = 0;
            m_isOnGround = true;
        }
    }
    public void CoinCollected()
    {
        m_coinAmt++;
    }


    public void PatsFunction()
    {
        // Hi J-P! 
    }
}
