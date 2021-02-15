using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteracatable : Interactable
{
    [SerializeField]
    private MeshRenderer meshRendRef = null;
    [SerializeField]
    private Material mainMat = null;
    [SerializeField]
    private Material otherMat = null;

    private bool isMain = true;

    public override void Interact()
    {
        base.Interact();
        isMain = !isMain;
        if (isMain)
        {
            meshRendRef.material = mainMat;
        } else
        {
            meshRendRef.material = otherMat;
        }
    }
}
