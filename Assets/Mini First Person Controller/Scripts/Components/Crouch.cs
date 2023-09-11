using System.Collections;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    public KeyCode key = KeyCode.LeftControl;

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
        // Try to get components.
        movement = GetComponentInParent<FirstPersonMovement>();
        headToLower = movement.GetComponentInChildren<Camera>().transform;
        colliderToLower = movement.GetComponentInChildren<CapsuleCollider>();
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(key))
        {
            if (headToLower)
            {
                if (!defaultHeadYLocalPosition.HasValue)
                    defaultHeadYLocalPosition = headToLower.localPosition.y;

                //headToLower.localPosition = new Vector3(headToLower.localPosition.x, crouchYHeadPosition, headToLower.localPosition.z);
            }

            float loweringAmount = 0;

            if (colliderToLower)
            {
                if (!defaultColliderHeight.HasValue)
                    defaultColliderHeight = colliderToLower.height;

                if(defaultHeadYLocalPosition.HasValue)
                    loweringAmount = defaultHeadYLocalPosition.Value - crouchYHeadPosition;
                else
                    loweringAmount = defaultColliderHeight.Value * .5f;

                //colliderToLower.height = Mathf.Max(defaultColliderHeight.Value - loweringAmount, 0);
                //colliderToLower.center = .5f * colliderToLower.height * Vector3.up;
            }

            StopAllCoroutines();
            StartCoroutine(Crouching(defaultColliderHeight.Value - loweringAmount, crouchYHeadPosition));

            //if (!IsCrouched)
            //{
                //IsCrouched = true;
                SetSpeedOverrideActive(true);
                CrouchStart?.Invoke();
            //}
        }

        

        if(Input.GetKeyUp(key))
        {
            //if (IsCrouched)
            {
                if (headToLower)
                {
                    //headToLower.localPosition = new Vector3(headToLower.localPosition.x, defaultHeadYLocalPosition.Value, headToLower.localPosition.z);
                }

                if (colliderToLower)
                {
                    //colliderToLower.height = defaultColliderHeight.Value;
                    //colliderToLower.center = .5f * colliderToLower.height * Vector3.up;
                }

                    StopAllCoroutines();
                    StartCoroutine(Crouching(defaultColliderHeight.Value, defaultHeadYLocalPosition.Value));
                //IsCrouched = false;
                SetSpeedOverrideActive(false);
                CrouchEnd?.Invoke();
            }
        }
    }


    IEnumerator Crouching(float colliderHeight, float headPosition)
    {
        //while(!Mathf.Approximately(colliderToLower.height, colliderHeight))
        //{
        //    colliderToLower.height = Mathf.MoveTowards(colliderToLower.height, colliderHeight, Time.deltaTime);
        //    yield return null;
        //}
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
        // Stop if there is no movement component.
        if(!movement)
        {
            return;
        }

        // Update SpeedOverride.
        if (state)
        {
            // Try to add the SpeedOverride to the movement component.
            if (!movement.speedOverrides.Contains(SpeedOverride))
            {
                movement.speedOverrides.Add(SpeedOverride);
            }
        }
        else
        {
            // Try to remove the SpeedOverride from the movement component.
            if (movement.speedOverrides.Contains(SpeedOverride))
            {
                movement.speedOverrides.Remove(SpeedOverride);
            }
        }
    }

    float SpeedOverride() => movementSpeed;
    #endregion
}
