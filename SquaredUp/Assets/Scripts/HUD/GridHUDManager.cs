using UnityEngine;
using UnityEngine.UI;
using TMPro;


/* Grid Design HUD for Squared Up
 * 
 * 
 */

/// <summary>Manages the HUD for skills</summary>
public class GridHUDManager : MonoBehaviour
{
    // Reference to the grid HUD's total parent
    [SerializeField] private Transform gridHUDParent = null;

    // Prefab that will be created to display the states of the skills
    [SerializeField] private GameObject skillStatePrefab = null;

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
        PauseController.GamePauseEvent += OpenHUD;
        PauseController.GameUnpauseEvent += CloseHUD;
        // Navigate menu
        InputEvents.MainAxisEvent += OnHUDAxis;
        InputEvents.RevertEvent += OnRevert;

        // Testing
        InputEvents.HackerAxisEvent += OnHackerAxis;
    }
    // Called when the script is disabled.
    // Unsubscribe from events.
    private void OnDisable()
    {
        PauseController.GamePauseEvent -= OpenHUD;
        PauseController.GameUnpauseEvent -= CloseHUD;
        InputEvents.MainAxisEvent -= OnHUDAxis;
        InputEvents.HackerAxisEvent -= OnHackerAxis;
        InputEvents.RevertEvent -= OnRevert;
    }

    // Called 1st
    // Initialize
    private void Start()
    {
        // Create the HUD
        CreateSkillHUDParents();
        CreateSkillStatesHUD();

        int amountSkills = SkillController.Instance.GetSkillAmount();

        // Initialize the selection indices
        row = 0;
        index = new int[amountSkills];
        for (int i = 0; i < amountSkills; ++i) { index[i] = 0; }

        // Hide HUD on start
        HUDstatus(false);
    }

    /// <summary>Creates an object for every skill there is. This object will be the parent of the skill's states HUD</summary>
    private void CreateSkillHUDParents()
    {
        // Assumption is that it starts with no children
        if (gridHUDParent.childCount != 0)
        {
            Debug.LogError(gridHUDParent.name + " cannot have children. Please remove them");
        }

        // Create a parent for each skill to hold the state HUD things
        int amountSkills = SkillController.Instance.GetSkillAmount();
        for (int i = 0; i < amountSkills; ++i)
        {
            // Create the transform, set its parent, and its local position
            Transform parent = new GameObject().transform;
            parent.SetParent(gridHUDParent);
            parent.localPosition = new Vector3(horizontalSpace * i, 0);
            // Set its name
            parent.name = SkillController.Instance.GetSkill(i).ToString() + " HUD Parent";
        }
    }

    /// <summary>Creates an object for every states of each skill and childs it under the corresponding parent</summary>
    private void CreateSkillStatesHUD()
    {
        // Create state HUDs for each skill
        int count = 0;
        foreach (Transform parent in gridHUDParent)
        {
            Skill curSkill = SkillController.Instance.GetSkill(count);
            for (int i = 0; i < curSkill.GetAmountStates(); ++i)
            {
                CreateSingleStateUI(parent, curSkill, i);
            }
            ++count;
        }
    }

    /// <summary>Helper function for CreateSkillStatesHUD. Create the UI object for the current skill.</summary>
    /// <param name="parent">Transform that will be the parent of the single UI element.</param>
    /// <param name="curSkill">Current skill whose state the UI is generated from.</param>
    /// <param name="index">Index of the current state of the skill.</param>
    private void CreateSingleStateUI(Transform parent, Skill curSkill, int index)
    {
        // Create the transform as a child, set its local position, and give it a good name
        Transform child = Instantiate(skillStatePrefab, parent).transform;
        child.transform.localPosition = new Vector3(0, -verticalSpace * index, 0);
        child.name = curSkill.GetStateName(index);

        // Try to pull a UI element off the skill
        try
        {
            AddSpecificUIFromState(child, curSkill, index);
        }
        // If we failed to get the prefab, just slap some text on it
        catch
        {
            AddDefaultTextToState(child);
        }
    }

    /// <summary>Helper function for CreateSingleStateUI. Create and initialize a UI element from the current state of the current skill.</summary>
    /// <param name="skillTransform">Transform of the state UI element.</param>
    /// <param name="curSkill">Current skill whose state the UI is generated from.</param>
    /// <param name="index">Index of the current state of the skill.</param>
    private void AddSpecificUIFromState(Transform skillTransform, Skill curSkill, int index)
    {
        GameObject stateUIPref = curSkill.GetStateUIElement(index);
        // Set the prefab as a child of the newley created object
        GameObject stateUIInstance = Instantiate(stateUIPref, skillTransform);
        // Try to pull the state ui behavior off the state instance to initialize it
        try
        {
            StateUIBehavior stateUIBev = stateUIInstance.GetComponent<StateUIBehavior>();
            stateUIBev.Initialize(this);
        }
        // Some won't have behaviors and that's okay
        catch { }
    }

    /// <summary>Helper function for CreateSingleStateUI. Create a text element on the UI that just says the name of the skill.</summary>
    /// <param name="skillTransform">Transform of the skill's UI.</param>
    private void AddDefaultTextToState(Transform skillTransform)
    {
        // Add a child to house text
        GameObject grandchild = new GameObject("State Text");
        grandchild.transform.SetParent(skillTransform);
        grandchild.transform.localPosition = Vector3.zero;
        grandchild.transform.localScale = Vector3.one;
        // Add text and set it to say the state's name
        TextMeshProUGUI text = grandchild.AddComponent<TextMeshProUGUI>();
        text.text = skillTransform.name;
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
            int maxRow = gridHUDParent.childCount;
            // If there is another skill to the right
            if (row + 1 < maxRow)
            {
                // If that skill is also unlocked
                Skill nextSkill = SkillController.Instance.GetSkill(row + 1);
                if (nextSkill.IsSkillUnlocked())
                {
                    foreach (Transform parent in gridHUDParent)
                    {
                        parent.Translate(-horizontalSpace, 0, 0);
                    }
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
                foreach (Transform parent in gridHUDParent)
                {
                    parent.Translate(horizontalSpace, 0, 0);
                }
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

    // For testing
    // TODO Remove this for final build
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

    // Called when the player hits escape
    // Close the hud without using the skills
    private void OnRevert()
    {
        HUDstatus(false);
    }


    /// <summary>Gets the index of the current state of the selected skill.</summary>
    /// <param name="skillIndex">Index of the skill to check the state of.</param>
    public int GetStateIndexOfSkill(int skillIndex)
    {
        return index[skillIndex];
    }
}
