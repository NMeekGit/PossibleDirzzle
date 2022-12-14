using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    :base (currentContext, playerStateFactory) {
        IsRootState = true;
        InitializeSubState();
    }

    public override void EnterState(){
        Debug.Log("Jump State");
        HandleJump();
    }

    public override void UpdateState(){
        CheckSwitchStates();
        HandleGravity();
    }

    public override void ExitState(){
        Ctx.Animator.SetBool(Ctx.IsJumpingHash, false);
        if (Ctx.IsJumpPressed) {
            Ctx.RequireNewJumpPress = true;
        }
    }

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
        if (Ctx.CharacterController.isGrounded) {
            SwitchState(Factory.Grounded());
        }
    }

    void HandleJump()
    {
        Ctx.IsJumping = true;
        Ctx.Animator.SetBool(Ctx.IsJumpingHash, true);
        Ctx.AudioManager.Play("Jump");
        Ctx.CurrentMovementY = Ctx.InitialJumpVelocity;
        Ctx.AppliedMovementY = Ctx.InitialJumpVelocity;
    }

    void HandleGravity()
    {
            float previousVelocity = Ctx.CurrentMovementY;
            Ctx.CurrentMovementY = Ctx.CurrentMovementY + (Ctx.Gravity * Time.deltaTime);
            Ctx.AppliedMovementY = (previousVelocity + Ctx.CurrentMovementY) * 0.5f;
    }
}
