using UnityEngine;

/// <summary>Base class to animate a mesh using a MeshTransitioner.</summary>
[RequireComponent(typeof(MeshTransitioner))]
public abstract class MeshChangingAnimation : MonoBehaviour
{
    // If we want the mesh transitions to be random or sequential
    [SerializeField] private bool randomOrder = false;

    // References
    // Reference to the mesh transitioner
    private MeshTransitioner meshTransitioner;

    // Vertices to change to
    private Vertices[] vertices;
    // The index of the previous mesh
    private int lastMeshIndex = 0;


    // Called 0th
    // Set references
    private void Awake()
    {
        meshTransitioner = GetComponent<MeshTransitioner>();
    }
    
    // Called 1st
    // Initialization
    private void Start()
    {
        vertices = Initialize();
        lastMeshIndex = 0;
        meshTransitioner.SetMeshVertices(vertices[lastMeshIndex].vertices);
        TransitionNextMesh();
    }


    /// <summary>Initialize the vertices that will be lerped between.</summary>
    protected abstract Vertices[] Initialize();

    /// <summary>Returns the amount of vertices in the meshes we are changing.</summary>
    protected int GetVertexCount()
    {
        return meshTransitioner.GetVertexCount();
    }
    /// <summary>Returns the vertex at the given index.</summary>
    protected Vector3 GetVertex(int index)
    {
        return meshTransitioner.GetVertex(index);
    }

    /// <summary>Starts transitioning the current mesh to the next one.</summary>
    private void TransitionNextMesh()
    {
        int nextIndex;
        // Pick random index
        if (randomOrder)
        {
            nextIndex = PickRandomMeshIndex();
        }
        // Get next index in list
        else
        {
            nextIndex = NextMeshIndex();
        }

        lastMeshIndex = nextIndex;
        meshTransitioner.StartChangeMesh(vertices[nextIndex].vertices, TransitionNextMesh);
    }

    /// <summary>Picks a random index for meshes that is not the last index.</summary>
    private int PickRandomMeshIndex()
    {
        // Create a list of indices except for the last index
        int[] tempRandomList = new int[vertices.Length -1];
        int count = 0;
        for (int i = 0; i < vertices.Length; ++i)
        {
            if (i != lastMeshIndex)
            {
                tempRandomList[count] = i;
                ++count;
            }
        }
        // Return a random index in the list
        return tempRandomList[Random.Range(0, tempRandomList.Length)];
    }

    /// <summary>Returns the index of the mesh after the last mesh. Loops the list so 0 is after the last index.</summary>
    private int NextMeshIndex()
    {
        if (lastMeshIndex + 1 < vertices.Length)
        {
            return lastMeshIndex + 1;
        }
        else
        {
            return 0;
        }
    }
}

/// <summary>Class to hold an array of Vector3</summary>
public class Vertices
{
    // List of vertices
    public Vector3[] vertices;

    /// <summary>Constructs a list of vertices</summary>
    /// <param name="verts">List of vertices</param>
    public Vertices(Vector3[] verts)
    {
        vertices = verts;
    }
}
