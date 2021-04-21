using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] LayerMask layerMask, raycastLayer;
    private RaycastHit2D hit, oldHit;
    private Vector3 hitByPos, hitPointPos;
    private int fromDir;
    [SerializeField]
    private int[,] angleArray = new int[4, 4] { { -1, 90, 270, -1}, { -1, -1, 180, 0},{ 90, -1, -1, 270},{ 0, 180, -1, -1 } }; 
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
    }
    void FixedUpdate()
    {
        if (lineRenderer.enabled)
        {
            SetLine();
        }
        else if (hit && (hit.collider.gameObject.layer == Mathf.Log(raycastLayer.value, 2)))
        {
            if (hit.collider.gameObject.GetComponent<LaserScript>())
            {
                hit.collider.gameObject.GetComponent<LaserScript>().VoidCast();
            }
            else if (hit.collider.gameObject.GetComponent<EndTower>())
            {
                hit.collider.gameObject.GetComponent<EndTower>().VoidCast();
            }
        }
    }
    public void HitByCast(Vector3 hitBy, Vector3 hitPoint, int from)
    {
        hitByPos = hitBy;
        hitPointPos = hitPoint;
        fromDir = from;
        if (!lineRenderer.enabled)
        {
            SetLine();
        }
    }

    private void SetLine()
    {
        int angleZ = (int)(this.transform.rotation.eulerAngles.z);
        int castAngle = angleArray[(fromDir / 90), (angleZ / 90)];
        if (castAngle != -1)
        {
            lineRenderer.SetPosition(0, hitPointPos);
            lineRenderer.SetPosition(1, Casting( castAngle));
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    public void VoidCast()
    {
        lineRenderer.enabled = false;
        hitByPos = new Vector3();
        //hit.collider.gameObject.GetComponent<LaserScript>().VoidCast();
    }

    public Vector3 Casting(int castAngle_)
    {
        Vector3 rotatedVector = Quaternion.Euler(0f, 0f, castAngle_) * Vector3.up;
        Vector3 modifyStart = Quaternion.Euler(0f, 0f, castAngle_) * new Vector3(0f, .1f, 0f);
        hit = Physics2D.Raycast(hitPointPos + modifyStart, rotatedVector, Mathf.Infinity, ~layerMask);
        if (oldHit && hit && hit.collider.gameObject != oldHit.collider.gameObject)
        {
            if (oldHit.collider.gameObject.layer == Mathf.Log(raycastLayer.value, 2))
            {
                if (oldHit.collider.gameObject.GetComponent<LaserScript>())
                {
                    Debug.Log(oldHit.collider.gameObject.name);
                    oldHit.collider.gameObject.GetComponent<LaserScript>().VoidCast();
                }
                else if (oldHit.collider.gameObject.GetComponent<EndTower>())
                {
                    oldHit.collider.gameObject.GetComponent<EndTower>().VoidCast();
                }
            }
        }
        oldHit = hit;
        if (hit && hit.collider.gameObject.layer == Mathf.Log(raycastLayer.value, 2))
        {
            if (hit.collider.gameObject.GetComponent<LaserScript>())
            {
                hit.collider.gameObject.GetComponent<LaserScript>().HitByCast(this.transform.position, hit.point, castAngle_);
            }
            else if (hit.collider.gameObject.GetComponent<EndTower>())
            {
                hit.collider.gameObject.GetComponent<EndTower>().HitByCast();
            }
        }
        return (Vector3)hit.point + new Vector3(0, 0, -.1f);

    }
}
