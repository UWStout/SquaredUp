using System;
using System.Collections.Generic;
using UnityEngine;

public class GuardMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private GuardMovementUnit[] moveUnits = new GuardMovementUnit[0];

    private Vector2 startPosition = Vector2.negativeInfinity;

    private bool shouldMove = true;
    private int curIndex = 0;
    private Vector2 lastMoveDir = Vector2.negativeInfinity;
    private bool isFirstMove = true;
    private float waitTimer = 0;
    private float timeToWait = 0;


    #region UnityMessages
    private void Start()
    {
        startPosition = transform.position;
    }
    private void Update()
    {
        if (moveUnits.Length == 0)
        {
            return;
        }
        if (!shouldMove)
        {
            return;
        }
        if (waitTimer < timeToWait)
        {
            waitTimer += Time.deltaTime;
            return;
        }

        GuardMovementUnit curUnit = moveUnits[curIndex];
        Vector2 curTargetPos = startPosition + curUnit.GetPoint();

        Vector2 curMoveDir = (curTargetPos - (Vector2)transform.position).normalized;
        if (!isFirstMove && curMoveDir != lastMoveDir)
        {
            transform.position = curTargetPos;
            curIndex = GetNextIndex();
            isFirstMove = true;
            waitTimer = 0;
            timeToWait = curUnit.GetWaitTime();
            return;
        }
            
        isFirstMove = false;
        lastMoveDir = curMoveDir;
        transform.position += (Vector3)(curMoveDir * Time.deltaTime * moveSpeed);
    }
    private void OnDrawGizmosSelected()
    {
        Vector2 origin = transform.position;

        Gizmos.color = Color.red;
        foreach (GuardMovementUnit unit in moveUnits)
        {
            if (unit.GetWaitTime() > 0)
            {
                float size = unit.GetWaitTime() * 0.05f;
                size = Mathf.Clamp(size, 0.0f, 0.5f);
                Gizmos.DrawSphere(origin + unit.GetPoint(), size);
            }
        }
        Gizmos.color = Color.yellow;
        if (moveUnits.Length > 0)
        {
            Gizmos.DrawLine(origin, origin + moveUnits[0].GetPoint());
        }
        Gizmos.color = Color.blue;
        for (int i = 1; i < moveUnits.Length; ++i)
        {
            Gizmos.DrawLine(origin + moveUnits[i - 1].GetPoint(), origin + moveUnits[i].GetPoint());
        }
        if (moveUnits.Length > 1)
        {
            Gizmos.DrawLine(origin + moveUnits[moveUnits.Length - 1].GetPoint(), origin + moveUnits[0].GetPoint());
        }
    }
    #endregion UnityMessages


    public void AllowMove(bool condition)
    {
        shouldMove = condition;
    }


    private GuardMovementUnit[] GetGlobalPositionPath()
    {
        List<GuardMovementUnit> globalPos = new List<GuardMovementUnit>(moveUnits.Length);
        for (int i = 0; i < moveUnits.Length; ++i)
        {
            GuardMovementUnit curUnit = moveUnits[i];
            globalPos.Add(new GuardMovementUnit(curUnit.GetPoint() + startPosition, curUnit.GetWaitTime()));
        }
        return globalPos.ToArray();
    }
    private int GetNextIndex()
    {
        int index = curIndex + 1;
        if (index >= moveUnits.Length)
        {
            index = 0;
        }
        return index;
    }
}

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