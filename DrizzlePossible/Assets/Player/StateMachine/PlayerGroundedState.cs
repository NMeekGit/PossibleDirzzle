using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    :base (currentContext, playerStateFactory) {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState(){
        Debug.Log("Ground State");
        Ctx.CurrentMovementY = Ctx.GroundedGravity;
        Ctx.AppliedMovementY = Ctx.GroundedGravity;
    }

    public override void UpdateState(){
        CheckSwitchStates();
    }

    public override void ExitState(){}

    public override void InitializeSubState(){
        if (Ctx.IsMovementPressed && Ctx.IsRunPressed) {
            SetSubState(Factory.Run());
        } else if (Ctx.IsMovementPressed && !Ctx.IsRunPressed) {
            SetSubState(Factory.Walk());
        } else if (!Ctx.IsMovementPressed && !Ctx.IsRunPressed) {
            SetSubState(Factory.Idle());
        }
    }

    public override void CheckSwitchStates(){

        if (Ctx.IsJumpPressed && !Ctx.RequireNewJumpPress) {
            SwitchState(Factory.Jump());
        }
    }
}
