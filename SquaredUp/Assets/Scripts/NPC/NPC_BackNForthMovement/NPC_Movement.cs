using UnityEngine;

public class NPC_Movement : MonoBehaviour
{
    private Rigidbody2D rb = null;

    public bool isWalking = true;

    public float walkingSpeed;
    private float walkingCounter = 0.0f;
    public float WalkingCounter
    {
        get { return walkingCounter; }
        set { walkingCounter = value; }
    }
    public float stopTime;
    private float stopCounter = 0.0f;
    public float StopCounter
    {
        get { return stopCounter; }
        set { stopCounter = value; }
    }

    public int[] paths;
    private int[] route = new int[0];

    public float[] movementTime;
    private float[] movementList = new float[0];

    private int numSpot = 0;
    public int NumSpot
    {
        get { return numSpot; }
        set { numSpot = value; }
    }

    public GameObject Vision;

    private bool guardStop = false;
    public bool GuardStop
    {
        get { return guardStop; }
        set { guardStop = value; }
    }


    // Called 0th
    // Set references
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Do this here so it can be overwritten by the load in start
        isWalking = true;
        guardStop = false;
        walkingCounter = movementTime[numSpot];
        stopCounter = stopTime;
    }
    // Called 1st
    // Initialization
    private void Start()
    {
        int newLength = paths.Length * 2;
        route = new int[newLength];

        newLength = movementTime.Length * 2;
        movementList = new float[newLength];
        //Debug.Log(route.Length);

        ExpandPathList(paths, route);

        ExpandMovementList(movementTime, movementList);
    }
    // Update is called once per frame
    private void Update()
    {
        int walkDirection = route[numSpot];
        UpdateFacingDirection(walkDirection);
        if (!guardStop)
        {
            if (isWalking)
            {
                walkingCounter -= Time.deltaTime;
                UpdateVelocity(walkDirection);
                if (walkingCounter < 0)
                {
                    isWalking = false;
                    stopCounter = stopTime;
                }
            }
            else
            {
                rb.velocity = Vector2.zero;

                stopCounter -= Time.deltaTime;
                if (stopCounter < 0)
                {
                    isWalking = true;

                    numSpot++;
                    if (numSpot > route.Length - 1)
                    {
                        numSpot = 0;
                    }
                    walkingCounter = movementList[numSpot];
                }
            }
        }

    }

    public void AllowMove(bool allow)
    {
        guardStop = !allow;
        if (guardStop)
        {
            rb.velocity = Vector2.zero;
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

    private void UpdateFacingDirection(int direction)
    {
        switch (direction)
        {
            case 0:
                Vision.transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case 1:
                Vision.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case 2:
                Vision.transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case 3:
                Vision.transform.rotation = Quaternion.Euler(0, 0, 270);
                break;
            default:
                Debug.LogError("Unhandled WalkDirection");
                break;
        }
    }

    private void UpdateVelocity(int direction)
    {
        switch (direction)
        {
            case 0:
                rb.velocity = new Vector2(0, walkingSpeed);
                break;
            case 1:
                rb.velocity = new Vector2(walkingSpeed, 0);
                break;
            case 2:
                rb.velocity = new Vector2(0, -walkingSpeed);
                break;
            case 3:
                rb.velocity = new Vector2(-walkingSpeed, 0);
                break;
            default:
                Debug.LogError("Unhandled WalkDirection");
                break;
        }
    }
}


