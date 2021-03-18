using UnityEngine;

/// <summary>Animation to transition between specified meshes.</summary>
public class SpecificMeshChangeAnimation : MeshChangingAnimation
{
    // Reference to the shapes to change to
    [SerializeField] private ShapeData[] shapeData = null;

    /// <summary>Initialize the mesh vertices to have the shape data</summary>
    protected override Vertices[] Initialize()
    {
        Vertices[] rtnVerts = new Vertices[shapeData.Length];
        for (int i = 0; i < rtnVerts.Length; ++i)
        {
            rtnVerts[i] = new Vertices(shapeData[i].ShapeVertices);
        }
        return rtnVerts;
    }
}
