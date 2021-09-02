using UnityEngine;

/// <summary>
/// Saves data for a grid mover.
/// </summary>
[RequireComponent(typeof(GridMover))]
public class GridMoverSavable : SavableMonoBehav<GridMoverSavable>
{
    private GridMover gridMover = null;


    protected override void Awake()
    {
        base.Awake();

        gridMover = GetComponent<GridMover>();
    }


    public override void Load(object serializedObj)
    {
        TransformSaveData data = serializedObj as TransformSaveData;

        transform.localScale = data.GetLocalScale();
        transform.localRotation = data.GetLocalRotation();
        gridMover.SetPosition(data.GetGlobalPosition());
    }
    public override object Save()
    {
        return new TransformSaveData(transform);
    }
}
