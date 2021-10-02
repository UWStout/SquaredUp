using UnityEngine;

/// <summary>
/// Saves and loads data for the HUD's color and shape selected indices.
/// </summary>
public class SkillHUDSavable : SavableMonoBehav<SkillHUDSavable>
{
    [SerializeField] private SkillHUDManager skillHUDManager = null;


    public override void Load(object serializedObj)
    {
        SkillHUDSaveData data = serializedObj as SkillHUDSaveData;

        skillHUDManager.colorRowIndex = data.GetColorIndex();
        skillHUDManager.shapeColIndex = data.GetShapeIndex();
    }

    public override object Save()
    {
        return new SkillHUDSaveData(skillHUDManager);
    }
}
