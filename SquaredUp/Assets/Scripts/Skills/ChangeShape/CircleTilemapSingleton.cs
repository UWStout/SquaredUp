using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Singleton to control the circle tilemap
/// </summary>
[RequireComponent(typeof(Tilemap))]
public class CircleTilemapSingleton : MonoBehaviour
{
    // Singleton
    private static CircleTilemapSingleton instance = null;
    public static CircleTilemapSingleton Instance { get { return instance; } }

    // Speed of the transparency transition
    [SerializeField] private float alphaChangeSpeed = 2f;

    // Reference to the circle tilemap
    private Tilemap tilemap = null;
    // References to all the tiles that are on the circle wall map
    private TileBase[] circleWallTiles = null;
    // Current alpha value of the tiles on the tilemap
    private float currentTransparency = 1f;

    // Coroutine variables
    private bool isFadeCoroutineActive = false;
    private Coroutine fadeCoroutine = null;


    // Called 0th
    // Set references
    private void Awake()
    {
        // Set up singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Cannot have multiple CircleTilemapSingletons in the scene");
            Destroy(this.gameObject);
        }

        // References
        tilemap = GetComponent<Tilemap>();

        // Get all the tiles used in the tilemap
        circleWallTiles = new TileBase[tilemap.GetUsedTilesCount()];
        tilemap.GetUsedTilesNonAlloc(circleWallTiles);
    }
    // Called 1st
    // Initialization
    private void Start()
    {
        currentTransparency = 1f;
    }
    // Called when the component set inactive.
    private void OnDisable()
    {
        // Get rid of any run-time changes
        SetOpacity(1f);
    }


    /// <summary>Starts a coroutine to fade the tiles to the given alpha.</summary>
    /// <param name="targetAlpha">Alpha to fade the tiles to.</param>
    public void StartTileFade(float targetAlpha)
    {
        if (isFadeCoroutineActive)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(TileFadeCoroutine(targetAlpha));
    }

    /// <summary>Coroutine to fade the tiles to the given alpha.</summary>
    /// <param name="targetAlpha">Alpha to fade the tiles to.</param>
    private IEnumerator TileFadeCoroutine(float targetAlpha)
    {
        isFadeCoroutineActive = true;

        // Lerp starting point
        float startAlpha = currentTransparency;
        // Lerp time step
        float t = alphaChangeSpeed * Time.deltaTime;
        while (t < 1)
        {
            // Update the transparency of the tiles
            SetOpacity(Mathf.Lerp(startAlpha, targetAlpha, t));
            t += alphaChangeSpeed * Time.deltaTime;

            yield return null;
        }
        // Set it since we are done
        SetOpacity(targetAlpha);

        isFadeCoroutineActive = false;
        yield return null;
    }

    /// <summary>Set the alpha of all the tiles to the specified value.
    /// Also refreshes the tilemap.</summary>
    /// <param name="alphaValue">Alpha value [0, 1] to set the tiles to.</param>
    private void SetOpacity(float alphaValue)
    {
        foreach (TileBase tileB in circleWallTiles) {
            Tile tile = (Tile)tileB;
            Color col = tile.color;
            col.a = alphaValue;
            tile.color = col;
        }
        currentTransparency = alphaValue;
        RefreshTilemap();
    }

    /// <summary>Refreshes all tiles on the tilemap.</summary>
    private void RefreshTilemap()
    {
        tilemap.RefreshAllTiles();
    }
}
