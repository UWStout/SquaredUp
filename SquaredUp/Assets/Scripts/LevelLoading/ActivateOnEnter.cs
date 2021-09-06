using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ActivateOnEnter : MonoBehaviour
{
    [SerializeField] private GameObject[] activateObjs = null;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (GameObject go in activateObjs)
            {
                go.SetActive(true);
            }
        }
    }
}
