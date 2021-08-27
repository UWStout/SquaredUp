using System;

/// <summary>
/// Save data for a guard who rotates.
/// </summary>
[Serializable]
public class GuardRotatorSaveData : GuardMovementBaseSaveData
{
    public GuardRotatorSaveData(GuardRotator guardRotator) : base(guardRotator)
    {

    }
}
