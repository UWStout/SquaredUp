using UnityEngine;

// Interactable for testing. Changes color of the interactable.
public class TestInteracatable : Interactable
{
    // References
    // Reference to a mesh renderer
    [SerializeField]
    private MeshRenderer meshRendRef = null;
    // Main material of the interactable.
    [SerializeField]
    private Material mainMat = null;
    // Secondary material of the interactable.
    [SerializeField]
    private Material otherMat = null;

    // If the current material is hte main material.
    private bool isMain;

    // Called before the first frame update.
    private void Start()
    {
        isMain = true;
    }

    /// <summary>
    /// Swaps the material of the mesh renderer from main to secondary or secondary to main.
    /// </summary>
    public override void Interact()
    {
        if (!isMain)
        {
            meshRendRef.material = mainMat;
        } else
        {
            meshRendRef.material = otherMat;
        }
        isMain = !isMain;
    }
}
