using UnityEngine;

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
