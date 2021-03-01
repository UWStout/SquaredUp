using UnityEngine;

public class TempleSkillUnlock : MonoBehaviour
{
    /// <summary> Unlocks the rectangle and the color green</summary>
    private void UnlockSkills()
    {
        GridHUDManager.Instance.UnlockNextColor();
        GridHUDManager.Instance.UnlockNextShape();
    }
}
