using UnityEngine;

// Abstract parent class of a skill for the player to use
public abstract class Skill : MonoBehaviour
{
    /// <summary>Activation of the skill</summary>
    public abstract void Use();
}
