using System.Collections;
using UnityEngine;

/// <summary>Follows the specified target along the x axis and y axis.
/// Meant for mainly the camera.</summary>
public class Follow : MonoBehaviour
{
    private enum FrameTime { BEGINNING, END };

    // Reference to the target to follow.
    [SerializeField] private Transform followTarget = null;
    // If this should follow on the axes
    [SerializeField] private bool xAxisFollow = true;
    [SerializeField] private bool yAxisFollow = true;
    [SerializeField] private bool zAxisFollow = false;
    // If this should wait until the end of the frame to follow
    [SerializeField] private FrameTime whenToUpdate = FrameTime.END;
    // If we should follow on start
    [SerializeField] private bool startFolowOnStart = true;

    // If we should be following right now
    private bool shouldFollow = true;
    // If the follow coroutine is currently active
    private bool isFollowing = false;


    // Called 1st
    private void Start()
    {
        if (startFolowOnStart)
        {
            StartFollow();
        }
    }


    /// <summary>Starts following the target</summary>
    public void StartFollow()
    {
        shouldFollow = true;
        // If coroutine is terminated, begin it again.
        if (!isFollowing)
        {
            StartCoroutine(FollowCoroutine());
        }
    }
    /// <summary>Stops following the target</summary>
    public void StopFollow()
    {
        shouldFollow = false;
    }

    /// <summary>Coroutine to follow the target</summary>
    private IEnumerator FollowCoroutine()
    {
        isFollowing = true;
        while (shouldFollow)
        {
            // If we want to wait to move until the end of the frame
            if (whenToUpdate == FrameTime.END)
            {
                yield return new WaitForEndOfFrame();
            }

            // Actually move
            Move();

            // If we want to move at the beginning of the frame
            if (whenToUpdate == FrameTime.BEGINNING)
            {
                yield return null;
            }
        }
        isFollowing = false;
        yield return null;
    }

    /// <summary>Moves this transform to follow the target transform</summary>
    private void Move()
    {
        Vector3 pos = transform.position;
        // Apply follows as desired
        if (xAxisFollow) { pos.x = followTarget.position.x; }
        if (yAxisFollow) { pos.y = followTarget.position.y; }
        if (zAxisFollow) { pos.z = followTarget.position.z; }
        // Set position
        transform.position = pos;
    }
}
