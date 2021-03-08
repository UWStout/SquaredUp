using System.Collections;
using UnityEngine;

/// <summary>Skill that zooms the camera in</summary>
public class ChangeZoomSkill : SkillBase<ZoomData>
{
    //SFX for zoom ability
    public AudioSource zoom;
    // Reference to the PlayerMovement script
    [SerializeField] private PlayerMovement playMove = null;
    // Reference to camera whose orthographic size to change.
    private Camera zoomCam = null;

    // Lerp speed
    [SerializeField] [Range(0, 1)] private float zoomSpeed = 0.1f;
    [SerializeField] [Min(0)] private float closeEnoughVal = 0.01f;
    // If the zoom coroutine is finished
    private bool zoomFin = true;
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
        zoomTarget = SkillData.GetData(0).ZoomAmount;
        Use(0);
    }

    /// <summary>Starts to zoom the camera to the zoom amount of the ZoomData with the corresponding index</summary>
    public override void Use(int stateIndex)
    {
        BeginZoom(SkillData.GetData(stateIndex).ZoomAmount);
        zoom.Play();
        // Can't move while zoomed out
        playMove.AllowMovement(stateIndex == 0);
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
