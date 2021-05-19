using UnityEngine;

public class NPC_MovementLoop : MonoBehaviour
{
    private Rigidbody2D rb = null;

    public bool isWalking;

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

    public float[] movementTime;

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
        walkDirection = paths[numSpot];
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
        guardStop = !allow;
        if (guardStop)
        {
            rb.velocity = Vector2.zero;
        }
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


