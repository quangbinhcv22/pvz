using System.Linq;
using UnityEngine;

public partial class Character
{
    [SerializeField] protected SpineAnimator animator;
    //
    // private const float StandardSecondsPerAttack = 1f;
    // private float standardizedAttackAnimFactor;
    //
    //
    protected virtual void SwitchState(string stateName)
    {
        animator.PlayOnce(stateName);
    }


    private void CalculateAnimFactor()
    {
        // if (!animator || !animator.runtimeAnimatorController) return;
        //
        // var clips = animator.runtimeAnimatorController.animationClips;
        //
        // var attackClip = clips.First(clip => clip.name == AttackClipName);
        // var attackClipTime = attackClip.length;
        //
        // standardizedAttackAnimFactor = StandardSecondsPerAttack / attackClipTime;
    }


    private float attackAnimSpeed;

    private void ReCalculateAttackAnimSpeed(float unknown)
    {
        // var time = actionTime.Value;
        // var speedFactor = actionSpeedFactor.Value;
        //
        // attackAnimSpeed = standardizedAttackAnimFactor * speedFactor / time;
        // animator.SetFloat(AttackSpeed, attackAnimSpeed);
    }
}