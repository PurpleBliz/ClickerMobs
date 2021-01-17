using System.Collections;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public void PlayAnimation(Animator animator, string name, float time)
    {
        animator.SetBool(name, true);
        StartCoroutine(StopAnimation(animator, name, time));
    }

    private IEnumerator StopAnimation(Animator animator, string name, float time)
    {
        yield return new WaitForSeconds(time);
        animator.SetBool(name, false);
    }
}