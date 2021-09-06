using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Canvas))]
public class LoreMenu : MonoBehaviour
{
    [SerializeField] private Canvas loreCanvas; // Canvas on which the lore and menu is displayed
    [SerializeField] private LoreTextPickup[] texts = new LoreTextPickup[3];
    [SerializeField] private UnityEngine.UI.Button[] loreButtons = new UnityEngine.UI.Button[3];
    [SerializeField] private TextMeshProUGUI[] textBoxes = new TextMeshProUGUI[3];

    /*
    private void Update()
    {
        OnKeyboardDown();
    }
    private void OnKeyboardDown()
    {
        // If space is pressed, enable the Canvas and disable player movement
        if(Input.GetKeyDown(KeyCode.Space))
        {
            loreCanvas.gameObject.SetActive(true);
            movement.AllowMovement(false);
            // Check for each lore that is collected and enable corresponding button
            for(int i=0; i<texts.Length; ++i)
            {
               //Debug.Log("iteration" + (i + 1));
                if(texts[i].GetCollected())
                {
                    loreButtons[i].gameObject.SetActive(true);
                 //Debug.Log("Lore " + (i + 1));
                }
            }
      
        }
        // If the lore menu is up and escape is pressed, disable the Canvas and renable player movement
        if(loreCanvas.enabled && Input.GetKeyDown(KeyCode.Backspace))
        {
            for(int i=0; i<texts.Length; ++i)
            {
                textBoxes[i].gameObject.SetActive(false);
            }
            loreCanvas.gameObject.SetActive(false);
            movement.AllowMovement(true);
        }
    }
    */
    public void UpdateLore()
    {
        Debug.Log("Updating lore");
        // Check for each lore that is collected and enable corresponding button
        for (int i = 0; i < texts.Length; ++i)
        {
            //Debug.Log("iteration" + (i + 1));
            if (texts[i] && texts[i].GetCollected())
            {
                loreButtons[i].gameObject.SetActive(true);
                //Debug.Log("Lore " + (i + 1));
            }
        }
    }

}
