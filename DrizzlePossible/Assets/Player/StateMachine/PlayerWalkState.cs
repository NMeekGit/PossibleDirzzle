using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    :base (currentContext, playerStateFactory) {}

    public override void EnterState(){
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, true);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, false);
    }

    public override void UpdateState(){
        CheckSwitchStates();
        Ctx.AppliedMovementX = Ctx.CurrentMovementInput.x;
        Ctx.AppliedMovementZ = Ctx.CurrentMovementInput.y;
        CheckFireState();
    }

    public override void ExitState(){}

    public override void InitializeSubState(){}

    public override void CheckSwitchStates(){
        if (Ctx.IsMovementPressed && Ctx.IsRunPressed) {
            SwitchState(Factory.Run());
        } else if (!Ctx.IsMovementPressed) {
            SwitchState(Factory.Idle());
        }
    }

    public void CheckFireState() {
        if (Ctx.IsShootingPressed && (Time.time > Ctx.NextFire)) {
            Ctx.NextFire = Time.time + Ctx.FireRate;
            RaycastHit hit;
            GameObject bullet = GameObject.Instantiate(Ctx.BulletPrefab, Ctx.FirePointTransform.position, Quaternion.identity, Ctx.BulletParent);
            BulletController bulletController = bullet.GetComponent<BulletController>();
            if (Physics.Raycast(Ctx.CameraTransform.position, Ctx.CameraTransform.forward, out hit, Mathf.Infinity)) {
                bulletController.target = hit.point;
                bulletController.hit = true;
            }
            else {
                bulletController.target = Ctx.CameraTransform.position + Ctx.CameraTransform.forward * 25f;
            }
        }
    }
}
