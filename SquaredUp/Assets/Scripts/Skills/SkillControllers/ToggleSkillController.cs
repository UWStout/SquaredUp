
public class ToggleSkillController : SkillControllerParent
{
    private const int ZOOM_INDEX = 0;

    // Current states for the skills
    private int[] curStates = new int[0];


    // Called 1st
    // Initilization
    private void Start()
    {
        curStates = new int[skills.Count];
        for (int i = 0; i < curStates.Length; ++i)
        {
            curStates[i] = 0;
        }
    }

    // Called when this component is enabled
    // Subscribe to events
    private void OnEnable()
    {
        InputEvents.ZoomEvent += OnZoom;
    }
    // Called when this component is disabled
    // Unsubscribe from events
    private void OnDisable()
    {
        InputEvents.ZoomEvent -= OnZoom;
    }


    /// <summary>Called when the zoom input event is triggered. Toggle the zoom</summary>
    private void OnZoom()
    {
        IncrementSkillState(ZOOM_INDEX);
    }

    /// <summary>Increments the state of the given skill and uses it</summary>
    /// <param name="skillIndex">Skill whose state to increment and use</param>
    private void IncrementSkillState(int skillIndex)
    {
        Skill zoomSkill = GetSkill(skillIndex);
        UseSkill(skillIndex, IncrementState(skillIndex));
    }

    /// <summary>Increments the state for the given skill index. Wraps it around so that if its reached
    /// the last state it will go back to the first</summary>
    /// <param name="skillIndex">Skill to increase the state for</param>
    /// <returns>Incremented state value</returns>
    private int IncrementState(int skillIndex)
    {
        ++curStates[skillIndex];
        int amountStates = GetSkill(skillIndex).GetAmountStates();
        if (curStates[skillIndex] >= amountStates)
        {
            curStates[skillIndex] = 0;
        }
        return curStates[skillIndex];
    }
}
