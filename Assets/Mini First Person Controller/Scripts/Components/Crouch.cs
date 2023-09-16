using System.Collections;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    [SerializeField] KeyCode _key = KeyCode.LeftControl;

    [Header("Slow Movement")]
    [Tooltip("Movement to slow down when crouched.")]
    public FirstPersonMovement movement;
    [Tooltip("Movement speed when crouched.")]
    public float movementSpeed = 2;

    [Header("Low Head")]
    [Tooltip("Head to lower when crouched.")]
    public Transform headToLower;
    [HideInInspector]
    public float? defaultHeadYLocalPosition;
    public float crouchYHeadPosition = 1;
    
    [Tooltip("Collider to lower when crouched.")]
    public CapsuleCollider colliderToLower;
    [HideInInspector]
    public float? defaultColliderHeight;

    [SerializeField] float _CrouchTransitionSpeed = 2;

    public bool IsCrouched { get; private set; }
    public event System.Action CrouchStart, CrouchEnd;


    void Reset()
    {
        movement = GetComponentInParent<FirstPersonMovement>();
        headToLower = movement.GetComponentInChildren<Camera>().transform;
        colliderToLower = movement.GetComponentInChildren<CapsuleCollider>();
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(_key))
        {
            if (headToLower)
            {
                if (!defaultHeadYLocalPosition.HasValue)
                    defaultHeadYLocalPosition = headToLower.localPosition.y;
            }

            float loweringAmount = 0;

            if (colliderToLower)
            {
                if (!defaultColliderHeight.HasValue)
                    defaultColliderHeight = colliderToLower.height;

                if (defaultHeadYLocalPosition.HasValue)
                    loweringAmount = defaultHeadYLocalPosition.Value - crouchYHeadPosition;
                else
                    loweringAmount = defaultColliderHeight.Value * .5f;
            }

            StopAllCoroutines();
            StartCoroutine(Crouching(defaultColliderHeight.Value - loweringAmount, crouchYHeadPosition));

            SetSpeedOverrideActive(true);
            CrouchStart?.Invoke();
        }

        if(Input.GetKeyUp(_key))
        {
            StopAllCoroutines();
            StartCoroutine(Crouching(defaultColliderHeight.Value, defaultHeadYLocalPosition.Value));
            SetSpeedOverrideActive(false);
            CrouchEnd?.Invoke();
        }
    }


    IEnumerator Crouching(float colliderHeight, float headPosition)
    {
        bool headInPosition = false, colliderInPosition = false;

        while (!headInPosition || !colliderInPosition)
        {
            if(!headInPosition)
            {
                if(!Mathf.Approximately(headToLower.localPosition.y, headPosition))
                {
                    headToLower.localPosition = new(headToLower.localPosition.x, 
                        Mathf.MoveTowards(headToLower.localPosition.y, headPosition, Time.deltaTime * _CrouchTransitionSpeed), 
                        headToLower.localPosition.z);
                }
                else
                    headInPosition = true;
            }

            if (!colliderInPosition)
            {
                if (!Mathf.Approximately(colliderToLower.height, colliderHeight))
                {
                    colliderToLower.height = Mathf.MoveTowards(colliderToLower.height, colliderHeight, Time.deltaTime * _CrouchTransitionSpeed);
                    colliderToLower.center = .5f * colliderToLower.height * Vector3.up;
                }
                else
                    colliderInPosition = true;
            }

            yield return null;
        }
    }

    #region Speed override.
    void SetSpeedOverrideActive(bool state)
    {
        if (state)
        {
            if (!movement.SpeedOverrides.Contains(SpeedOverride))
                movement.SpeedOverrides.Add(SpeedOverride);
        }
        else
            movement.SpeedOverrides.Remove(SpeedOverride);
    }

    float SpeedOverride() => movementSpeed;
    #endregion
}
