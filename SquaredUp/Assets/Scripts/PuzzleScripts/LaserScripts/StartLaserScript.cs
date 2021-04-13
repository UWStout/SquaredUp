using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLaserScript : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] LayerMask layerMask1, raycastLayer;
    private RaycastHit2D oldHit;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        lineRenderer.useWorldSpace = true;
    }

    void FixedUpdate()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, Casting());
    }

    public Vector3 Casting()
    {
        float angleZ = this.transform.rotation.eulerAngles.z;
        Vector3 rotatedVector = Quaternion.Euler(0f, 0f, angleZ) * Vector3.up;
        Vector3 modifyStart = Quaternion.Euler(0f, 0f, angleZ) * new Vector3(0f, 1f, 0f);
        RaycastHit2D hit = Physics2D.Raycast(transform.position+modifyStart, rotatedVector, Mathf.Infinity, ~(layerMask1));
        Debug.Log((int)angleZ);
        if (hit.collider.gameObject.layer == Mathf.Log(raycastLayer.value, 2))
        {
            hit.collider.gameObject.GetComponent<LaserScript>().HitByCast(this.transform.position,hit.point,(int)angleZ);
            oldHit = hit;
            return hit.point;
        }
        if (oldHit&&oldHit.collider.gameObject.layer == Mathf.Log(raycastLayer.value, 2))
        {
            oldHit.collider.gameObject.GetComponent<LaserScript>().VoidCast();
        }
        return hit.point;
    }
}
