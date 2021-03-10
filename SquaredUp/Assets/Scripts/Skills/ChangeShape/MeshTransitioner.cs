using UnityEngine;

public class MeshTransitioner
{
    private Mesh mesh;
    private Vector3[] vertices;

    public MeshTransitioner(Mesh mesh)
    {
        this.mesh = mesh;
        this.vertices = this.mesh.vertices;
    }

    public Vector3[] LerpMeshPoints(Vector3[] targetPoints, float t)
    {
        Vector3[] rtnPoints = new Vector3[vertices.Length];

        // If the current amount of points is less than the target amount of points, we can't do this
        if (vertices.Length < targetPoints.Length)
        {
            Debug.LogError("Too many points to in target");
            return null;
        }
        // If the target amount of points is less than the current amount of points, we can just set multiple of our points
        // to be at the same locations
        else if (vertices.Length > targetPoints.Length)
        {
            // What the ratio of the points is
            float ratio = vertices.Length / targetPoints.Length;

            int startPoint = 0;
            int endPoint = 0;
            for (int i = 0; i < targetPoints.Length; ++i)
            {
                startPoint = endPoint;
                endPoint = (int)((i + 1) * ratio);
                for (int k = startPoint; k < endPoint; ++k)
                {
                    rtnPoints[k] = Vector3.Lerp(vertices[k], targetPoints[i], t);
                }
            }
        }
        // If the target amount of points is exactly the current amount of points, just lerp for each point
        else
        {
            for (int i = 0; i < vertices.Length; ++i)
            {
                rtnPoints[i] = Vector3.Lerp(vertices[i], targetPoints[i], t);
            }
        }

        return rtnPoints;
    }

    public void ApplyVerticesToMesh(Vector3[] verts)
    {
        if (mesh.vertices.Length != verts.Length)
        {
            mesh.vertices = verts;
        }
    }
}
