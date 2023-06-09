using System.Collections;
using System.Collections.Generic;
using PlayerControllerInputSysten;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IStateMachine
{
    public bool OnTreeTrunk = false;

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
    public float moveInputHor;

    public float moveInputVert;

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

    protected IState _currentState;

    public Animator m_anim;

    protected Vector2 _treePos;

    private static readonly int Move = Animator.StringToHash("Move");

    private static readonly int JumpState = Animator.StringToHash("JumpState");

    private static readonly int IsJumping = Animator.StringToHash("IsJumping");

    private static readonly int
        WallGrabbing = Animator.StringToHash("WallGrabbing");

    private static readonly int IsDashing = Animator.StringToHash("IsDashing");

    void Start()
    {
        SetInitialState();

        // create pools for particles
        //PoolManager.instance.CreatePool(dashEffect, 2);
        //PoolManager.instance.CreatePool(jumpEffect, 2);
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

        UpdatemoveInput();
    }

    public void ChangeState(IState nextState)
    {
        _currentState.ExitState();
        nextState.EnterState();
        _currentState = nextState;
    }

    public void SetTreePosition()
    {
        SetPlayerPosition(_treePos);
    }

    public void UpdateIsGrounded()
    {
        isGrounded =
            Physics2D
                .OverlapCircle(groundCheck.position,
                groundCheckRadius,
                whatIsGround);

        m_groundedRemember -= Time.deltaTime;

        if (isGrounded)
        {
            m_groundedRemember = m_groundedRememberTime;
        }
    }

    public void UpdateOnWall()
    {
        var position = transform.position;
        m_onWall =
            Physics2D
                .OverlapCircle((Vector2) position + grabRightOffset,
                grabCheckRadius,
                whatIsGround) ||
            Physics2D
                .OverlapCircle((Vector2) position + grabLeftOffset,
                grabCheckRadius,
                whatIsGround);
        m_onRightWall =
            Physics2D
                .OverlapCircle((Vector2) position + grabRightOffset,
                grabCheckRadius,
                whatIsGround);
        m_onLeftWall =
            Physics2D
                .OverlapCircle((Vector2) position + grabLeftOffset,
                grabCheckRadius,
                whatIsGround);
    }

    public float GetGroundedRemember()
    {
        return m_groundedRemember;
    }

    public bool GetIsOnWall()
    {
        return m_onWall;
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
        if (!m_facingRight && moveInputHor > 0f)
        {
            Flip();
        }
        else if (m_facingRight && moveInputHor < 0f)
        {
            Flip();
        }
    }

    public virtual float GetHorizontalAxis()
    {
        return PlayerControllerInputSysten.InputSystem.HorizontalRaw();
    }

    public virtual float GetVerticalAxis()
    {
        return PlayerControllerInputSysten.InputSystem.VerticalRaw();
    }

    public virtual bool GetJump()
    {
        return PlayerControllerInputSysten.InputSystem.Jump();
    }

    public virtual void SetInitialState()
    {
        _currentState = new Idle_Standing_PlayerState(this);
    }

    public void UpdatemoveInput()
    {
        moveInputHor = GetHorizontalAxis();
        moveInputVert = GetVerticalAxis();
    }

    public bool IsMoving()
    {
        if (moveInputHor != 0)
        {
            return true;
        }

        return false;
    }

    public bool IsMovingTree()
    {
        if (moveInputVert != 0)
        {
            return true;
        }

        return false;
    }

    public Vector2 GetVelocity()
    {
        return m_rb.velocity;
    }

    public void JumpBody()
    {
        m_rb.velocity = new Vector2(m_rb.velocity.x, jumpForce);

        // jumpEffect
        // PoolManager.instance.ReuseObject(jumpEffect, groundCheck.position, Quaternion.identity);
    }

    public void SlideBody()
    {
        m_rb.velocity = new Vector2(moveInputHor * speed, -slideSpeed);
        // jumpEffect
        // PoolManager.instance.ReuseObject(jumpEffect, groundCheck.position, Quaternion.identity);
    }

    public void UpdateWallStickTime()
    {
        m_wallStick -= Time.deltaTime;
    }

    public float GetWallStickTime()
    {
        return m_wallStick;
    }

    public void ResetWallStickTime()
    {
        m_wallStick = m_wallStickTime;
    }

    public void ExtraJumpBody()
    {
        m_rb.velocity = new Vector2(m_rb.velocity.x, m_extraJumpForce);
        m_extraJumps--;

        // jumpEffect
        // PoolManager.instance.ReuseObject(jumpEffect, groundCheck.position, Quaternion.identity);
    }

    public void WallJumpBody()
    {
        CalculateSides();
        if (m_playerSide == m_onWallSide)
        {
            Flip();
        }
        m_rb
            .AddForce(new Vector2(-m_onWallSide * wallJumpForce.x,
                wallJumpForce.y),
            ForceMode2D.Impulse);
    }

    public void WallClimbBody()
    {
        CalculateSides();
        if (m_playerSide == m_onWallSide)
        {
            Flip();
        }

        m_rb
            .AddForce(new Vector2(-m_onWallSide * wallClimbForce.x,
                wallClimbForce.y),
            ForceMode2D.Impulse);
    }

    public void FixedUpdateWallJumpPhysics()
    {
        m_rb.velocity =
            Vector2
                .Lerp(m_rb.velocity,
                (new Vector2(moveInputHor * speed, m_rb.velocity.y)),
                1.5f * Time.fixedDeltaTime);
    }

    public int GetPlayerSide()
    {
        return m_playerSide;
    }

    public void ResetExtraJumps()
    {
        m_extraJumps = extraJumpCount;
    }

    public bool HasExtraJumps()
    {
        return (m_extraJumps > 0) ? true : false;
    }

    public int GetWallSide()
    {
        return m_onWallSide;
    }

    public void MoveBody()
    {
        m_rb.velocity = new Vector2(moveInputHor * speed, m_rb.velocity.y);
    }

    public void MoveBodyDashDown()
    {
        m_rb.velocity = new Vector2(0, -dashSpeed);
    }

    public void MoveBodyTree()
    {
        m_rb.velocity = new Vector2(m_rb.velocity.x, moveInputVert * speed);
    }

    public void StopBody()
    {
        m_rb.velocity = new Vector2(0, m_rb.velocity.y);
    }

    public void FreezeBody()
    {
        m_rb.velocity = new Vector2(0, 0);
    }

    public void FixedUpdateJumpPhysics()
    {
        if (m_rb.velocity.y < 0f)
        {
            m_rb.velocity +=
                Vector2.up *
                Physics2D.gravity.y *
                (fallMultiplier - 1) *
                Time.fixedDeltaTime;
        }
    }

    //ANIMATION
    public void SetAnimIdle()
    {
        m_anim.SetFloat(Move, 0);
    }

    public void SetAnimJump()
    {
        m_anim.SetBool(IsJumping, true);
    }

    public void StopAnimJump()
    {
        m_anim.SetBool(IsJumping, false);
    }

    public void UpdateAnimJump()
    {
        float verticalVelocity = m_rb.velocity.y;
        m_anim.SetFloat (JumpState, verticalVelocity);
    }

    public void SetAnimMoving()
    {
        m_anim.SetFloat(Move, Mathf.Abs(m_rb.velocity.x));
    }

    public void SetAnmimWallGrab()
    {
        m_anim.SetBool(WallGrabbing, true);
    }

    public void StopAnmimWallGrab()
    {
        m_anim.SetBool(WallGrabbing, false);
    }

    public virtual void TriggerEnter(Collider2D coll)
    {
        Debug.Log("triggier");
        return;
    }

    public virtual void TriggerExit(Collider2D coll)
    {
        return;
    }

    public void TurnOffGravity()
    {
        m_rb.gravityScale = 0.0f;
    }

    public void TurnOnGravity()
    {
        m_rb.gravityScale = 4.0f;
    }

    public void SetPlayerPosition(Vector2 pos)
    {
        gameObject.transform.position = pos;
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        TriggerEnter (coll);
    }

    public void OnTriggerExit2D(Collider2D coll)
    {
        TriggerExit (coll);
    }
}
