using UnityEngine;

/// <summary>Helps to transition between shapes</summary>
public class MeshTransitioner
{
    // Vertices of the current/starting mesh
    private Vector3[] startingVertices;

    /// <summary>Constructs a MeshTransitioner with the given starting vertices</summary>
    /// <param name="startVert">Vertices of the current/starting mesh</param>
    public MeshTransitioner(Vector3[] startVert)
    {
        this.startingVertices = startVert;
    }

    /// <summary>Lerps between the starting vertices and the given vertices by t</summary>
    /// <param name="targetVertices">Vertices to lerp towards</param>
    /// <param name="t">Step amount to lerp</param>
    /// <returns>Lerped vertices array</returns>
    public Vector3[] LerpMeshPoints(Vector3[] targetVertices, float t)
    {
        if (targetVertices.Length != startingVertices.Length)
        {
            Debug.LogError("Starting[" + startingVertices.Length + "] and target["+ targetVertices.Length +"] vertex array lengths do not match");
        }
        Vector3[] rtnPoints = new Vector3[startingVertices.Length];

        for (int i = 0; i < rtnPoints.Length; ++i)
        {
            rtnPoints[i] = Vector3.Lerp(startingVertices[i], targetVertices[i], t);
        }

        return rtnPoints;
    }
}
