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
            lineRenderer.SetPosition(0, hitPointPos);
            lineRenderer.SetPosition(1, Casting());
        }
        else if (hit && (hit.collider.gameObject.layer == Mathf.Log(raycastLayer.value, 2)))
        {
            hit.collider.gameObject.GetComponent<LaserScript>().VoidCast();
        }
    }
    public void HitByCast(Vector3 hitBy, Vector3 hitPoint, int from)
    {
        hitByPos = hitBy;
        hitPointPos = hitPoint;
        fromDir = from;
        if (!lineRenderer.enabled)
        {
            Casting();
        }
    }

    public void VoidCast()
    {
        lineRenderer.enabled = false;
        hitByPos = new Vector3();
        //hit.collider.gameObject.GetComponent<LaserScript>().VoidCast();
    }

    public Vector3 Casting()
    {

        int angleZ = (int)(this.transform.rotation.eulerAngles.z);
        int castAngle = angleArray[(fromDir/90), (angleZ/90)];
        if (castAngle != -1)
        {
            Vector3 rotatedVector = Quaternion.Euler(0f, 0f, castAngle) * Vector3.up;
            Vector3 modifyStart = Quaternion.Euler(0f, 0f, castAngle) * new Vector3(0f, .1f, 0f);
            hit = Physics2D.Raycast(hitPointPos + modifyStart, rotatedVector, Mathf.Infinity, ~layerMask);
            lineRenderer.enabled = true;
        }
        else { lineRenderer.enabled = false; }
        
        if (oldHit && hit && hit.collider.gameObject != oldHit.collider.gameObject)
        {
            if (oldHit.collider.gameObject.layer == Mathf.Log(raycastLayer.value, 2))
            {
                if (oldHit.collider.gameObject.GetComponent<LaserScript>())
                {
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
                hit.collider.gameObject.GetComponent<LaserScript>().HitByCast(this.transform.position, hit.point, castAngle);
            }
            else if (hit.collider.gameObject.GetComponent<EndTower>())
            {
                hit.collider.gameObject.GetComponent<EndTower>().HitByCast();
            }
        }
        return hit.point;

    }
}
