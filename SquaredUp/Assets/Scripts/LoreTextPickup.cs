using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoreTextPickup : MonoBehaviour
{
    public AudioSource pickup; // The SFX for picking up the item
    public GameObject other; // The player game object which is being collided with
    public UnityEngine.UI.Text textBox; // The Text object containing the lore entry
    private bool isCollected = false; // Boolean storing whether the pickup has been collected

    // Start is called before the first frame update
    void Start()
    {
        // Initialize other as the Player object for detecting collision
        other = GameObject.FindGameObjectWithTag("Player");
        textBox.enabled = false;
    }

    // Implementation in which the pickup persists after collection
    // Displays text through enabling / disabling Text object (sets isCollected to true and changes text on first pickup)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isCollected)
        {
            textBox.enabled = true;
            isCollected = true;
            pickup.Play();
        }
        else
        {
            textBox.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        textBox.enabled = false;
        textBox.text = "You have already collected this text!";
    }
}
