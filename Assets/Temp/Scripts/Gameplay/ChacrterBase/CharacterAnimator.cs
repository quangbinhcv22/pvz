using System.Linq;
using UnityEngine;

public partial class Character
{
    // private const string AttackClipName = "attack";
    //
    // private static readonly int Idle = Animator.StringToHash("idle");
    // private static readonly int Attack = Animator.StringToHash("attack");
    // private static readonly int Ultimate = Animator.StringToHash("ultimate");
    // private static readonly int Move = Animator.StringToHash("move");
    // private static readonly int Run = Animator.StringToHash("run");
    // private static readonly int AttackSpeed = Animator.StringToHash("attack_speed");
    //
    //
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