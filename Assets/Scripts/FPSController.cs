using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    //Variables------------------------------------------------------------------
    float mYaw;
    float mPitch;
    [Header("Rotation")]
    [SerializeField] GameObject mPitchController;
    [SerializeField] float mSpeedYaw;
    [SerializeField] float mSpeedPitch;
    [SerializeField] float mMinPitch;
    [SerializeField] float mMaxPitch;
    [SerializeField] bool mInvertPitch;
    [SerializeField] bool mInvertYaw;
    [SerializeField] bool m_AngleLocked;
    [SerializeField] bool m_AimLocked;

    [Header("Move")]
    [SerializeField] CharacterController mCharacterController;
    [SerializeField] float mMoveSpeed;
    [SerializeField] float mRunMultiplier;

    [Header("Keys")]
    public KeyCode mForwardKey;
    public KeyCode mBackwardKey;
    public KeyCode mRightKey;
    public KeyCode mLeftKey;
    public KeyCode mRunKey;
    public KeyCode mJumpKey;
    public KeyCode m_DebugLockAngleKey = KeyCode.I;
    public KeyCode m_DebugLockKey = KeyCode.O;

    [Header("Gravity and Jump")]
    [SerializeField] bool mOnGround;
    [SerializeField] bool mContactCeiling;

    [SerializeField] float mMaxJumpHeight;
    [SerializeField] float mDistanceJump;

    [SerializeField] float mGravity;
    [SerializeField] float mGravityMultiplier;
    [SerializeField] float mTimeToPeak;
    [SerializeField] float distanceToPeak;

    [SerializeField] float mVerticalSpeed;
    [SerializeField] float mJumpSpeed;

    bool gotKey = false;


    //Functions--------------------------------------------------------------------
    private void Awake()
    {
        //Initial set for translation and rotation
        recalcualteYawAndPitch();

        //Initial set gravity and speed to jump
        mTimeToPeak = (mDistanceJump / 2) / mMoveSpeed;
        mGravity = -(2 * mMaxJumpHeight) / Mathf.Pow(mTimeToPeak, 2);
        mJumpSpeed = (2 * mMaxJumpHeight * mMoveSpeed) / (mDistanceJump / 2);
    }


    private void FixedUpdate()
    {
        if (!m_AngleLocked)
        {
            Rotate();
        }
        Move();

        
    }

    private void Update()
    {
        if (Input.GetKeyDown(m_DebugLockAngleKey))
        {
            m_AngleLocked = !m_AngleLocked;
        }
        if (Input.GetKeyDown(m_DebugLockKey))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            m_AimLocked = Cursor.lockState == CursorLockMode.Locked;
        }
    }

    private void Move()
    {
        Vector3 forward = new Vector3(Mathf.Sin(mYaw * Mathf.Deg2Rad), 0.0f, Mathf.Cos(mYaw * Mathf.Deg2Rad));
        Vector3 right = new Vector3(Mathf.Sin((mYaw + 90.0f) * Mathf.Deg2Rad), 0.0f, Mathf.Cos( (mYaw + 90.0f) * Mathf.Deg2Rad));
        Vector3 lMovement = new Vector3();

        if (Input.GetKey(mForwardKey)) { lMovement = forward; }
        else if (Input.GetKey(mBackwardKey)) { lMovement -= forward; }
        if (Input.GetKey(mRightKey)) { lMovement += right; }
        else if (Input.GetKey(mLeftKey)) { lMovement -= right; }

        if (mOnGround && Input.GetKey(mJumpKey)) mVerticalSpeed = mJumpSpeed;

        lMovement.Normalize();

        lMovement *= mMoveSpeed * Time.fixedDeltaTime * (Input.GetKey(mRunKey) ? mRunMultiplier : 1.0f);

        mVerticalSpeed += mGravity * Time.fixedDeltaTime;

        lMovement.y = mVerticalSpeed * Time.fixedDeltaTime;

        CollisionFlags colls = mCharacterController.Move(lMovement);

        mOnGround = (colls & CollisionFlags.Below) != 0;
        mContactCeiling = (colls & CollisionFlags.Above) != 0;

        if (mOnGround) mVerticalSpeed = 0.0f;
        if (mContactCeiling && mVerticalSpeed > 0.0f) mVerticalSpeed = 0.0f;
    }

    private void Rotate()
    {
        float xMouseAxis = Input.GetAxis("Mouse X");
        float yMouseAxis = Input.GetAxis("Mouse Y");

        mYaw += xMouseAxis * mSpeedYaw * (mInvertYaw ? -1 : 1); 
        mPitch -= yMouseAxis * mSpeedPitch * (mInvertPitch ? -1 : 1);
        mPitch = Mathf.Clamp(mPitch, mMinPitch, mMaxPitch);
        transform.rotation = Quaternion.Euler(0.0f, mYaw, 0.0f);
        mPitchController.transform.localRotation = Quaternion.Euler(mPitch, 0.0f, 0.0f);
    }
    
    public void setKey()
    {
        gotKey = true;
    }
    public bool getKey()
    {
        return gotKey;
    }
    public void recalcualteYawAndPitch()
    {
        mYaw = transform.rotation.eulerAngles.y;
        mPitch = mPitchController.transform.rotation.eulerAngles.x;
        if (mPitch > 180) mPitch -= 360;
    }
}
