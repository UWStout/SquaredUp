using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform followTarget = null;

    // LateUpdate is called once per end of frame
    void LateUpdate()
    {
        Vector3 pos = followTarget.position;
        pos.y = this.transform.position.y;
        this.transform.position = pos;
    }
}
