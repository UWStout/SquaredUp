using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridHittable : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="hit"></param>
    /// <returns>If the mover can move after hitting this.</returns>
    public abstract bool Hit(GridHit hit);
}
