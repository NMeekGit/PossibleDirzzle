using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    :base (currentContext, playerStateFactory) {}

    public override void EnterState(){
        Ctx.Animator.SetBool(Ctx.IsWalkingHash, false);
        Ctx.Animator.SetBool(Ctx.IsRunningHash, false);
        Ctx.AppliedMovementX = 0;
        Ctx.AppliedMovementZ = 0;
    }

    public override void UpdateState(){
        CheckSwitchStates();
        CheckFireState();
    }

    public override void ExitState(){}

    public override void InitializeSubState(){}

    public override void CheckSwitchStates(){
        if (Ctx.IsMovementPressed && Ctx.IsRunPressed) {
            SwitchState(Factory.Run());
        } else if (Ctx.IsMovementPressed) {
            SwitchState(Factory.Walk());
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
