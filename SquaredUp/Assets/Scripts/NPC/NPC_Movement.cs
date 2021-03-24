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
        position = 0;
        newLength = paths.Length * 2 - 1;
        route = new int[newLength];

        for(int i = 0;i <= paths.Length; i++)
        {
            Debug.Log();
            route[numSpot] = paths[i];
            numSpot++;
        }

        for (int j = paths.Length - 2;j > 0; j--)
        {
            route[numSpot] = paths[j];
            numSpot++;
            
        }
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
                position++;
                Debug.Log(position);
            }
        }
        else
        {

            stopCounter -= Time.deltaTime;

            NPCRigidBody.velocity = Vector2.zero;

            if (stopCounter < 0)
            {
                if (position > numSpot)
                {
                    walkDirection = route[position];
                    isWalking = true;
                    walkingCounter = walkingTime;
                } else
                {
                    position = 0;
                    walkDirection = route[position];
                    isWalking = true;
                    walkingCounter = walkingTime;
                }
                
            }
        }

    } 
}

