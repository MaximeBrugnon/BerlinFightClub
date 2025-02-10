using UnityEngine;

public class WalkState : BaseState
{
    private float moveSpeed = 5f;

    public WalkState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void AnimationTriggerEvent(Character.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        character.animator.SetFloat("Speed", moveSpeed);
        Debug.Log("Hello Walk State");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (!character.IsMoving())
        {
            character.StateMachine.ChangeState(character.IdleState);
        }

        if (Input.GetButtonDown("Jump") && !character.IsJumping)
        {
            character.StateMachine.ChangeState(character.JumpState);

        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Vector2 velocity = new Vector2(character.inputMovement.x * moveSpeed, character.inputMovement.y * moveSpeed/4);
        character.MoveCharacter(velocity);

    }
}
