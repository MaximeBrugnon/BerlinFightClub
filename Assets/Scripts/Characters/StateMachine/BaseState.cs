using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState
{
    protected Character character;
    protected StateMachine statemachine;

    public BaseState(Character character, StateMachine stateMachine)
    {
        this.character = character;
        this.statemachine = stateMachine;
    }

    public virtual void EnterState() { }

    public virtual void ExitState() { }

    public virtual void FrameUpdate() { }

    public virtual void PhysicsUpdate() { }

    public virtual void AnimationTriggerEvent(Character.AnimationTriggerType triggerType) { }

   

}
