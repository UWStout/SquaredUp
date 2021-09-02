
public class GridDiagonalCornerCircleWall : GridHittable
{
    public override bool Hit(GridHit hit)
    {
        if (!hit.moverObj.CompareTag("Player"))
        {
            return false;
        }
        // Only let the player pass if they are a circle
        return ChangeShapeSkill.instance.GetCurrentState().TypeOfShape == ShapeData.ShapeType.CIRCLE;
    }
}
