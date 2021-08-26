
public interface IGuardMovement
{
    /// <summary>
    /// Allows the guard to move to prevents them from moving.
    /// </summary>
    /// <param name="condition">True - can move. False - can't move.</param>
    void AllowMove(bool condition);
}
