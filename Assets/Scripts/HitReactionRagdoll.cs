using System.Collections;
using System.Linq;
using UnityEngine;

public class HitReactionRagdoll : MonoBehaviour
{
    Animator animator;
    Rigidbody[] ragdollRBs;
    Collider[] ragdollColliders;

    [Header("Thresholds")]
    [SerializeField]
    float ragdollThreshold = 20f;

    [Header("Impulse")]
    [SerializeField]
    float impulseBase = 2.5f;

    [Header("Recovery")]
    [SerializeField]
    Rigidbody coreRigidBody;
    [SerializeField]
    float sleepVelocityThreshold = 0.05f;
    [SerializeField]
    float recoverCheckDelay = 0.5f;


    private bool isRagdoll;

    private float timeWindowAccum;
    private float timeWindowEnd;
    public float timeWindowDuration = 1.25f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        ragdollRBs = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();
    }

    public void ReportHit(Vector3 incoming, Vector3 impactPoint, float magnitude)
    {
        // cumulation of hits during predefined time window
        float now = Time.time;
        if (now > timeWindowEnd) { timeWindowAccum = 0f; timeWindowEnd = now + timeWindowDuration; }
        timeWindowAccum += magnitude;

        bool goRagdoll = magnitude >= ragdollThreshold || timeWindowAccum >= ragdollThreshold;
        if (!goRagdoll)
        {
            //the magnitude is not high enough to enable the ragdoll
            return;
        }

        EnableRagdoll();

        float impulse = impulseBase * Mathf.Max(magnitude, ragdollThreshold);
        ApplyImpulse(incoming, impactPoint, impulse);

        // try to recover after a while
        StopAllCoroutines();
        StartCoroutine(CoRecover());
    }

    private void EnableRagdoll()
    {
        if (isRagdoll)
            return;

        isRagdoll = true;
        animator.enabled = false;

        foreach (var rb in ragdollRBs)
        {
            rb.isKinematic = false;
            //rb.useGravity = true;
        }

        foreach (var col in ragdollColliders)
            col.enabled = true;
    }

    private void DisableRagdoll()
    {
        foreach (var rb in ragdollRBs)
            rb.isKinematic = true;

        isRagdoll = false;
    }

    private void ApplyImpulse(Vector3 incoming, Vector3 impactPoint, float impulse)
    {
        // apply force on the closest rigid body (based on the hit position)
        Rigidbody target = ragdollRBs
                            .OrderBy(rb => Vector3.SqrMagnitude(rb.worldCenterOfMass - impactPoint))
                            .FirstOrDefault();

        if (target != null)
            target.AddForceAtPosition(incoming * impulse, impactPoint, ForceMode.Impulse);
    }

    private IEnumerator CoRecover()
    {
        // waiting till the body is calm -> no velocity in rigid bodies (Warning! Some rigid bodies may still have velocity because of running collisions and high twist of body parts etc.)
        /*yield return new WaitForSeconds(recoverCheckDelay);
        while (ragdollRBs.Any(rb => rb.linearVelocity.magnitude > sleepVelocityThreshold))
            yield return new WaitForSeconds(recoverCheckDelay);*/

        //waiting till the core of body is calm -> no velocity in rigid body of core part (usually spine, hips etc.) (more secure than the previous option)
        do
            yield return new WaitForSeconds(recoverCheckDelay);
        while (coreRigidBody.linearVelocity.magnitude > sleepVelocityThreshold);

        bool faceDown = Vector3.Dot(transform.up, Vector3.up) < 0f;
        PlayGetUp(faceDown);

        animator.enabled = true;

        DisableRagdoll();
    }

    private void PlayGetUp(bool faceDown)
    {
        //no animations for get up front and back now, so Idle is played immediately
        //animator.Play(faceDown ? "GetUp_Front" : "GetUp_Back", 0, 0f);
        animator.Play("Idle", 0, 0f);
    }

}
