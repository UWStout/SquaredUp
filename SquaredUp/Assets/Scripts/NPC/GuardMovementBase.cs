using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for moving and rotating guards.
/// </summary>
[DisallowMultipleComponent]
public abstract class GuardMovementBase : MonoBehaviour, IGuardMovement
{
    [SerializeField] private Transform vision = null;
    [SerializeField] private GuardMovementUnit[] moveUnits = new GuardMovementUnit[0];

    public bool CheckShouldMove() => shouldMove;
    private bool shouldMove = true;
    public int GetCurIndex() => curIndex;
    private int curIndex = 0;
    public float GetWaitTimer() => waitTimer;
    private float waitTimer = 0;
    public float GetTimeToWait() => timeToWait;
    private float timeToWait = 0;


    #region UnityMessages
    // Called 0th
    // Domestic Initialization
    private void Update()
    {
        // Don't move if we have no path
        if (moveUnits.Length == 0)
        {
            return;
        }
        // Don't move if we shouldn't
        if (!shouldMove)
        {
            return;
        }
        // Wait
        if (waitTimer < timeToWait)
        {
            waitTimer += Time.deltaTime;
            return;
        }

        GuardMovementUnit curUnit = moveUnits[curIndex];
        Move(curUnit);
    }
    #endregion UnityMessages


    #region Public
    public void AllowMove(bool condition)
    {
        shouldMove = condition;
    }
    #endregion Public


    #region Abstract/Virtual
    protected abstract void Move(GuardMovementUnit curUnit);
    #endregion Abstract/Virtual


    #region Protected
    /// <summary>
    /// Has the guard has the given direction.
    /// </summary>
    /// <param name="faceDir"></param>
    protected void FaceDirection(Vector2 faceDir)
    {
        float angle = Mathf.Atan2(faceDir.y, faceDir.x) * Mathf.Rad2Deg;
        vision.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        vision.eulerAngles += new Vector3(0, 0, 90);
    }
    /// <summary>
    /// Gets the next index in the move units array.
    /// </summary>
    protected int GetNextIndex()
    {
        int index = curIndex + 1;
        if (index >= moveUnits.Length)
        {
            index = 0;
        }
        return index;
    }
    /// <summary>
    /// Increments the index to the next one and updates the waiting timer
    /// using the given unit.
    /// </summary>
    protected void IncrementNextUnit(GuardMovementUnit unitReached)
    {
        curIndex = GetNextIndex();
        waitTimer = 0;
        timeToWait = unitReached.GetWaitTime();
    }

    protected IReadOnlyList<GuardMovementUnit> GetMovementUnits() => moveUnits;

    /// <summary>
    /// Loads in the given runtime variables.
    /// </summary>
    protected void Load(bool lShouldMove, int lCurIndex, float lWaitTimer, float lTimeToWait)
    {
        shouldMove = lShouldMove;
        curIndex = lCurIndex;
        waitTimer = lWaitTimer;
        timeToWait = lTimeToWait;
    }
    #endregion Protected
}

/// <summary>
/// A unit with a Vector3 for the guard to move based on and then the amount of time to wait
/// after the guard has reached that point.
/// </summary>
[Serializable]
public struct GuardMovementUnit
{
    [SerializeField] private Vector2 moveToPoint;
    [SerializeField] [Min(0.0f)] private float waitTime;


    public GuardMovementUnit(Vector2 point, float wait)
    {
        moveToPoint = point;
        waitTime = wait;
    }


    public Vector2 GetPoint() => moveToPoint;
    public float GetWaitTime() => waitTime;
}
