using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>Manages the HUD for skills</summary>
public class HUDManager : MonoBehaviour
{
    // Reference to the grid HUD's total parent
    [SerializeField] private Transform gridHUDParent = null;

    // Selection row and column parents
    [SerializeField] private Transform colorRow = null;
    [SerializeField] private Transform shapeCol = null;

    // Spacing for how the states should be layed out
    [SerializeField] private float verticalSpace = 150f;
    [SerializeField] private float horizontalSpace = 150f;

    // Indexing information
    private int row = 0;
    private int[] index = null;

    // If the HUD is currently active
    private bool isHUDActive = false;


    // Called when the script is enabled.
    // Subscribe to events.
    private void OnEnable()
    {
        // Open HUD when the game pauses and close it when it opens
        SkillMenuController.OpenSkillMenuEvent += OpenHUD;
        SkillMenuController.CloseSkillMenuEvent += CloseHUD;
        // Navigate menu
        InputEvents.MainAxisEvent += OnHUDAxis;
        InputEvents.RevertEvent += OnRevert;

        // Testing
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        InputEvents.HackerAxisEvent += OnHackerAxis;
#endif
    }
    // Called when the script is disabled.
    // Unsubscribe from events.
    private void OnDisable()
    {
        SkillMenuController.OpenSkillMenuEvent -= OpenHUD;
        SkillMenuController.CloseSkillMenuEvent -= CloseHUD;
        InputEvents.MainAxisEvent -= OnHUDAxis;
        InputEvents.HackerAxisEvent -= OnHackerAxis;
#if DEVELOPMENT_BUILD || UNITY_EDITOR
        InputEvents.RevertEvent -= OnRevert;
#endif
    }

    // Called 1st
    // Initialize
    private void Start()
    {
        int amountSkills = SkillController.Instance.GetSkillAmount();

        // Initialize the selection indices
        row = 0;
        index = new int[amountSkills];
        for (int i = 0; i < amountSkills; ++i) { index[i] = 0; }

        // Hide HUD on start
        HUDstatus(false);
    }


    /// <summary>Gets the index of the current state of the selected skill.</summary>
    /// <param name="skillIndex">Index of the skill to check the state of.</param>
    public int GetStateIndexOfSkill(int skillIndex)
    {
        return index[skillIndex];
    }

    ///<summary>Helper method to open HUD</summary>
    private void OpenHUD()
    {
        HUDstatus(true);
    }
    /// <summary>Helper method to close HUD and execute the skill calls</summary>
    private void CloseHUD()
    {
        HUDstatus(false);
        ExecuteHUD();
    }
    ///<summary>Activates or deactivates HUD</summary>
    private void HUDstatus(bool status)
    {
        // Show HUD
        if (status)
        {
            ActivateAllSets();
        }
        // Hide HUD
        else
        {
            DeactivateAllSets();
        }
        isHUDActive = status;
    }
    /// <summary>Turns off the parents of each of the sets</summary>
    private void DeactivateAllSets()
    {
        foreach (Transform parent in gridHUDParent)
        {
            parent.gameObject.SetActive(false);
        }
    }
    /// <summary>Turns on the unlocked options in each set</summary>
    private void ActivateAllSets()
    {
        int count = 0;
        foreach (Transform parent in gridHUDParent)
        {
            Skill curSkill = SkillController.Instance.GetSkill(count);
            ActivateSingleSet(parent, curSkill);
            ++count;
        }
    }
    /// <summary>Helper function for ActivateAllSets to turn on all the sets</summary>
    private void ActivateSingleSet(Transform setParent, Skill skillRef)
    {
        int count = 0;
        // Activate the states if they are unlocked
        foreach (Transform child in setParent)
        {
            child.gameObject.SetActive(skillRef.IsStateUnlocked(count));
            ++count;
        }
        // Activate the parent if the skill is unlocked
        setParent.gameObject.SetActive(skillRef.IsSkillUnlocked());
    }

    /// <summary>Uses the skills based on what was selected</summary>
    private void ExecuteHUD()
    {
        // Use the skills
        SkillController.Instance.UseAllSkills(index);
    }

    /// <summary>Called when the player uses the HUD navigation</summary>
    private void OnHUDAxis(Vector2 rawInput)
    {
        // Only navigate when active
        if (isHUDActive)
        {
            // Horizontal movement
            if (rawInput.x < 0) { OnHUDRight(); }
            else if (rawInput.x > 0) { OnHUDLeft(); }

            // Vertical movement
            if (rawInput.y < 0) { OnColumnUp(); }
            else if (rawInput.y > 0) { OnColumnDown(); }
        }
    }

    /// <summary>Moves the HUD left</summary>
    private void OnHUDLeft()
    {
        if (isHUDActive)
        {
            int maxRow = colorRow.childCount;
            // If there is another skill to the right
            if (row + 1 < maxRow)
            {
                // If that skill is also unlocked
                Skill nextSkill = SkillController.Instance.GetSkill(row + 1);
                if (nextSkill.IsSkillUnlocked())
                {
                    colorRow.Translate(-horizontalSpace, 0, 0);
                    ++row;
                }
            }
        }
    }
    /// <summary>Moves the HUD right</summary>
    private void OnHUDRight()
    {
        if (isHUDActive)
        {
            if (row > 0)
            {
                colorRow.Translate(horizontalSpace, 0, 0);
                --row;
            }
        }
    }
    /// <summary>Move one column up</summary>
    private void OnColumnUp()
    {
        if (isHUDActive)
        {
            Skill curSkill = SkillController.Instance.GetSkill(row);
            int totalStates = curSkill.GetAmountStates();
            // If there is a next state
            if (index[row] + 1 < totalStates)
            {
                // And that state is unlocked
                if (curSkill.IsStateUnlocked(index[row] + 1))
                {
                    gridHUDParent.GetChild(row).Translate(0, verticalSpace, 0);
                    ++index[row];
                }
            }
        }
    }
    /// <summary>Move on column down</summary>
    private void OnColumnDown()
    {
        if (isHUDActive)
        {
            if (index[row] > 0)
            {
                gridHUDParent.GetChild(row).Translate(0, -verticalSpace, 0);
                --index[row];
            }
        }
    }

    // Called when the player hits escape
    // Close the hud without using the skills
    private void OnRevert()
    {
        HUDstatus(false);
    }

    // For testing
#if DEVELOPMENT_BUILD || UNITY_EDITOR
    private void OnHackerAxis(Vector2 rawInputVector)
    {
        if (rawInputVector.y > 0)
        {
            Debug.Log("Elite Hacker detected. Unlocking hidden memes.");
            SkillController.Instance.UnlockSkillState(row, index[row] + 1);
        }

        if (rawInputVector.x > 0)
        {
            Debug.Log("Elite Hacker detected. Expanding meme library.");
            SkillController.Instance.UnlockSkill(row + 1);
        }

        // Update the HUD
        ActivateAllSets();
    }
#endif
}
