using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaintainOutlineWidth : MonoBehaviour
{
    [SerializeField] private Transform body = null;
    [SerializeField] private Transform outline = null;
    [SerializeField] private float outlineWidth = 0.2f;

    // Start is called before the first frame update
    private void Start()
    {
        Vector3 bodyTotalScale = GetTotalScale(body);
        Vector3 outlineTotalScale = GetTotalScale(outline);
    }

    private Vector3 GetTotalScale(Transform startTrans)
    {
        Vector3 totalScale = outline.localScale;
        Transform nextParent = body.parent;
        while (nextParent != null)
        {
            totalScale = Vector3.Scale(totalScale, nextParent.localScale);
            nextParent = nextParent.parent;
        }
        return totalScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
