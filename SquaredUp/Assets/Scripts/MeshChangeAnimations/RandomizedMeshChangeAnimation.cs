using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizedMeshChangeAnimation : MeshChangingAnimation
{
    [SerializeField] private int amountMeshesToGenerate = 2;

    /// <summary>Initialize the mesh vertices to have the shape data</summary>
    protected override Vertices[] Initialize()
    {
        Vertices[] vertsList = new Vertices[amountMeshesToGenerate];
        int amountVertices = GetVertexCount();
        for (int i = 0; i < vertsList.Length; ++i)
        {
            vertsList[i] = GenerateVertices(amountVertices);
        }
        return vertsList;
    }

    private Vertices GenerateVertices(int amountVertices)
    {
        Vector3[] vertices = new Vector3[amountVertices];
        for (int i = 0; i < vertices.Length; ++i)
        {
            vertices[i] = GenerateRandomVertex(GetVertex(i).normalized);
        }
        return new Vertices(vertices);
    }

    private Vector3 GenerateRandomVertex(Vector3 direction)
    {
        return Random.Range(0.4f, 0.5f) * direction;
    }
}
