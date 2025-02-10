using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IMovable
{

    #region StateMachine Variables

    public StateMachine StateMachine { get; set; }
    public IdleState IdleState { get; set; }
    public WalkState WalkState { get; set; }
    public JumpState JumpState { get; set; }

    #endregion

    #region Movement Variables

    public bool IsFacingRight { get; set; }
    public Vector2 inputMovement;

    public Rigidbody2D RB { get; set; }
    public bool IsJumping { get; set; }
    public float jumpYAxis;

    #endregion

    #region Scale and constraint

    protected float MaxScale { get; set; }
    protected float MinScale { get; set; }
    protected float MaxY { get; set; }
    protected float MinY { get; set; }

    #endregion

    public Animator animator;

    public GameObject dustPrefab;


    #region Movement Functions

    public void MoveCharacter(Vector2 velocity)
    {
        transform.position = transform.position + new Vector3(velocity.x, velocity.y, 0.0f) * Time.deltaTime;

        if (transform.position.y >= MaxY)
        {
            transform.position = new Vector3(transform.position.x, MaxY, 0f);
        } else if (transform.position.y <= MinY)
        {
            transform.position = new Vector3(transform.position.x, MinY, 0f);
        }

        ScaleCharacter();
        CheckForLeftOrRightFacing();
    }

    public void ScaleCharacter()
    {
        float verticalDisplacementFactor = 1 - (transform.position.y - MinY) / (MaxY - MinY);
        float scaleFactor = MinScale + verticalDisplacementFactor * (MaxScale - MinScale);

        transform.localScale = new Vector3(IsFacingRight ? -1 * scaleFactor : 1 * scaleFactor, scaleFactor, 1);
    }


    public void CheckForLeftOrRightFacing()
    {
        if ((inputMovement.x < -0.1f && !IsFacingRight) || (inputMovement.x > 0.1f && IsFacingRight))
        {
            IsFacingRight = !IsFacingRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;

            transform.localScale = scale;
        }
    }

    #endregion


    private void Awake()
    {
        StateMachine = new StateMachine();

        IdleState = new IdleState(this, StateMachine);
        WalkState = new WalkState(this, StateMachine);
        JumpState = new JumpState(this, StateMachine);

        RB = GetComponentInChildren<Rigidbody2D>();

        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        StateMachine.Initialize(IdleState);
    }

    void Update()
    {
        inputMovement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        StateMachine.CurrentState.FrameUpdate();
    }

    void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentState.AnimationTriggerEvent(triggerType);
    }

    
    public enum AnimationTriggerType
    {
        Iddle,
        Walk,
        Run,
        Die,
        Kick,
        Punch,
    }

    public bool IsMoving()
    {
        return inputMovement.x > 0.1f
            || inputMovement.x < -0.1f
            || inputMovement.y > 0.1f
            || inputMovement.y < -0.1f;
    }
}
