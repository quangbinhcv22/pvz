using Spine;
using Spine.Unity;
using UnityEngine;

public class SpineAnimator : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation animator;
    
    
    void Start()
    {
        animator.Initialize(true);
    }

    private void AnimationStateOnComplete(TrackEntry trackentry)
    {
        if (trackentry.Loop) return;

        SetStateDefault();
    }

    public void SetStateDefault()
    {
        PlayLoop(StateName.Idle);
    }


    public void PlayOnce(string stateName)
    {
        animator.AnimationState.Complete -= AnimationStateOnComplete;
        animator.AnimationState.Complete += AnimationStateOnComplete;

        animator.AnimationState.SetAnimation(0, stateName, false);
    }

    public void PlayLoop(string stateName)
    {
        animator.AnimationState.SetAnimation(0, stateName, true);
    }

    public void PlayLast(string stateName)
    {
        animator.AnimationState.Complete -= AnimationStateOnComplete;

        animator.AnimationState.SetAnimation(0, stateName, false);
    }
    
    public void Continue()
    {
        animator.enabled = true;
    }

    public void Stop()
    {
        animator.enabled = false;
    }
}

public static class StateName
{
    public const string Idle = "idle";
    public const string Die = "die";
    public const string Attack = "attack";
    public const string Ultimate = "ultimate";
    public const string Cooldown = "cooldown";
    public const string Walk = "walk";
    public const string Run = "run";
}