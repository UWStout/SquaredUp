using UnityEngine;

// Throwaway test script that tempararily causes the player to change shape.
public class TempChangeShapeTest : MonoBehaviour
{
    [SerializeField] private ChangeShapeSkill changeShapeSkill = null;
    [SerializeField] private ShapeData shapeToChangeTo = null;

    public void ChangeShape()
    {
        changeShapeSkill.SpecifyChangeType(shapeToChangeTo);
        changeShapeSkill.Use();
    }
}
