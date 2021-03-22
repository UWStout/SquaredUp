using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>Skill that zooms the camera in</summary>
public class ChangeZoomSkill : SkillBase<ZoomData>
{
    //SFX for zoom ability
    public AudioSource zoom;
    // Reference to the PlayerInput to swap maps
    [SerializeField] private PlayerInput playerInp = null;
    // Name of the zoomed out action map in player input
    [SerializeField] private string zoomActionMapName = "ZoomedOut";
    // Name of the player action map in player input
    [SerializeField] private string playerActionMapName = "Player";
    // Reference to camera whose orthographic size to change.
    private Camera zoomCam = null;

    // Lerp speed
    [SerializeField] [Min(0.0001f)] private float zoomSpeed = 1f;
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
        if (UpdateCurrentState(stateIndex))
        {
            BeginZoom(SkillData.GetData(stateIndex).ZoomAmount);
            zoom.Play();
            // We are zooming out
            if (stateIndex != 0)
            {
                // Can't move while zoomed out
                playerInp.SwitchCurrentActionMap(zoomActionMapName);
            }
            // We are zooming in
            else
            {
                playerInp.SwitchCurrentActionMap(playerActionMapName);
            }
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
            zoomCam.orthographicSize = Mathf.Lerp(zoomCam.orthographicSize, zoomTarget, zoomSpeed * Time.deltaTime);
            yield return null;
        }
        zoomCam.orthographicSize = zoomTarget;
        zoomFin = true;
        yield return null;
    }
}
