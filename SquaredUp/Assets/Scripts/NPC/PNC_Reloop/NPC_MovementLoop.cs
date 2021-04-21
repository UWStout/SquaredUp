using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_MovementLoop : MonoBehaviour
{
    private const float VISION_LENGTH = 8;

    private Rigidbody2D NPCRigidBody;

    public bool isWalking;

    public float walkingSpeed;
    private float walkingCounter;
    public float stopTime;
    private float stopCounter;

    public int[] paths;

    public float[] movementTime;

    private int numSpot;
    private int position;

    private int walkDirection;

    public GameObject Vision;

    bool gaurdStop = false;

    void Start()
    {
        NPCRigidBody = GetComponent<Rigidbody2D>();

        stopCounter = stopTime;
        walkingCounter = walkingSpeed;
        numSpot = 0;

        //Debug.Log(route.Length);


    }

    // Update is called once per frame
    void Update()
    {
        if (!gaurdStop)
        {
            if (isWalking)
            {
                walkingCounter -= Time.deltaTime;
                switch (walkDirection)
                {
                    case 0:
                        Vision.transform.localPosition = new Vector2(0, VISION_LENGTH);
                        Vision.transform.rotation = Quaternion.Euler(0, 0, 180);
                        NPCRigidBody.velocity = new Vector2(0, walkingSpeed);
                        break;
                    case 1:
                        Vision.transform.localPosition = new Vector2(VISION_LENGTH, 0);
                        Vision.transform.rotation = Quaternion.Euler(0, 0, 90);
                        NPCRigidBody.velocity = new Vector2(walkingSpeed, 0);
                        break;
                    case 2:
                        Vision.transform.localPosition = new Vector2(0, -VISION_LENGTH);
                        Vision.transform.rotation = Quaternion.Euler(0, 0, 0);
                        NPCRigidBody.velocity = new Vector2(0, -walkingSpeed);
                        break;
                    case 3:
                        Vision.transform.localPosition = new Vector2(-VISION_LENGTH, 0);
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
                    if (numSpot > paths.Length - 1)
                    {
                        numSpot = 0;
                    }
                    walkDirection = paths[numSpot];
                    isWalking = true;
                    walkingCounter = movementTime[numSpot];

                }
            }
        }

    }

    public void AllowMove(bool allow)
    {
        gaurdStop = !allow;
        if (gaurdStop)
        {
            NPCRigidBody.velocity = Vector2.zero;
        }
    }


}


