using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionInRange : MonoBehaviour
{

    public GameObject Player;
    public int prisonX;
    public int prisonY;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("enter");
        if(Player.tag == "Player")
        {
            Debug.Log("change");
            Player.transform.localPosition = new Vector2(prisonX, prisonY);
        }
    }
}
