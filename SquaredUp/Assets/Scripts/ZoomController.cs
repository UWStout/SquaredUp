using System.Collections;
using UnityEngine;

/// <summary>Skill that zooms the camera in</summary>
public class ZoomController : MonoBehaviour
{
    // SFX for zoom ability
    [SerializeField] private AudioSource zoomAudioSource = null;

    // Name of the zoomed out action map in player input
    [SerializeField] private string zoomActionMapName = "ZoomedOut";

    // Reference to camera whose orthographic size to change.
    private Camera zoomCam = null;

    // Zoom ortho sizes for zoomed in and out
    [SerializeField] [Min(0f)] private float zoomInOrthoSize = 12f;
    [SerializeField] [Min(0f)] private float zoomOutOrthoSize = 36f;
    // If we are zoomed in
    private bool areZoomedIn = true;

    // Lerp speed
    [SerializeField] [Min(0.1f)] private float zoomSpeed = 1f;
    [SerializeField] [Min(0f)] private float closeEnoughVal = 0.01f;
    // If the zoom coroutine is finished
    private bool zoomFin = true;
    // Current target zoom amount
    private float zoomTarget = 1f;


    // Called when the component is set active
    // Subscribe to events
    private void OnEnable()
    {
        InputEvents.ZoomEvent += ToggleZoom;
    }
    // Called when the component is set inactive
    // Unsubscribe from events
    private void OnDisable()
    {
        InputEvents.ZoomEvent -= ToggleZoom;
    }

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
        zoomTarget = zoomInOrthoSize;
        areZoomedIn = true;
    }


    /// <summary>Toggles if the camera is zoomed in or out.</summary>
    private void ToggleZoom()
    {
        zoomAudioSource.Play();
        // Zoom out if we are zoomed in
        if (areZoomedIn)
        {
            areZoomedIn = false;
            ZoomOut();
        }
        // Zoom in if we are zoomed out
        else
        {
            ZoomIn();
            areZoomedIn = true;
        }
    }

    /// <summary>Starts to zoom the camera in. Lets the player move again.</summary>
    private void ZoomIn()
    {
        // Let player move now that we are zoomed back in
        InputController.Instance.PopInputMap(zoomActionMapName);
        // Start coroutine to zoom in
        BeginZoom(zoomInOrthoSize);
    }

    /// <summary>Starts to zoom the camera out. Doesn't let the player move.</summary>
    private void ZoomOut()
    {
        // Can't move while zoomed out
        InputController.Instance.SwitchInputMap(zoomActionMapName);
        // Start coroutine to zoom out
        BeginZoom(zoomOutOrthoSize);
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
            zoomCam.orthographicSize = Mathf.Lerp(zoomCam.orthographicSize, zoomTarget, zoomSpeed * Time.deltaTime);
            yield return null;
        }
        zoomCam.orthographicSize = zoomTarget;
        zoomFin = true;
        yield return null;
    }
}
