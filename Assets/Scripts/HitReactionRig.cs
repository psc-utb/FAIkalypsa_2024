using UnityEngine;
using UnityEngine.Animations.Rigging;
using System.Collections;

public class HitReactionRig : MonoBehaviour
{
    [Header("Rig Targets")]
    public Transform torsoOffsetTarget;       // Target for Multi-PositionConstraint
    public Transform torsoRotationTarget;     // Target for Multi-RotationConstraint

    [Header("Constraints")]
    public MultiPositionConstraint positionConstraint;
    public MultiRotationConstraint rotationConstraint;

    [Header("Settings")]
    public float offsetStrength = 0.1f;       // Translate strength (meters)
    public float rotationStrength = 15f;      // Rotation strength (degrees)
    public float blendInTime = 0.1f;          // How fast the weight will increase
    public float holdTime = 0.2f;             // How long the effect will last
    public float blendOutTime = 0.3f;         // How fast the efect comes back to the initial position and rotation

    private Vector3 initialOffsetPos;
    private Quaternion initialRotation;

    void Awake()
    {
        initialOffsetPos = torsoOffsetTarget.localPosition;
        initialRotation = torsoRotationTarget.localRotation;
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
        Quaternion rotation = Quaternion.Euler(localDir.z * rotationStrength, 0, -localDir.x * rotationStrength);

        StopAllCoroutines();
        StartCoroutine(FlinchRoutine(offset, rotation));
    }

    private IEnumerator FlinchRoutine(Vector3 offset, Quaternion rotation)
    {
        // Set targets
        torsoOffsetTarget.localPosition = initialOffsetPos + offset;
        torsoRotationTarget.localRotation = initialRotation * rotation;

        // Blend IN
        yield return StartCoroutine(BlendWeight(0f, 1f, blendInTime));

        // Holds effect
        yield return new WaitForSeconds(holdTime);

        // Blend OUT
        yield return StartCoroutine(BlendWeight(1f, 0f, blendOutTime));

        // Reset targets
        torsoOffsetTarget.localPosition = initialOffsetPos;
        torsoRotationTarget.localRotation = initialRotation;
    }

    private IEnumerator BlendWeight(float from, float to, float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            float w = Mathf.Lerp(from, to, t / duration);
            positionConstraint.weight = w;
            rotationConstraint.weight = w;
            yield return null;
        }
    }
}
