using UnityEngine;
using UnityEngine.Animations.Rigging;
using System.Collections;

public class HitReactionRig : MonoBehaviour
{
    [Header("Rig Targets")]
    [SerializeField]
    private Transform offsetTarget;       // Target for Multi-PositionConstraint
    [SerializeField]
    private Transform rotationTarget;     // Target for Multi-RotationConstraint

    [Header("Constraints")]
    [SerializeField]
    private MultiPositionConstraint positionConstraint;
    [SerializeField]
    private MultiRotationConstraint rotationConstraint;

    [Header("Settings")]
    [SerializeField]
    private float offsetStrength = 0.1f;       // Translate strength (meters)
    [SerializeField]
    private float pitchStrength = 15f;         // Pitch rotation strength (degrees)
    [SerializeField]
    private float yawStrength = 6f;            // Yaw rotation strength (degrees)
    [SerializeField]
    private float rollStrength = 8f;           // Roll rotation strength (degrees)
    [SerializeField]
    private float blendInTime = 0.1f;          // How fast the weight will increase
    [SerializeField]
    private float holdTime = 0.2f;             // How long the effect will last
    [SerializeField]
    private float blendOutTime = 0.3f;         // How fast the efect comes back to the initial position and rotation

    [Header("Axis-Direction Correction")]
    [SerializeField]
    private bool axisCorrectionX = false;
    [SerializeField]
    private bool axisCorrectionY = false;
    [SerializeField]
    private bool axisCorrectionZ = false;


    private Vector3 initialOffsetPos;
    private Quaternion initialRotation;

    bool blendInIsRunning = false;

    void Awake()
    {
        initialOffsetPos = transform.position;
        initialRotation = transform.rotation;
    }

    /// <summary>
    /// Reaction to hit - calculates direction, sets offset and rotation, blends constraint weight in coroutine.
    /// </summary>
    public void ApplyHit(Vector3 hitDirectionWorld)
    {
        //original code
        /*Vector3 localDir = transform.InverseTransformDirection(hitDirectionWorld.normalized);

        // Offset based on hit direction (X = left/right, Z = forward/backwward)
        Vector3 offset = new Vector3(localDir.x, 0, -localDir.z) * offsetStrength;

        // Rotation based on hit direction (light rotation of the chest)
        Quaternion rotation = Quaternion.Euler(localDir.z * rollStrength, 0, -localDir.x * pitchStrength);

        StopAllCoroutines();
        StartCoroutine(FlinchRoutine(offset, rotation));*/

        if (blendInIsRunning == false)
        {
            //direction of bullet is opossite to body part which is hit
            Vector3 dir = -hitDirectionWorld.normalized;

            // Coefficients based on local axes of body part which is hit -> scalar product of direction of bullet and forward axis of body part in world space
            float frontBack = Vector3.Dot(dir, transform.forward);    // + => hit from the front direction (+Z)
            float leftRight = Vector3.Dot(dir, transform.right);      // + => hit from the right side (+X)

            // Rotate in direction of hit/bullet
            // front hit (frontBack is positive+) => rotate back => pitchDeg is positive around local X
            float pitchDeg = (axisCorrectionX ? -1 : 1) * frontBack * pitchStrength;
            // right hit (leftRight is negative-) => rotate to left
            float yawDeg = (axisCorrectionY ? -1 : 1) * -leftRight * yawStrength;
            // right hit (leftRight is positive+) => rotate to left => rollDeg is negative around local Z
            float rollDeg = (axisCorrectionZ ? -1 : 1) * leftRight * rollStrength;

            // Rotation around local axis
            Quaternion pitch = Quaternion.AngleAxis(pitchDeg, transform.right); // X
            Quaternion yaw = Quaternion.AngleAxis(yawDeg, transform.up);        // Y
            Quaternion roll = Quaternion.AngleAxis(rollDeg, transform.forward); // Z

            // Total rotation areound all local axes
            Quaternion worldRotDiff = pitch * yaw * roll;

            // target position – move from the bullet direction
            Vector3 worldPosDiff = dir * offsetStrength;

            StopAllCoroutines();
            StartCoroutine(FlinchRoutine(worldPosDiff, worldRotDiff));
        }
    }

    private IEnumerator FlinchRoutine(Vector3 offset, Quaternion rotation)
    {
        // Set targets
        if (offsetTarget)
            offsetTarget.position = transform.position + offset;
        if (rotationTarget)
            rotationTarget.rotation = transform.rotation * rotation;

        // Blend IN
        blendInIsRunning = true;
        yield return BlendWeight(0f, 1f, blendInTime);
        blendInIsRunning = false;

        // Holds effect
        yield return new WaitForSeconds(holdTime);

        // Blend OUT
        yield return BlendWeight(1f, 0f, blendOutTime);

        // Reset targets
        if (offsetTarget)
            offsetTarget.position = initialOffsetPos;
        if (rotationTarget)
            rotationTarget.rotation = initialRotation;

    }

    private IEnumerator BlendWeight(float from, float to, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float w = Mathf.Lerp(from, to, t / duration);
            if (positionConstraint)
                positionConstraint.weight = w;
            if (rotationConstraint)
                rotationConstraint.weight = w;
            yield return null;
        }
    }
}
