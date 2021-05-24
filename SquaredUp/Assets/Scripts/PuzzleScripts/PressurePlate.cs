using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    // Obstacle being controlled by pressure plate
    [SerializeField] private GameObject wall = null;
    // AudioSource to play the click sound for pressing down on the pressure plate
    [SerializeField] private AudioSource pressDownAudio = null;
    // AudioSource to play the release sound for when everything leaves the pressure plate
    [SerializeField] private AudioSource releaseAudio = null;
    // Reference to the pressure plate sprite renderer
    [SerializeField] private SpriteRenderer pressurePlateSpriteRend = null;
    // Color to change the pressure plate when it is triggered
    [SerializeField] private Color pressedColor = Color.white;
    // Amount of objects in the pressure plate's trigger
    private int amountIn = 0;
    // Amount of things that were previously on the pressure plate
    private int previousAmountIn = 0;
    // Starting color of the pressure plate
    private Color defaultColor = Color.white;


    // Called 1st
    // Initialization
    private void Start()
    {
        defaultColor = pressurePlateSpriteRend.color;
    }
    // Called when this object's trigger collides with another physics object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Increment how many objects are in the trigger and hide the wall
        ++amountIn;
        // When we go from 0 to 1 thing pushing down, something has begun pushing down
        if (amountIn == 1)
        {
            wall.SetActive(false);
            pressurePlateSpriteRend.color = pressedColor;

            
            // If we now have the previous amount, reset the amount
            if (amountIn == previousAmountIn)
            {
                ResetPreviousAmount();
            }
            // Don't play the audio until we have put more on the amount than was there previously
            else if (amountIn > previousAmountIn)
            {
                pressDownAudio.Play();
            }
        }
    }
    // Called when this object's trigger stops colliding with another physics object
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Decreemnt how many objects are in the trigger and show the wall if all objects have left
        --amountIn;
        // When nothing remains pushing the plate down
        if (amountIn <= 0)
        {
            wall.SetActive(true);
            pressurePlateSpriteRend.color = defaultColor;
            amountIn = 0;
            releaseAudio.Play();
        }
        // Gets rid of the previous amount
        ResetPreviousAmount();
    }


    /// <summary>
    /// Set the amount of things holding the pressure plate down.
    /// Optionally play the audio associated.
    /// </summary>
    /// <param name="prevAmount">Amount of things that were holding the pressure plate down.</param>
    public void SetPreviousAmountIn(int prevAmount)
    {
        previousAmountIn = prevAmount;

        // Push pressure plate down
        if (amountIn == 1)
        {
            wall.SetActive(false);
            pressurePlateSpriteRend.color = pressedColor;
        }
        // Put pressure plate up
        else if (amountIn <= 0)
        {
            wall.SetActive(true);
            pressurePlateSpriteRend.color = defaultColor;
        }
    }
    /// <summary>
    /// Gets the amount of things holding the pressure plate down.
    /// </summary>
    /// <returns>Amount of things holding the pressure plate down.</returns>
    public int GetAmountIn()
    {
        return amountIn;
    }


    /// <summary>
    /// Resets the amount of things that were holding the pressure plate down.
    /// </summary>
    private void ResetPreviousAmount()
    {
        previousAmountIn = 0;
    }
}
