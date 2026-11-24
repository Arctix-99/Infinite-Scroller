using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private InputActionAsset m_inputAction;
    [SerializeField] private float m_moveSpeed = 10f;
    [SerializeField] private float m_jumpForce = 5f;
    [SerializeField] private float m_speedPerSecond = 0.25f;
    //[SerializeField] private float m_maxSpeed = 20f;
    private InputAction m_jumpAction;
    private InputAction m_moveAction;
    private Rigidbody2D m_body;
    private bool m_isOnGround;
    private Vector2 m_moveAmt;
    private Animator m_animator;
    private SpriteRenderer m_spriteRenderer;
    private int m_doubleJump = 0;
    private float m_elapsedTime = 0f;
    private float m_currentSpeed;
    private int m_coinAmt;
    private bool m_isStarted;
    private InputAction m_attack;
    void Start()
    {
        m_body = GetComponent<Rigidbody2D>();
        m_moveAction = m_inputAction.FindAction("Move");
        m_jumpAction = m_inputAction.FindAction("Jump");
        m_animator = GetComponent<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_currentSpeed = m_moveSpeed;
        m_attack = m_inputAction.FindAction("Attack");
    }


    void Update()
    {
        if (!m_isStarted)
        {
            m_isStarted = m_attack.WasPressedThisFrame();
            
            return;
        }
        Move();
        Jump();




    }

    private void Jump()
    {
        //          Double Saut                             //
        if (m_jumpAction.WasPressedThisFrame() && m_doubleJump < 2)
        {
            m_body.linearVelocityY = 0;
            m_body.AddForce(Vector2.up * m_jumpForce, ForceMode2D.Impulse);
            m_isOnGround = false;
            m_doubleJump++;
            if (m_isOnGround == true)
            {
                m_doubleJump = 0;
            }
        }



        //      Animator Jump(remplacer par courir)         //
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
        //          Movement Avec Physique                  //
        Vector2 m_moveAmt = m_moveAction.ReadValue<Vector2>();
        Vector2 calculatedVelocity = m_body.linearVelocity;
        calculatedVelocity.x = m_moveSpeed * m_currentSpeed;
        m_body.linearVelocity = calculatedVelocity;
        m_moveSpeed += m_speedPerSecond * Time.deltaTime;

        //          Animator Movement                       //
        if (m_moveSpeed != 0 && m_isOnGround)
        {
            m_animator.SetBool("isRunning", true);
        }
        else
        {
            m_animator.SetBool("isRunning", false);
        }
    }
    //              Player Death                            //
    public void OnDeath()
    {
        SceneManager.LoadScene("DeathScene");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
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
