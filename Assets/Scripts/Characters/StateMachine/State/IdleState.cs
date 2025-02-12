using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    public IdleState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void AnimationTriggerEvent(Character.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        character.animator.SetFloat("Speed", 0f);
        Debug.Log("Hello Idle State");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        if (character.IsMoving())
        {
            character.StateMachine.ChangeState(character.WalkState);
        }

        if (Input.GetButtonDown("Jump") && !character.IsJumping)
        {
            character.StateMachine.ChangeState(character.JumpState);

        }

        if (Input.GetButtonDown("Guard"))
        {
            character.animator.SetBool("Guard", true);
        }

        if (Input.GetButtonUp("Guard"))
        {
            character.animator.SetBool("Guard", false);

        }

        if (Input.GetButtonDown("Punch"))
        {
            character.StateMachine.ChangeState(character.FightState);

        }
    }

    public override void PhysicsUpdate()
    {

    }

}
