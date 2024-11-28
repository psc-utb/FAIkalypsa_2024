using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul_AudioBase : StateMachineBehaviour
{

    AudioClip[] audios;
    public AudioClip[] Audios { get => audios; set => audios = value; }

    public AudioSource AudioSource { get; set; }


    public AudioClip GetRandomAudioClip()
    {
        if (Audios != null && Audios.Length > 0)
        {
            int index = Random.Range(0, Audios.Length);
            return Audios[index];
        }

        return null;
    }


    public void PlayRandomAudioClip()
    {
        if (Audios != null && AudioSource != null)
        {
            AudioClip audioClip = GetRandomAudioClip();
            if (audioClip != null)
            {
                AudioSource.clip = audioClip;
                AudioSource.Play();
            }
        }
    }


    float delayBetweenPlaying;
    float currentDelayBetweenPlaying;
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public void PlayRandomAudioClipWithDelay(float minDelay, float maxDelay)
    {
        if (AudioSource.isPlaying == false)
        {
            if (currentDelayBetweenPlaying > delayBetweenPlaying)
            {
                PlayRandomAudioClip();
                delayBetweenPlaying = Random.Range(minDelay, maxDelay);
                currentDelayBetweenPlaying = 0;
            }
            else
            {
                currentDelayBetweenPlaying += Time.deltaTime;
            }
        }
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
