using System;

[Serializable]
public class SkillHUDSaveData
{
    private int colorIndex = 0;
    private int shapeIndex = 0;


    public SkillHUDSaveData(SkillHUDManager skillHUDManager)
    {
        colorIndex = skillHUDManager.colorRowIndex;
        shapeIndex = skillHUDManager.shapeColIndex;
    }


    public int GetColorIndex()
    {
        return colorIndex;
    }
    public int GetShapeIndex()
    {
        return shapeIndex;
    }
}
