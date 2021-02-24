using System.Collections;
using UnityEngine;

public class ChangeZoomSkill : Skill
{
    public enum ZoomLevel { IN, OUT };

    // Size of zoom in and out for orthographics camera
    [SerializeField] [Min(0)] private float zoomInVal = 12f;
    [SerializeField] [Min(0)] private float zoomOutVal = 36f;

    // Reference to camera whose orthographic size to change.
    private Camera zoomCam = null;

    // Lerp speed
    [SerializeField] [Range(0, 1)] private float zoomSpeed = 0.1f;
    [SerializeField] [Min(0)] private float closeEnoughVal = 0.01f;
    // If the zoom coroutine is finished
    private bool zoomFin = false;
    // Current target zoom amount
    private float zoomTarget = 1f;


    // Called 0th
    // Set references
    private void Awake()
    {
        zoomCam = Camera.main;
    }

    // Called 1st
    // Initialization
    private void Start()
    {
        zoomTarget = zoomInVal;
        ProcessZoom(ZoomLevel.IN);
    }

    /// <summary>Starts to zoom the camera to the position corresponding to the enum</summary>
    public override void Use(int enumVal)
    {
        ProcessZoom((ZoomLevel)enumVal);
    }

    /// <summary>Handles what should happen on zoom in and zoom out</summary>
    private void ProcessZoom(ZoomLevel zoom)
    {
        Debug.Log("Process Zoom " + zoom);
        switch (zoom)
        {
            case ZoomLevel.IN:
                BeginZoom(zoomInVal);
                break;
            case ZoomLevel.OUT:
                BeginZoom(zoomOutVal);
                break;
            default:
                Debug.LogError("Unhandled ZoomLevel " + zoom + " in ChangeZoomSkill");
                break;
        }
    }

    /// <summary>Starts the zoom in/out process to the given orthoSize</summary>
    private void BeginZoom(float orthoSize)
    {
        zoomTarget = orthoSize;
        if (zoomFin)
        {
            StartCoroutine(ZoomCoroutine());
        }
    }

    /// <summary>Coroutine to smoothly zoom the camera</summary>
    private IEnumerator ZoomCoroutine()
    {
        zoomFin = false;
        while (Mathf.Abs(zoomCam.orthographicSize - zoomTarget) > closeEnoughVal)
        {
            zoomCam.orthographicSize = Mathf.Lerp(zoomCam.orthographicSize, zoomTarget, zoomSpeed);
            yield return null;
        }
        zoomCam.orthographicSize = zoomTarget;
        zoomFin = true;
        yield return null;
    }
}
