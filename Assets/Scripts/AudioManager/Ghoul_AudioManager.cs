using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Ghoul_AudioManager : MonoBehaviour
{

    Animator animator;
    AudioSource audioSource;


    [SerializeField]
    AudioClip[] idleAudios;

    [SerializeField]
    AudioClip[] walkAudios;

    [SerializeField]
    AudioClip[] runAudios;

    [SerializeField]
    AudioClip[] attackAudios;

    [SerializeField]
    AudioClip[] deathAudios;


    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }


    // Start is called before the first frame update
    void Start()
    {
        GhoulAudioInit();
    }

    void GhoulAudioInit()
    {
        if (animator != null)
        {
            GhoulIdleAudio ghoulIdleAudio = animator.GetBehaviour<GhoulIdleAudio>();
            if (ghoulIdleAudio != null)
            {
                ghoulIdleAudio.Audios = idleAudios;
                ghoulIdleAudio.AudioSource = audioSource;
            }

            GhoulWalkAudio ghoulWalkAudio = animator.GetBehaviour<GhoulWalkAudio>();
            if (ghoulWalkAudio != null)
            {
                ghoulWalkAudio.Audios = walkAudios;
                ghoulWalkAudio.AudioSource = audioSource;
            }

            GhoulRunAudio ghoulRunAudio = animator.GetBehaviour<GhoulRunAudio>();
            if (ghoulRunAudio != null)
            {
                ghoulRunAudio.Audios = runAudios;
                ghoulRunAudio.AudioSource = audioSource;
            }

            GhoulAttackAudio ghoulAttackAudio = animator.GetBehaviour<GhoulAttackAudio>();
            if (ghoulAttackAudio != null)
            {
                ghoulAttackAudio.Audios = attackAudios;
                ghoulAttackAudio.AudioSource = audioSource;
            }

            GhoulDeathAudio ghoulDeathAudio = animator.GetBehaviour<GhoulDeathAudio>();
            if (ghoulDeathAudio != null)
            {
                ghoulDeathAudio.Audios = deathAudios;
                ghoulDeathAudio.AudioSource = audioSource;
            }
        }
    }
    
    public void Reinitializate()
    {
        GhoulAudioInit();
    }


    // Update is called once per frame
    void Update()
    {

    }
}
