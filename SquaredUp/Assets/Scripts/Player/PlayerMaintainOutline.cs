using System.Collections;
using UnityEngine;

/// <summary>Keeps the player's outline a consistent size during runtime.</summary>
public class PlayerMaintainOutline : MonoBehaviour
{
    // Non outline part of the thing.
    [SerializeField] private Transform body = null;
    // Outline of the thing.
    [SerializeField] private Transform outline = null;
    // Thickness of the outline.
    [SerializeField] [Min(0)] private float outlineWidth = 0.2f;

    // Reference to the coroutine running.
    private Coroutine maintainCoroutine = null;


    // Called 1st
    // Domestic Initialization
    private void Start()
    {
        StartMaintainOutlineCoroutine();
    }
    // Called when this component is enabled.
    // Subscribe to events.
    private void OnEnable()
    {
        ChangeFormController.OnAvailableSpotFound += StartMaintainOutlineCoroutine;
        ChangeFormController.OnFinishChangingForm += StopMaintainOutlineCoroutine;
    }
    // Called when this component is disabled.
    // Unsubscribe from events.
    private void OnDisable()
    {
        ChangeFormController.OnAvailableSpotFound -= StartMaintainOutlineCoroutine;
        ChangeFormController.OnFinishChangingForm -= StopMaintainOutlineCoroutine;
    }


    /// <summary>Starts the coroutine to maintain the outline.</summary>
    private void StartMaintainOutlineCoroutine()
    {
        if (maintainCoroutine == null)
        {
            maintainCoroutine = StartCoroutine(MaintainOutlineCoroutine());
        }
    }

    /// <summary>Stops the coroutine to maintain the outline.</summary>
    private void StopMaintainOutlineCoroutine()
    {
        if (maintainCoroutine != null)
        {
            StopCoroutine(maintainCoroutine);
            maintainCoroutine = null;
        }
        UpdateBodyScale();
    }

    /// <summary>Coroutine to maintain the outline.</summary>
    private IEnumerator MaintainOutlineCoroutine()
    {
        while (true)
        {
            yield return null;
            UpdateBodyScale();
        }
    }

    /// <summary>Updates the scale of the body to keep the outline consistent.</summary>
    private void UpdateBodyScale()
    {
        Vector3 targetScale = outline.lossyScale - new Vector3(outlineWidth, outlineWidth, outlineWidth);
        for (int i = 0; i < 3; ++i)
        {
            if (targetScale[i] < 0)
            {
                targetScale[i] = outlineWidth;
            }
        }
        SetGlobalScale(body, targetScale);
    }

    /// <summary>Sets the global scale of the given transform to the given scale.</summary>
    /// <param name="transform">Transform to set scale of.</param>
    /// <param name="globalScale">Global scale to set transform to.</param>
    public void SetGlobalScale(Transform transform, Vector3 globalScale)
    {
        transform.localScale = Vector3.one;
        transform.localScale = new Vector3(globalScale.x / transform.lossyScale.x, globalScale.y / transform.lossyScale.y, globalScale.z / transform.lossyScale.z);
    }
}
