using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorState : MonoBehaviour
{
    private int targetedBy=0;
    [SerializeField]
    private Sprite Closed;
    [SerializeField]
    private Sprite Open;
    [SerializeField]
    private GameObject doorAttributes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WithinSight()
    {
        targetedBy++;
        //disable wall
         doorAttributes.SetActive(false);
    }

    public void OutOfSight()
    {
        targetedBy--;
        if (targetedBy == 0){
            //enable wall
            doorAttributes.SetActive(true);
        }
    }
}
