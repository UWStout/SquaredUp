using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTower : MonoBehaviour
{
    private bool hitByCast;
    [SerializeField] Material targetMaterial;
    [SerializeField]
    GameObject wall;
    // Start is called before the first frame update
    void Start()
    {
        hitByCast = false;   
    }

    // Update is called once per frame
    void Update()
    {
        if (hitByCast)
        {
            wall.SetActive(false);
        }
    }

    public void HitByCast(Material matCheck)
    {
        if (!targetMaterial || matCheck.color == targetMaterial.color)
        {
            hitByCast = true;
        }
        else
        {
            hitByCast = false;
        }
    }

    public void VoidCast()
    {
        hitByCast = false;
        wall.SetActive(true);
    }
}
