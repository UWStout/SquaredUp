using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Movement : MonoBehaviour
{

    public float movementSpeed;

    private Rigidbody2D NPCRigidBody;

    public bool isWalking;

    public float walkingTime;
    private float walkingCounter;
    public float stopTime;
    private float stopCounter;

    public int[] paths;
    private int[] route;
    private int numSpot;
    private int position;
    private int newLength;

    private int walkDirection;

    void Start()
    {
        NPCRigidBody = GetComponent<Rigidbody2D>();

        stopCounter = stopTime;
        walkingCounter = walkingTime;
        numSpot = 0;
        newLength = paths.Length * 2;
        route = new int[newLength];
        Debug.Log(route.Length);


        for (int i = 0;i < paths.Length; i++)
        {
            //Debug.Log();
            route[numSpot] = paths[i];
            numSpot++;
        }

        for (int j = paths.Length - 1;j >= 0; j--)
        {
            Debug.Log(numSpot);
            switch (paths[j])
            {
                case 0:
                    route[numSpot] = 2;
                    break;
                case 1:
                    route[numSpot] = 3;
                    break;
                case 2:
                    route[numSpot] = 0;
                    break;
                case 3:
                    route[numSpot] = 1;
                    break;
            }

            numSpot++;
            
        }
        Debug.Log(numSpot);
        numSpot = 0;
        Debug.Log(numSpot);
    }

    // Update is called once per frame
    void Update()
    {

        if (isWalking)
        {
            walkingCounter -= Time.deltaTime;

            switch (walkDirection)
            {
                case 0:
                    NPCRigidBody.velocity = new Vector2(0, movementSpeed);
                    break;
                case 1:
                    NPCRigidBody.velocity = new Vector2(movementSpeed, 0);
                    break;
                case 2:
                    NPCRigidBody.velocity = new Vector2(0, -movementSpeed);
                    break;
                case 3:
                    NPCRigidBody.velocity = new Vector2(-movementSpeed, 0);
                    break;
            }
            if (walkingCounter < 0)
            {
                isWalking = false;
                stopCounter = stopTime;
                numSpot++;
                Debug.Log(numSpot);
            }
        }
        else
        {

            stopCounter -= Time.deltaTime;

            NPCRigidBody.velocity = Vector2.zero;

            if (stopCounter < 0)
            {
                if (numSpot <= route.Length - 1)
                {
                    walkDirection = route[numSpot];
                    isWalking = true;
                    walkingCounter = walkingTime;
                } else
                {
                    numSpot = 0;
                    walkDirection = route[numSpot];
                    isWalking = true;
                    walkingCounter = walkingTime;
                }
                
            }
        }

    } 
}

