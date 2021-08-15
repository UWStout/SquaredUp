using UnityEngine;

/// <summary>
/// Wrapper to call an animation from a unity event.
/// </summary>
[RequireComponent(typeof(Animation))]
public class PlayAnimation : MonoBehaviour
{
    private Animation anim = null;


    private void Awake()
    {
        anim = GetComponent<Animation>();
    }


    public void Play()
    {
        anim.Play();
    }
}
