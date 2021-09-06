using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DeactivateOnExit : MonoBehaviour
{
    [SerializeField] private GameObject[] deactivateObjs = null;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (GameObject go in deactivateObjs)
            {
                go.SetActive(false);
            }
        }
    }
}
