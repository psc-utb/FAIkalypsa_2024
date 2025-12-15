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
    private float rollStrength   = 8f;         // Roll rotation strength (degrees)
    [SerializeField]
    private float blendInTime = 0.1f;          // How fast the weight will increase
    [SerializeField]
    private float holdTime = 0.2f;             // How long the effect will last
    [SerializeField]
    private float blendOutTime = 0.3f;         // How fast the efect comes back to the initial position and rotation

    private Vector3 initialOffsetPos;
    private Quaternion initialRotation;

    void Awake()
    {
        if (offsetTarget)
            initialOffsetPos = offsetTarget.localPosition;
        if(rotationTarget)
            initialRotation = rotationTarget.localRotation;
    }

    /// <summary>
    /// Reaction to hit - calculates direction, sets offset and rotation, blends constraint weight.
    /// </summary>
    public void ApplyHit(Vector3 hitDirectionWorld)
    {
        Vector3 localDir = transform.InverseTransformDirection(hitDirectionWorld.normalized);

        // Offset based on hit direction (X = left/right, Z = forward/backwward)
        Vector3 offset = new Vector3(localDir.x, 0, -localDir.z) * offsetStrength;

        // Rotation based on hit direction (light rotation of the chest)
        Quaternion rotation = Quaternion.Euler(localDir.z * rollStrength, 0, -localDir.x * pitchStrength);

        StopAllCoroutines();
        StartCoroutine(FlinchRoutine(offset, rotation));

    }

    private IEnumerator FlinchRoutine(Vector3 offset, Quaternion rotation)
    {
        // Set targets
        if (offsetTarget)
            offsetTarget.localPosition = initialOffsetPos + offset;
        if (rotationTarget)
            rotationTarget.localRotation = initialRotation * rotation;

        // Blend IN
        yield return StartCoroutine(BlendWeight(0f, 1f, blendInTime));

        // Holds effect
        yield return new WaitForSeconds(holdTime);

        // Blend OUT
        yield return StartCoroutine(BlendWeight(1f, 0f, blendOutTime));

        // Reset targets
        if(offsetTarget)
            offsetTarget.localPosition = initialOffsetPos;
        if (rotationTarget)
            rotationTarget.localRotation = initialRotation;
    }

    private IEnumerator BlendWeight(float from, float to, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float w = Mathf.Lerp(from, to, t / duration);
            if(positionConstraint)
                positionConstraint.weight = w;
            if (rotationConstraint)
                rotationConstraint.weight = w;
            yield return null;
        }
    }
}
