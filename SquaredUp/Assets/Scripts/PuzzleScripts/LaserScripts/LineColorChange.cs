using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineColorChange : MonoBehaviour
{

    private LineRenderer lineRenderer;
    [SerializeField] GameObject findMaterial;
    private Material thisMaterial;
    [SerializeField] private LayerMask raycastLayer, layerMask;
    private RaycastHit2D hit, oldHit;
    private Vector3 hitByPos, hitPointPos,rotatedVector;
    private int fromDir;
    [SerializeField]
    private int[,] angleArray = new int[4, 4] { { -1, 90, 270, -1 }, { -1, -1, 180, 0 }, { 90, -1, -1, 270 }, { 0, 180, -1, -1 } };
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = true;
        thisMaterial = findMaterial.GetComponent<MeshRenderer>().material;
        lineRenderer.material = thisMaterial;
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
            else if (hit.collider.gameObject.GetComponent<LineColorChange>())
            {
                hit.collider.gameObject.GetComponent<LineColorChange>().VoidCast();
            }
        }
    }
    public void HitByCast(Vector3 hitBy, Vector3 hitPoint, int from, Vector3 rotatedVector_)
    {
        hitByPos = hitBy;
        hitPointPos = hitPoint;
        fromDir = from;
        rotatedVector = rotatedVector_;
        if (!lineRenderer.enabled)
        {
            SetLine();
        }
    }

    private void SetLine()
    {
        lineRenderer.SetPosition(0, hitPointPos);
        lineRenderer.SetPosition(1, Casting());
        lineRenderer.material = thisMaterial;
        lineRenderer.enabled = true;
    }

    public void VoidCast()
    {
        lineRenderer.enabled = false;
        hitByPos = new Vector3();
    }

    public Vector3 Casting()
    {
        
        Vector3 modifyStart = Quaternion.Euler(0f, 0f, fromDir) * new Vector3(0f, 2*this.transform.localScale.y, 0f);
        hit = Physics2D.Raycast(hitPointPos+modifyStart, rotatedVector, Mathf.Infinity, layerMask);
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
                else if (oldHit.collider.gameObject.GetComponent<LineColorChange>())
                {
                    oldHit.collider.gameObject.GetComponent<LineColorChange>().VoidCast();
                }
            }
        }
        oldHit = hit;
        if (hit && hit.collider.gameObject.layer == Mathf.Log(raycastLayer.value, 2))
        {
            if (hit.collider.gameObject.GetComponent<LaserScript>())
            {
                hit.collider.gameObject.GetComponent<LaserScript>().HitByCast(this.transform.position, hit.point, fromDir, lineRenderer.material, layerMask);
            }
            else if (hit.collider.gameObject.GetComponent<EndTower>())
            {
                hit.collider.gameObject.GetComponent<EndTower>().HitByCast(lineRenderer.material);
            }
            else if (hit.collider.gameObject.GetComponent<LineColorChange>())
            {
                hit.collider.gameObject.GetComponent<LineColorChange>().HitByCast(rotatedVector, hit.point, fromDir, rotatedVector);
            }
        }
        return hit.point;

    }
}
