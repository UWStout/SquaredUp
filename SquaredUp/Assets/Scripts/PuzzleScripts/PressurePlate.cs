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
    // Starting color of the pressure plate
    [SerializeField] private Color defaultColor = Color.white;
    // Color to change the pressure plate when it is triggered
    [SerializeField] private Color pressedColor = Color.white;
    // Amount of objects in the pressure plate's trigger
    private int amountIn = 0;
    // Amount of things that were previously on the pressure plate
    private int previousAmountIn = 0;


    // Called when this object's trigger collides with another physics object
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Increment how many objects are in the trigger and hide the wall
        ++amountIn;
        // When we go from 0 to 1 things pushing down, something has begun pushing down
        if (amountIn == 1)
        {
            wall.SetActive(false);
            pressurePlateSpriteRend.color = pressedColor;


            // For not playing the sound on loading a save state
            // If we now have the previous amount, reset the previous amount
            if (amountIn == previousAmountIn)
            {
                ResetPreviousAmount();
            }
            // Don't play the audio until we have put more on the amount than was there previously
            else if (amountIn > previousAmountIn)
            {
                PlaySoundIfVisible(pressDownAudio);
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
            PlaySoundIfVisible(releaseAudio);
        }
        // Gets rid of the previous amount
        ResetPreviousAmount();
    }


    /// <summary>
    /// Set the amount of things holding the pressure plate down.
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
    /// <summary>
    /// Plays the given AudioSource if it is visible on the screen.
    /// </summary>
    /// <param name="sound">Sound to play.</param>
    private void PlaySoundIfVisible(AudioSource sound)
    {
        if (pressurePlateSpriteRend.isVisible)
        {
            sound.Play();
        }
    }
}
