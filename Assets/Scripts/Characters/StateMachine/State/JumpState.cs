using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : BaseState
{
    public float jumpForce = 800f;
    private JumpSequenceType jumpSequence = JumpSequenceType.Grounded;
    private float jumpLastHeight = 0;
    private GameObject dustCloud;

    public JumpState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void AnimationTriggerEvent(Character.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();

        dustCloud = GameObject.Find("Dustcloud");

        character.RB.WakeUp();
        character.RB.gravityScale = 5f;

        character.animator.SetBool("Jumping", true);

        GameObject.Find("Shadow").transform.localScale = new Vector3(.7f, .7f, .7f);
    }

    public override void ExitState()
    {
        base.ExitState();
        character.IsJumping = false;
        character.RB.Sleep();
        character.RB.gravityScale = 0;
        character.RB.transform.localPosition = new Vector3(0, 0, 0);

        GameObject.Find("Shadow").transform.localScale = new Vector3(1, 1, 1);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        RefreshJumpSequence();

        if (character.IsJumping)
        {
            switch (jumpSequence)
            {
                case JumpSequenceType.GoingUp:
                    {
                        // Jumping up
                        character.animator.SetBool("Jumping", true);
                        break;
                    }
                case JumpSequenceType.GoingDown:
                    {
                        // Falling back
                        character.animator.SetBool("Jumping", false);
                        break;
                    }
                case JumpSequenceType.Grounded:

                    // Animation
                    character.animator.SetTrigger("TouchGround");

                    ShowParticles();

                    // Leave state
                    character.StateMachine.ChangeState(character.IdleState);
                    break;
            }

        } else if (!character.IsJumping)
        {
            character.IsJumping = true;
            character.RB.AddForce(Vector2.up * jumpForce);
        }

    }

    public enum JumpSequenceType
    {
        GoingUp,
        GoingDown,
        Grounded
    }

    public void RefreshJumpSequence()
    {
        if (character.RB.transform.localPosition.y <= 0)
        {
            jumpSequence = JumpSequenceType.Grounded;
        }
        else if (character.RB.transform.localPosition.y > jumpLastHeight)
        {
            jumpSequence = JumpSequenceType.GoingUp;
        }
        else if (character.RB.transform.localPosition.y < jumpLastHeight)
        {
            jumpSequence = JumpSequenceType.GoingDown;

        }

        jumpLastHeight = character.RB.transform.localPosition.y;
    }

    public void ShowParticles()
    {
        GameObject.Instantiate(character.dustPrefab, character.transform);
    }
}
