using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorState : MonoBehaviour
{
    //variables on the door
    private int targetedBy=0;
    [SerializeField]
    private GameObject doorAttributes;


    public void WithinSight()
    {
        //increment the number of targets
        targetedBy++;
        //disable wall
         doorAttributes.SetActive(false);
    }

    public void OutOfSight()
    {
        //decrement the number of targets
        targetedBy--;
        //if wall is not targeted activate wall
        if (targetedBy == 0){
            //enable wall
            doorAttributes.SetActive(true);
        }
    }
}
