using UnityEngine;
using UnityEngine.UI;

/// <summary> Updates the image for the color state UI prefab to be the player's currently selected shape.</summary>
[RequireComponent(typeof(Image))]
public class ColorStateUIBehavior : StateUIBehavior
{
    // Index of the shape skill
    private const int SHAPE_INDEX = 0;

    // Sprites associated with the shapes. Make sure these line up with the skills
    [SerializeField] private Sprite[] shapeSprites = null;
    // Reference to the image on this object
    private Image imageRef = null;


    // Called when this component is enabled.
    // Subscribe to events.
    private void OnEnable()
    {
        InputEvents.MainAxisEvent += UpdateShapeImageVector2;
        InputEvents.PauseEvent += UpdateShapeImage;
    }
    // Called when this component is disabled.
    // Unsubscribe from events.
    private void OnDisable()
    {
        InputEvents.MainAxisEvent -= UpdateShapeImageVector2;
        InputEvents.PauseEvent -= UpdateShapeImage;
    }

    // Called 0th.
    // Set references.
    private void Awake()
    {
        imageRef = GetComponent<Image>();
    }


    /// <summary>Calls UpdateShapeImage.</summary>
    private void UpdateShapeImageVector2(Vector2 rawMainAxis)
    {
        UpdateShapeImage();
    }
    /// <summary>Changes the image on the color state to reflect the currently selected shape.</summary>
    private void UpdateShapeImage()
    {
        // Get the index of the shape skill
        int shapeIndex = this.HUDManager.GetStateIndexOfSkill(SHAPE_INDEX);
        try
        {
            // Change the sprite to the sprite of the shape with the given state index.
            imageRef.sprite = shapeSprites[shapeIndex];
        }
        catch
        {
            Debug.LogError("Selected shape out of bounds for image grab on " + this.name);
        }
    }
}
