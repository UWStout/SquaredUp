using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionInRange : MonoBehaviour
{

    public GameObject player;
    // public GameObject playerMovementReset;

    [SerializeField] private Transform jailCellLocation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        Debug.Log("change");
        // playerMovementReset.transform.localPosition = new Vector2(0, 0);
        player.transform.position = jailCellLocation.position;
        
    }
}
