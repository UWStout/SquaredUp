using UnityEngine;

[RequireComponent(typeof(GuardRotator))]
public class GuardRotatorSavable : SavableMonoBehav<GuardRotatorSavable>
{
    private GuardRotator guardRotator = null;


    protected override void Awake()
    {
        base.Awake();

        guardRotator = GetComponent<GuardRotator>();
    }


    public override void Load(object serializedObj)
    {
        // Cast the data
        GuardRotatorSaveData data = serializedObj as GuardRotatorSaveData;

        // Load the data
        guardRotator.Load(data.CheckShouldMove(), data.GetCurIndex(), data.GetWaitTimer(), data.GetTimeToWait());
    }

    public override object Save()
    {
        return new GuardRotatorSaveData(guardRotator);
    }
}
