﻿using System.Collections;
using UnityEngine;

/// <summary>Skill that allows the player to change their shape</summary>
public class ChangeShapeSkill : SkillBase<ShapeData>
{
    // SFX for shape transformation
    public AudioSource transformShape;
    // References
    // Transform that will be scaled for the player
    [SerializeField] private Transform playerScalableTrans = null;
    // Mesh Filters that will have their mesh changed to the shape being changed to
    [SerializeField] private MeshFilter[] playerMeshFilterRefs = null;
    // Reference to the player collider controller
    [SerializeField] private PlayerColliderController playerColContRef = null;
    // Refernce to the player movement script
    [SerializeField] private PlayerMovement playerMoveRef = null;

    // Coroutine variables for how fast to change the shape and when we are close enough
    [SerializeField] private float changeSpeed = 0.03f;
    // Target mesh vertices
    private Vector3[] targetVertices = new Vector3[0];
    // If the coroutine is finished
    private bool changeShapeCoroutFin = true;
    // Refrence to the coroutine running
    private Coroutine changeShapeCorout = null;

    // Original scale of the player
    private Vector3 originalScale = Vector3.one;

    // Where the player is currently facing
    private Vector2Int currentFacing = Vector2Int.up;


    // Called 1st
    // Initialize
    private void Start()
    {
        originalScale = playerScalableTrans.localScale;
        currentFacing = playerMoveRef.GetFacingDirection();
    }

    /// <summary>Changes the player to become the shape corresponding to the given index.
    /// Index matches what is specified in the editor. If index is unknown, consider using Use(ShapeData) instead.</summary>
    public override void Use(int stateIndex)
    {
        ShapeData data = SkillData.GetData(stateIndex);
        Vector2Int newFacing = playerMoveRef.GetFacingDirection();
        Vector3 size = GetSize(data, originalScale, newFacing);
        bool upcurstate = UpdateCurrentState(stateIndex);
        // Change shape even if one the current state if the player is trying to adjust their shape as well
        if (upcurstate || (currentFacing != newFacing && data.DirectionAffectsScale))
        {
            currentFacing = newFacing;
            // Swap the colliders
            // If the colliders couldn't be swapped, ergo could not fit, then do not swap the player's shape
            AvailableSpot availSpot = playerColContRef.ActivateCollider(data, size);
            if (availSpot.Available)
            {
                // Update the player's position so they don't get stuck in a wall
                playerScalableTrans.position = availSpot.Position;

                // Change all the meshes
                foreach (MeshFilter filter in playerMeshFilterRefs)
                {
                    filter.mesh = data.Mesh;
                }

                // Adjust the scale
                playerScalableTrans.localScale = size;
                transformShape.Play();
            }
        }
    }

    /// <summary>Gets the size of the shape given by the data</summary>
    /// <param name="data">Shape of collider to turn into</param>
    /// <param name="originalScale">The original scale of the player</param>
    /// <param name="playerFacingDirection">The direction the player is facing</param>
    private Vector3 GetSize(ShapeData data, Vector3 originalScale, Vector2Int playerFacingDirection)
    {
        Vector3 size = Vector3.Scale(data.Scale, originalScale);
        if (data.DirectionAffectsScale)
        {
            if (playerFacingDirection.y < 0)
            {
                size.y = -size.y;
            }
            else if (playerFacingDirection.x > 0)
            {
                float temp = size.x;
                size.x = size.y;
                size.y = temp;
            }
            else if (playerFacingDirection.x < 0)
            {
                float temp = size.x;
                size.x = -size.y;
                size.y = temp;
            }
        }

        return size;
    }

    private void StartChangeShape(Mesh meshToChangeTo)
    {
        targetVertices = meshToChangeTo.vertices;
        if (!changeShapeCoroutFin)
        {
            StopCoroutine(changeShapeCorout);
        }
        changeShapeCorout = StartCoroutine(ChangeShapeCoroutine());
    }

    private IEnumerator ChangeShapeCoroutine()
    {
        changeShapeCoroutFin = false;
        int iterations = (int) (1 / changeSpeed);
        MeshTransitioner[] transitioners = new MeshTransitioner[playerMeshFilterRefs.Length];
        for (int i = 0; i < transitioners.Length; ++i)
        {
            transitioners[i] = new MeshTransitioner(playerMeshFilterRefs[i].mesh);
        }
        for (int i = 0; i < iterations; ++i)
        {
            float t = changeSpeed * i;
            for (int k = 0; k < transitioners.Length; ++k) {
                Vector3[] vertices = transitioners[k].LerpMeshPoints(targetVertices, t);
                transitioners[k].ApplyVerticesToMesh(vertices);
            }
            yield return null;
        }

        changeShapeCoroutFin = true;
        yield return null;
    }
}
