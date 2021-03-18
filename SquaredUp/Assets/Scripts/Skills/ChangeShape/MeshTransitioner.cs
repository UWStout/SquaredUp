using System.Collections;
using UnityEngine;

/// <summary>Helps to transition between shapes</summary>
public class MeshTransitioner : MonoBehaviour
{
    // Mesh to start with
    [SerializeField] private MeshFilter[] targetMeshes;

    // Coroutine variables for how fast to change the shape and when we are close enough
    [SerializeField] private float changeSpeed = 0.005f;
    // If the coroutine is finished
    private bool changeMeshCoroutFin = true;
    // Reference to the coroutine running
    private Coroutine changeMeshCorout = null;
    // Function to call once mesh transition is done
    public delegate void FinishChange();


    /// <summary>Sets the vertices of the meshes to be the given vertices</summary>
    /// <param name="vertices">Vertices to set the meshes to have</param>
    public void SetMeshVertices(Vector3[] vertices)
    {
        for (int i = 0; i < targetMeshes.Length; ++i)
        {
            if (vertices.Length != targetMeshes[i].mesh.vertexCount)
            {
                Debug.LogError("Incorrect vertex amount for " + targetMeshes[i].name);
            }
            targetMeshes[i].mesh.vertices = vertices;
        }
    }

    /// <summary>Returns the amount of vertices in the meshes we are changing.</summary>
    public int GetVertexCount()
    {
        return targetMeshes[0].mesh.vertexCount;
    }
    /// <summary>Returns the vertex at the given index.</summary>
    public Vector3 GetVertex(int index)
    {
        return targetMeshes[0].mesh.vertices[index];
    }

    /// <summary>Starts smoothly changing the shape of the mesh to have the given vertices</summary>
    /// <param name="targetVertices">Target vertices</param>
    /// <param name="onFinishMeshChange">Function to call once the mesh has finished changing</param>
    public void StartChangeMesh(Vector3[] targetVertices, FinishChange onFinishMeshChange=null)
    {
        // If there is an ongoing coroutine, stop it
        if (!changeMeshCoroutFin)
        {
            StopCoroutine(changeMeshCorout);
        }
        // Start a new coroutine
        changeMeshCorout = StartCoroutine(ChangeMeshCoroutine(targetVertices, onFinishMeshChange));
    }

    /// <summary>Coroutine to smoothly change the shape of the mesh</summary>
    /// <param name="targetVertices">Vertices to transition mesh towards</param>
    /// <param name="onFinishMeshChange">Function to call once the mesh has finished changing</param>
    private IEnumerator ChangeMeshCoroutine(Vector3[] targetVertices, FinishChange onFinishMeshChange)
    {
        changeMeshCoroutFin = false;
        Vector3[] startingVertices = targetMeshes[0].mesh.vertices;

        // The amount of lerps that will be done
        int iterations = (int)(1 / changeSpeed);
        for (int i = 0; i < iterations; ++i)
        {
            // Step
            float t = changeSpeed * i;

            // Lerp for the transition for each mesh
            for (int k = 0; k < targetMeshes.Length; ++k)
            {
                Vector3[] vertices = LerpMeshPoints(startingVertices, targetVertices, t);
                targetMeshes[k].mesh.vertices = vertices;
            }

            yield return null;
        }
        // Set the variables without lerping now that we are done
        for (int k = 0; k < targetMeshes.Length; ++k)
        {
            targetMeshes[k].mesh.vertices = targetVertices;
        }

        changeMeshCoroutFin = true;
        // Call the specified functionality after changing meshes
        onFinishMeshChange?.Invoke();

        yield return null;
    }

    /// <summary>Lerps between the starting vertices and the given vertices by t</summary>
    /// <param name="startingVertices">Vertices to begin lerping from</param>
    /// <param name="targetVertices">Vertices to lerp towards</param>
    /// <param name="t">Step amount to lerp</param>
    /// <returns>Lerped vertices array</returns>
    private Vector3[] LerpMeshPoints(Vector3[] startingVertices, Vector3[] targetVertices, float t)
    {
        if (targetVertices.Length != startingVertices.Length)
        {
            Debug.LogError("Starting[" + startingVertices.Length + "] and target[" + targetVertices.Length + "] vertex array lengths do not match");
        }
        Vector3[] rtnPoints = new Vector3[startingVertices.Length];

        for (int i = 0; i < rtnPoints.Length; ++i)
        {
            rtnPoints[i] = Vector3.Lerp(startingVertices[i], targetVertices[i], t);
        }

        return rtnPoints;
    }
}
