using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Movement : MonoBehaviour
{

    private Rigidbody2D NPCRigidBody;

    public bool isWalking;

    public float walkingSpeed;
    private float walkingCounter;
    public float stopTime;
    private float stopCounter;

    public int[] paths;
    private int[] route;

    public float[] movementTime;
    private float[] movementList;

    private int numSpot;
    private int position;
    private int newLength;

    private int walkDirection;

    public GameObject Vision;

    void Start()
    {
        NPCRigidBody = GetComponent<Rigidbody2D>();

        stopCounter = stopTime;
        walkingCounter = walkingSpeed;
        numSpot = 0;
        
        newLength = paths.Length * 2;
        route = new int[newLength];

        newLength = movementTime.Length * 2;
        movementList = new float[newLength];
        //Debug.Log(route.Length);

        ExpandPathList(paths, route);

        ExpandMovementList(movementTime, movementList);


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
                    Vision.transform.localPosition = new Vector2(0, 2);
                    Vision.transform.rotation = Quaternion.Euler(0,0,180);
                    NPCRigidBody.velocity = new Vector2(0, walkingSpeed);
                    break;
                case 1:
                    Vision.transform.localPosition = new Vector2(2, 0);
                    Vision.transform.rotation = Quaternion.Euler(0, 0, 90);
                    NPCRigidBody.velocity = new Vector2(walkingSpeed, 0);
                    break;
                case 2:
                    Vision.transform.localPosition = new Vector2(0, -2);
                    Vision.transform.rotation = Quaternion.Euler(0, 0, 0);
                    NPCRigidBody.velocity = new Vector2(0, -walkingSpeed);
                    break;
                case 3:
                    Vision.transform.localPosition = new Vector2(-2, 0);
                    Vision.transform.rotation = Quaternion.Euler(0, 0, 270);
                    NPCRigidBody.velocity = new Vector2(-walkingSpeed, 0);
                    break;
            }
            if (walkingCounter < 0)
            {
                isWalking = false;
                stopCounter = stopTime;
                numSpot++;
                //Debug.Log(numSpot);
            }
        }
        else
        {

            stopCounter -= Time.deltaTime;

            NPCRigidBody.velocity = Vector2.zero;

            if (stopCounter < 0)
            {
                if (numSpot > route.Length - 1)
                {
                    numSpot = 0;
                }
                walkDirection = route[numSpot];
                isWalking = true;
                walkingCounter = movementList[numSpot];
                
            }
        }

    } 

    private void ExpandPathList(int[] input, int[] output)
    {
        for (int i = 0; i < input.Length; i++)
        {
            output[numSpot] = input[i];
            numSpot++;
        }

        for (int j = input.Length - 1; j >= 0; j--)
        {
            switch (paths[j])
            {
                case 0:
                    output[numSpot] = 2;
                    break;
                case 1:
                    output[numSpot] = 3;
                    break;
                case 2:
                    output[numSpot] = 0;
                    break;
                case 3:
                    output[numSpot] = 1;
                    break;
            }

            numSpot++;

        }
        numSpot = 0;
    }

    private void ExpandMovementList(float[] input, float[] output)
    {
        for (int i = 0; i < input.Length; i++)
        {
           // Debug.Log(numSpot);
            output[numSpot] = input[i];
            numSpot++;
        }

        for (int j = input.Length - 1; j >= 0; j--)
        {
            switch (paths[j])
            {
                case 0:
                    output[numSpot] = input[j];
                    break;
                case 1:
                    output[numSpot] = input[j];
                    break;
                case 2:
                    output[numSpot] = input[j];
                    break;
                case 3:
                    output[numSpot] = input[j];
                    break;
            }

            numSpot++;

        }
        numSpot = 0;
    }
}


