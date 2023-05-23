using System.Collections;
using System.Collections.Generic;
using PlayerControllerInputSysten;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IStateMachine
{
    [SerializeField]
    private float speed;

    [Header("Jumping")]
    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private float fallMultiplier;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private float groundCheckRadius;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private int extraJumpCount = 1;

    [SerializeField]
    private GameObject jumpEffect;

    [Header("Dashing")]
    [SerializeField]
    private float dashSpeed = 30f;

    [
        Tooltip(
            "Amount of time (in seconds) the player will be in the dashing speed")
    ]
    [SerializeField]
    private float startDashTime = 0.1f;

    [Tooltip("Time (in seconds) between dashes")]
    [SerializeField]
    private float dashCooldown = 0.2f;

    [SerializeField]
    private GameObject dashEffect;

    // Access needed for handling animation in Player script and other uses
    [HideInInspector]
    public bool isGrounded;

    //[HideInInspector]
    public float moveInput;

    [HideInInspector]
    public bool canMove = true;

    [HideInInspector]
    public bool isDashing = false;

    [HideInInspector]
    public bool actuallyWallGrabbing = false;

    // controls whether this instance is currently playable or not
    [HideInInspector]
    public bool isCurrentlyPlayable = false;

    [Header("Wall grab & jump")]
    [Tooltip("Right offset of the wall detection sphere")]
    public Vector2 grabRightOffset = new Vector2(0.16f, 0f);

    public Vector2 grabLeftOffset = new Vector2(-0.16f, 0f);

    public float grabCheckRadius = 0.24f;

    public float slideSpeed = 2.5f;

    public Vector2 wallJumpForce = new Vector2(10.5f, 18f);

    public Vector2 wallClimbForce = new Vector2(4f, 14f);

    private Rigidbody2D m_rb;

    private ParticleSystem m_dustParticle;

    private bool m_facingRight = true;

    private readonly float m_groundedRememberTime = 0.25f;

    private float m_groundedRemember = 0f;

    private int m_extraJumps;

    private float m_extraJumpForce;

    private float m_dashTime;

    private bool m_hasDashedInAir = false;

    private bool m_onWall = false;

    private bool m_onRightWall = false;

    private bool m_onLeftWall = false;

    private bool m_wallGrabbing = false;

    private readonly float m_wallStickTime = 0.25f;

    private float m_wallStick = 0f;

    private bool m_wallJumping = false;

    private float m_dashCooldown;

    // 0 -> none, 1 -> right, -1 -> left
    private int m_onWallSide = 0;

    private int m_playerSide = 1;

    protected bool _isactive = true;

    private IState _currentState;

    public Animator m_anim;

    private static readonly int Move = Animator.StringToHash("Move");

    private static readonly int JumpState = Animator.StringToHash("JumpState");

    private static readonly int IsJumping = Animator.StringToHash("IsJumping");

    private static readonly int
        WallGrabbing = Animator.StringToHash("WallGrabbing");

    private static readonly int IsDashing = Animator.StringToHash("IsDashing");

    void Start()
    {
        _currentState = new Idle_Standing_PlayerState(this);

        // create pools for particles
        //PoolManager.instance.CreatePool(dashEffect, 2);
        // PoolManager.instance.CreatePool(jumpEffect, 2);
        // if it's the player, make this instance currently playable
        if (transform.CompareTag("Player")) isCurrentlyPlayable = true;

        m_extraJumps = extraJumpCount;
        m_dashTime = startDashTime;
        m_dashCooldown = dashCooldown;
        m_extraJumpForce = jumpForce * 0.7f;

        m_rb = GetComponent<Rigidbody2D>();
        m_dustParticle = GetComponentInChildren<ParticleSystem>();
        m_anim = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        _currentState.DoStateFixedUpdate();
    }

    void Update()
    {
        _currentState.DoStateUpdate();

        UpdateMoveInput();      

       
    }

    public void ChangeState(IState nextState)
    {
        _currentState.ExitState();
        nextState.EnterState();
        _currentState = nextState;
    }

    public void UpdateIsGrounded()
    {
        isGrounded =
            Physics2D
                .OverlapCircle(groundCheck.position,
                groundCheckRadius,
                whatIsGround);
    }

    public void CalculateSides()
    {
        if (m_onRightWall)
            m_onWallSide = 1;
        else if (m_onLeftWall)
            m_onWallSide = -1;
        else
            m_onWallSide = 0;

        if (m_facingRight)
            m_playerSide = 1;
        else
            m_playerSide = -1;
    }

    public void Flip()
    {
        m_facingRight = !m_facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void UpdateFlip()
    {
        if (!m_facingRight && moveInput > 0f)
        {
            Flip();
        }
        else if (m_facingRight && moveInput < 0f)
        {
            Flip();
        }
    }

    public virtual float GetHorizontalAxis()
    {       
        return PlayerControllerInputSysten.InputSystem.HorizontalRaw();
    }

    public virtual bool GetJump()
    {
        return PlayerControllerInputSysten.InputSystem.Jump();
    }

    public void UpdateMoveInput()
    {
        moveInput = GetHorizontalAxis();
    }

    public void SetAnimIdle()
    {
        m_anim.SetFloat(Move, 0);
    }

    public void SetAnimMoving()
    {
        m_anim.SetFloat(Move, Mathf.Abs(m_rb.velocity.x));
    }

    public bool IsMoving()
    {
        if (moveInput != 0)
        {
            return true;
        }

        return false;
    }

    public void MoveBody()
    {
        m_rb.velocity = new Vector2(moveInput * speed, m_rb.velocity.y);
    }

    public void StopBody()
    {
        m_rb.velocity = new Vector2(0, m_rb.velocity.y);
    }
}
