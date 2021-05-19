using UnityEngine;

public class NPC_Movement : MonoBehaviour
{
    private Rigidbody2D rb = null;

    public bool isWalking = false;

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

    private int walkDirection = 0;

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

        walkDirection = route[numSpot];
        UpdateFacingDirection(walkDirection);
    }
    // Update is called once per frame
    private void Update()
    {
        if (!guardStop)
        {
            if (isWalking)
            {
                walkingCounter -= Time.deltaTime;
                switch (walkDirection)
                {
                    case 0:
                        Vision.transform.rotation = Quaternion.Euler(0, 0, 180);
                        rb.velocity = new Vector2(0, walkingSpeed);
                        break;
                    case 1:
                        Vision.transform.rotation = Quaternion.Euler(0, 0, 90);
                        rb.velocity = new Vector2(walkingSpeed, 0);
                        break;
                    case 2:
                        Vision.transform.rotation = Quaternion.Euler(0, 0, 0);
                        rb.velocity = new Vector2(0, -walkingSpeed);
                        break;
                    case 3:
                        Vision.transform.rotation = Quaternion.Euler(0, 0, 270);
                        rb.velocity = new Vector2(-walkingSpeed, 0);
                        break;
                    default:
                        Debug.LogError("Unhandled WalkDirection");
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

                rb.velocity = Vector2.zero;

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
}


