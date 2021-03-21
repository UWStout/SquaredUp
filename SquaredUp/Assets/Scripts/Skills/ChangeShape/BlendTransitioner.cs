﻿using System.Collections;
using UnityEngine;

/// <summary>Helps to transition between shapes</summary>
public class BlendTransitioner : MonoBehaviour
{
    private const int BLEND_SHAPE_AMOUNT = 2;

    // Mesh to start with
    [SerializeField] private SkinnedMeshRenderer[] targetRenderers;

    // Coroutine variables for how fast to change the shape and when we are close enough
    [SerializeField] private float changeSpeed = 0.005f;
    // If the coroutine is finished
    private bool changeMeshCoroutFin = true;
    // Reference to the coroutine running
    private Coroutine changeMeshCorout = null;
    // Function to call once mesh transition is done
    public delegate void FinishChange();


    /// <summary>Starts smoothly changing the shape given</summary>
    /// <param name="onFinishMeshChange">Function to call once the mesh has finished changing</param>
    public void StartChangeShape(ShapeData.ShapeType shapeType, FinishChange onFinishMeshChange=null)
    {
        // If there is an ongoing coroutine, stop it
        if (!changeMeshCoroutFin)
        {
            StopCoroutine(changeMeshCorout);
        }
        // Start a new coroutine
        changeMeshCorout = StartCoroutine(ChangeMeshCoroutine(shapeType, onFinishMeshChange));
    }

    /// <summary>Coroutine to smoothly change the shape of the mesh</summary>
    /// <param name="onFinishMeshChange">Function to call once the mesh has finished changing</param>
    private IEnumerator ChangeMeshCoroutine(ShapeData.ShapeType shapeType, FinishChange onFinishMeshChange)
    {
        changeMeshCoroutFin = false;

        // Get the initial values for each of the blend shapes
        float[] startBlendShapes = new float[BLEND_SHAPE_AMOUNT];
        for (int i = 0; i < BLEND_SHAPE_AMOUNT; ++i)
        {
            startBlendShapes[i] = targetRenderers[0].GetBlendShapeWeight(i);
        }
        // Get index of the target blend shape
        int targetBlendIndex = GetBlendIndexFromShape(shapeType);

        // The amount of lerps that will be done
        int iterations = (int)(1 / changeSpeed);
        for (int i = 0; i < iterations; ++i)
        {
            // Step
            float t = changeSpeed * i;
            // Lerp it
            LerpRenderers(targetBlendIndex, startBlendShapes, t);

            yield return null;
        }
        // Set the variables without lerping now that we are done
        LerpRenderers(targetBlendIndex, startBlendShapes, 1);

        changeMeshCoroutFin = true;
        // Call the specified functionality after changing meshes
        onFinishMeshChange?.Invoke();

        yield return null;
    }

    /// <summary>Returns blend index corresponding to the given shape.
    /// Returns -2 if ShapeType is unrecognized. Returns -1 if shape is base shape.</summary>
    private int GetBlendIndexFromShape(ShapeData.ShapeType shape)
    {
        switch (shape)
        {
            case ShapeData.ShapeType.BOX:
                return 0;
            case ShapeData.ShapeType.CIRCLE:
                return -1;
            case ShapeData.ShapeType.TRIANGLE:
                return 1;
            default:
                Debug.LogError("Unhandled Shape Type " + shape);
                break;
        }
        return -2;
    }

    /// <summary>Lerps the skinned renderers to set their blend shape values correctly.</summary>
    /// <param name="targetBlendIndex">Index of the target blend shape to move towards.</param>
    /// <param name="startBlendShapes">Starting values for each of the blend shape values.</param>
    /// <param name="t">Step for the lerp</param>
    private void LerpRenderers(int targetBlendIndex, float[] startBlendShapes, float t)
    {
        float increaseWeight = t * 100;
        // Lerp for the transition for each renderer
        for (int k = 0; k < targetRenderers.Length; ++k)
        {
            // Iterate over each blend shape
            for (int bsIndex = 0; bsIndex < BLEND_SHAPE_AMOUNT; ++bsIndex)
            {
                float shapeWeight;
                // If its the blend shape we are increasing
                if (bsIndex == targetBlendIndex)
                {
                    shapeWeight = increaseWeight;
                }
                // If its any other blend shape, decrease it until it reaches 0
                else
                {
                    shapeWeight = Mathf.Max(0f, startBlendShapes[bsIndex] - increaseWeight);
                }
                targetRenderers[k].SetBlendShapeWeight(bsIndex, shapeWeight);
            }
        }
    }
}