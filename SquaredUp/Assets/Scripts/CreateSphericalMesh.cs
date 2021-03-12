using UnityEngine;

/// <summary>Helper class for updating the vertices of the sphere mesh so that fits it looks like the given mesh</summary>
public class CreateSphericalMesh
{
    /// <summary>
    /// Returns vertices for the sphere vertices given such that it will look like the given mesh (vertices & triangles)
    /// </summary>
    /// <param name="sphereVertices">Vertices of the sphere mesh</param>
    /// <param name="meshToConvertVertices">Vertices of the mesh to duplicate the shape of</param>
    /// <param name="meshToConvertTriangles">Triangles of the mesh to duplicate the shape of</param>
    /// <returns>Updated positions for vertices for the sphere such that it looks like the given mesh</returns>
    public Vector3[] ConvertMeshVerticesToSphericalCast(Vector3[] sphereVertices, Vector3[] meshToConvertVertices, int[] meshToConvertTriangles)
    {
        // Return vertices
        Vector3[] castVertices = new Vector3[sphereVertices.Length];
        // Current index of the 
        int index = 0;
        foreach (Vector3 sphVert in sphereVertices)
        {
            Vector3 rayDir = sphVert.normalized;
            float smallDist = float.MaxValue;
            for (int i = 2; i < meshToConvertTriangles.Length; ++i)
            {
                // Get each of the points of this triangle
                Vector3 qA = meshToConvertVertices[meshToConvertTriangles[i - 2]];
                Vector3 qB = meshToConvertVertices[meshToConvertTriangles[i - 1]];
                Vector3 qC = meshToConvertVertices[meshToConvertTriangles[i]];
                // Calculate normal of the triangle
                Vector3 triN = -Vector3.Cross((qA - qC), (qB - qC)).normalized;

                // Get the denominator first to avoid dividing by 0
                float denom = Vector3.Dot(triN, rayDir);
                if (Mathf.Abs(denom) > Mathf.Epsilon)
                {
                    // Get the center of the plane
                    Vector3 qCenter = (1.0f / 3) * (qA + qB + qC);
                    // Get the distance from the plane the current vertex is
                    float t = Vector3.Dot((qCenter - sphVert), triN) / denom;
                    // If the distance is positive and smaller than the current smallest, its the new smallest
                    if (t >= 0 && t < smallDist)
                    {
                        smallDist = t;
                    }
                }
            }
            // Use the distance to update the 
            castVertices[index] = smallDist * rayDir + sphVert;
            //Debug.Log(DetailedVector3ToString(sphVert) + " was converted to " + DetailedVector3ToString(castVertices[index]) + " with dist of " + smallDist);

            ++index;
        }
        return castVertices;
    }

    private string DetailedVector3ToString(Vector3 vector)
    {
        return "(" + vector.x + ", " + vector.y + ", " + vector.z + ")";
    }
}
