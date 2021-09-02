using UnityEngine;

/// <summary>
/// Saves grid ice so that it can keep momentum during a save.
/// </summary>
[RequireComponent(typeof(GridIcePushable))]
public class GridIceSavable : SavableMonoBehav<GridIceSavable>
{
    private GridIcePushable gridIce = null;


    protected override void Awake()
    {
        base.Awake();

        gridIce = GetComponent<GridIcePushable>();
    }


    public override void Load(object serializedObj)
    {
        GridIceSaveData data = serializedObj as GridIceSaveData;

        gridIce.slideDirection = data.GetSlideDirection();
    }

    public override object Save()
    {
        return new GridIceSaveData(gridIce);
    }
}
