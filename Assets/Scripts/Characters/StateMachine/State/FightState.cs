using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightState : BaseState
{
    private int comboCount = 0;
    private float comboResetTimer;
    private float nextComboDelay = 0.170f;
    public FightState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        comboResetTimer = nextComboDelay;
        comboCount = 1;

        character.animator.SetBool("Guard", true);
        character.animator.SetBool("Fighting", true);
        character.animator.SetTrigger("Punch");

    }

    public override void ExitState()
    {
        base.ExitState();
        character.animator.SetBool("Fighting", false);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (Input.GetButtonDown("Punch"))
        {
            switch (comboCount)
            {

                case 1:
                    {
                        Debug.Log("Punch 2");
                        nextComboDelay = 0.338f;

                        break;
                    }
                case 2:
                    {
                        Debug.Log("Punch 3 ");
                        nextComboDelay = 0.255f;
                        break;
                    }
                case 3:
                    {
                        Debug.Log("Kick !! ");
                        break;
                    }
            }
            character.animator.SetTrigger("Punch");
            comboResetTimer = nextComboDelay;
            comboCount += 1;
            Debug.Log("ComboCount " + comboCount);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        comboResetTimer -= Time.deltaTime;
        Debug.Log(comboResetTimer);
        if (comboResetTimer <= 0 || comboCount == 4)
        {
            character.StateMachine.ChangeState(character.IdleState);
        }

    }
}
