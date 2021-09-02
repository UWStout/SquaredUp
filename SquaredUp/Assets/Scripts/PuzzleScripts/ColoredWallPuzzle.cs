using System;
using System.Collections;
using UnityEngine;

public class ColoredWallPuzzle : Interactable
{
    private static event Action<GameObject> colorWallsTurnedOffEvent;

    //variables for coloredWall
    [SerializeField] private GameObject tilemapDisable;
    [SerializeField] private GameObject[] otherTileMaps;

    [SerializeField] private SpriteRenderer spriteRend = null; 
    [SerializeField] private Color activeColor = Color.white;
    [SerializeField] private Color inactiveColor = Color.white;
    [SerializeField] [Min(0.001f)] private float colorChangeSpeed = 0.05f;

    private bool isColorCoroutineActive = false;
    private Coroutine activeColorCoroutine = null;


    // Called on this component enabled
    private void OnEnable()
    {
        colorWallsTurnedOffEvent += OnColorWallsTurnedOff;
    }
    // Called on this component disabled
    private void OnDisable()
    {
        colorWallsTurnedOffEvent -= OnColorWallsTurnedOff;
    }


    //override interactable
    public override void InteractAbstract()
    {
        //set other tilemaps active
        foreach(GameObject g in otherTileMaps)
        {
            g.SetActive(true);
        }
        //disable target tilemap
        tilemapDisable.SetActive(false);
        colorWallsTurnedOffEvent?.Invoke(tilemapDisable);
    }

    //function to get tilemaps when gameobject is created
    public void SetTilemap(GameObject disable, GameObject enable1, GameObject enable2)
    {
        tilemapDisable = disable;
        otherTileMaps = new GameObject[2];
        otherTileMaps[0] = enable1;
        otherTileMaps[1] = enable2;
    }


    /// <summary>
    /// Called when an instance of this object turns a colored wall tilemap active/inactive.
    /// Changes the color of this 
    /// </summary>
    /// <param name="tilemapThatWasDisabled"></param>
    private void OnColorWallsTurnedOff(GameObject tilemapThatWasDisabled)
    {
        if (tilemapThatWasDisabled == tilemapDisable)
        {
            StartChangeColorCoroutine(inactiveColor);
        }
        else
        {
            StartChangeColorCoroutine(activeColor);
        }
    }
    private void StartChangeColorCoroutine(Color targetColor)
    {
        if (isColorCoroutineActive)
        {
            StopCoroutine(activeColorCoroutine);
        }
        activeColorCoroutine = StartCoroutine(ChangeColorCoroutine(targetColor));
    }
    private IEnumerator ChangeColorCoroutine(Color targetColor)
    {
        isColorCoroutineActive = true;

        Color startColor = spriteRend.color;

        float t = 0;
        // Start changing the colors
        while (t < 1)
        {
            spriteRend.color = Color.Lerp(startColor, targetColor, t);

            // Step
            t += colorChangeSpeed * Time.deltaTime;

            yield return null;
        }
        spriteRend.color = targetColor;

        isColorCoroutineActive = false;
    }
}
