using System.Collections;
using UnityEngine;

/// <summary>
/// Controller for the CannotFitVisual that pops up when the player cannot change in their current location.
/// </summary>
public class CannotFitVisualController : MonoBehaviour
{
    // Mesh renderer visual of the shape.
    [SerializeField] private SkinnedMeshRenderer meshRend = null;
    // BlendTransitioner to change the shape.
    [SerializeField] private BlendTransitioner blendTrans = null;
    // ScaleController to change the scale.
    [SerializeField] private ScaleController scaleCont = null;

    // Speed at which the visual fades out.
    [SerializeField] private float fadeSpeed = 5f;
    // Time to wait before fading out in seconds.
    [SerializeField] private float aliveTime = 1.5f;

    // Material of the cannot fit visual.
    private Material originalMat = null;

    // Coroutine info
    private Coroutine fadeCoroutine = null;
    private bool isFadeCoroutineActive = false;


    // Called 0th
    // Set references
    private void Awake()
    {
        // Copy the material
        originalMat = meshRend.material;
        Material copyMat = new Material(originalMat);
        meshRend.material = copyMat;
    }


    /// <summary>Shows the cannot fit visual at the given position as the given size and shape.</summary>
    /// <param name="pos">Position to move the cannot fit visual to.</param>
    /// <param name="size">Shape size to set.</param>
    /// <param name="shapeType">ShapeType to set the cannot fit visual to.</param>
    public void Activate(Vector3 pos, Vector3 size, ShapeData.ShapeType shapeType)
    {
        // Move to the player's position.
        transform.position = pos;
        // Make it fit the size and shape the player attempted to change to.
        scaleCont.ShapeScale = size;
        blendTrans.ChangeShapeInstant(shapeType);

        StartFadeOutCoroutine();
    }

    /// <summary>Starts the fade out coroutine.</summary>
    private void StartFadeOutCoroutine()
    {
        if (isFadeCoroutineActive)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeOutCoroutine());
    }

    /// <summary>Coroutine to fade out the cannot fit visual.</summary>
    private IEnumerator FadeOutCoroutine()
    {
        isFadeCoroutineActive = true;

        // Change the color back to default and show the renderer
        meshRend.material.color = originalMat.color;
        meshRend.enabled = true;

        // Wait a bit before fading out
        yield return new WaitForSeconds(aliveTime);

        // Start and target colors
        Color startColor = originalMat.color;
        Color targetColor = originalMat.color;
        targetColor.a = 0;
        // Slowly fade out the thing material
        float t = 0;
        while (t < 1)
        {
            meshRend.material.color = Color.Lerp(startColor, targetColor, t);
            t += fadeSpeed * Time.deltaTime;
            yield return null;
        }
        // Set the color to the target and turn off the renderer
        meshRend.material.color = targetColor;
        meshRend.enabled = false;

        // End the routine
        isFadeCoroutineActive = false;
        yield return null;
    }
}
