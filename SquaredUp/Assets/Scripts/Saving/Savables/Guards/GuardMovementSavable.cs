using UnityEngine;

[RequireComponent(typeof(GuardMovement))]
public class GuardMovementSavable : SavableMonoBehav<GuardMovementSavable>
{
    private GuardMovement guardMovement = null;


    protected override void Awake()
    {
        base.Awake();

        guardMovement = GetComponent<GuardMovement>();
    }


    public override void Load(object serializedObj)
    {
        // Cast the data
        GuardMovementSaveData data = serializedObj as GuardMovementSaveData;

        // Load the data
        guardMovement.Load(data.CheckShouldMove(), data.GetCurIndex(), data.GetWaitTimer(),
            data.GetTimeToWait(), data.GetLastMoveDirection(), data.GetIsFirstMove());
    }

    public override object Save()
    {
        return new GuardMovementSaveData(guardMovement);
    }
}
