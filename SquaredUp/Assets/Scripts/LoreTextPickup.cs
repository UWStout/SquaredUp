using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoreTextPickup : MonoBehaviour
{
    [SerializeField] private AudioSource pickup; // The SFX for picking up the item
    [SerializeField] private GameObject other; // The player game object which is being collided with
    [SerializeField] private TextMeshProUGUI textBox; // The TextMeshProGUI object containing the lore entry
    private bool isCollected = false; // Boolean storing whether the pickup has been collected

    // Start is called before the first frame update
    void Start()
    {
        // Initialize other as the Player object for detecting collision
        other = GameObject.FindGameObjectWithTag("Player");
        textBox.gameObject.SetActive(false);
    }

    // Implementation in which the pickup persists after collection
    // Displays text through enabling / disabling Text object (sets isCollected to true and changes text on first pickup)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isCollected)
        {
            textBox.gameObject.SetActive(true);
            isCollected = true;
            pickup.Play();
        }
        else
        {
            textBox.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        textBox.gameObject.SetActive(false);
        textBox.text = "You have already collected this text!";
    }

    // Simple getter for isCollected boolean (used to check for collected lore in LoreMenu)
    public bool GetCollected()
    {
        return this.isCollected;
    }

}
