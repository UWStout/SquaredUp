using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Singleton to control the circle tilemap
/// </summary>
[RequireComponent(typeof(Tilemap))]
public class CircleTilemapSingleton : SingletonMonoBehav<CircleTilemapSingleton>
{
    // Speed of the transparency transition
    [SerializeField] private float alphaChangeSpeed = 2f;

    // Reference to the circle tilemap
    private Tilemap tilemap = null;
    // References to all the tiles that are on the circle wall map
    private TileBase[] circleWallTiles = null;
    // Current alpha value of the tiles on the tilemap
    private float currentTransparency = 1f;
    // Target alpha value of the tiles on the tilemap
    private float targetTransparency = 1f;

    // Coroutine variables
    private bool isFadeCoroutineActive = false;
    private Coroutine fadeCoroutine = null;


    // Called 0th
    // Set references
    protected override void Awake()
    {
        base.Awake();

        // References
        tilemap = GetComponent<Tilemap>();

        // Get all the tiles used in the tilemap
        circleWallTiles = new TileBase[tilemap.GetUsedTilesCount()];
        tilemap.GetUsedTilesNonAlloc(circleWallTiles);
    }
    // Called when the component set inactive.
    private void OnDisable()
    {
        // Get rid of any run-time changes
        SetOpacity(currentTransparency);
    }


    /// <summary>Starts a coroutine to fade the tiles to the given alpha.</summary>
    /// <param name="targetAlpha">Alpha to fade the tiles to.</param>
    public void StartTileFade(float targetAlpha)
    {
        targetTransparency = targetAlpha;
        if (isFadeCoroutineActive)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(TileFadeCoroutine());
    }

    /// <summary>
    /// Instantly fades the tiles to the given alpha.
    /// </summary>
    /// <param name="targetAlpha">Alpha to fade the tiles to.</param>
    public void TileFadeInstant(float targetAlpha)
    {
        targetTransparency = targetAlpha;
        SetOpacity(targetAlpha);
    }
    /// <summary>
    /// Gets the current target the tiles are being faded to.
    /// </summary>
    /// <returns>Target alpha the tiles are being faded to.</returns>
    public float GetTargetTileAlpha()
    {
        return targetTransparency;
    }


    /// <summary>Coroutine to fade the tiles to the given alpha.</summary>
    private IEnumerator TileFadeCoroutine()
    {
        isFadeCoroutineActive = true;

        // Lerp starting point
        float startAlpha = currentTransparency;
        // Lerp time step
        float t = alphaChangeSpeed * Time.deltaTime;
        while (t < 1)
        {
            // Update the transparency of the tiles
            SetOpacity(Mathf.Lerp(startAlpha, targetTransparency, t));
            t += alphaChangeSpeed * Time.deltaTime;

            yield return null;
        }
        // Set it since we are done
        SetOpacity(targetTransparency);

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
        tilemap.RefreshAllTiles();
    }
}
