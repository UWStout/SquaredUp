using UnityEngine;


/* Grid Design HUD for Squared Up
 * 
 * 
 */


public class GridHUDManager : MonoBehaviour
{
    // Constants
    private const int AMOUNT_SKILLS = 4;
    private const int MAX_ROW = AMOUNT_SKILLS - 1;
    private static readonly int[] MAX_INDICES = { 3, 3, 1, 1 };
    private const int SHAPE_INDEX = 0;
    private const int COLOR_INDEX = 1;
    private const int ZOOM_INDEX = 2;
    private const int SCALE_INDEX = 3;

    // Singleton
    private static GridHUDManager instance;
    public static GridHUDManager Instance { get { return instance; } }

    // Reference to the grid HUD's total parent
    [SerializeField] private Transform gridHUDParent = null;
    // References to the parents of each skill
    [SerializeField] private Transform shapeOptionsParent = null;
    [SerializeField] private Transform colorOptionsParent = null;
    [SerializeField] private Transform zoomOptionsParent = null;
    [SerializeField] private Transform scaleOptionsParent = null;
    // All the parents will be put in this list on start for iteration purposes
    private Transform[] parentList = null;

    // Reference to skills
    private SkillController skillContRef = null;

    // Indexing information
    private int row = 0;
    private int[] index = new int[AMOUNT_SKILLS] { 0, 0, 0, 0 };
    // Current maxes to restrict what the player can select for iteration purposes
    // TODO Remove [SerializeField]s I did that for testing purposes only
    [SerializeField] private int curMaxRow = 2;
    [SerializeField]  private int[] curMaxIndices = new int[AMOUNT_SKILLS] { 0, 0, 1, 1 };

    // If the HUD is currently active
    private bool isHUDActive = false;

    // Holds which skills the player has unlocked
    // TODO Remove [SerializeField]s I did that for testing purposes only
    [SerializeField] private bool[] shapesUnlocked = { true, false, false, false };
    [SerializeField] private bool[] colorsUnlocked = { true, false, false, false };
    [SerializeField] private bool[] zoomUnlocked = { true, true };
    [SerializeField] private bool[] scaleUnlocked = { false, false };
    // All the unlock arrays will be put into this list on start
    private bool[][] unlockedLists = null;


    // Called when the script is enabled.
    // Subscribe to events.
    private void OnEnable()
    {
        // Open HUD when the game pauses and close it when it opens
        PauseController.GamePauseEvent += OpenHUD;
        PauseController.GameUnpauseEvent += CloseHUD;
        // Navigate menu
        InputEvents.MainAxisEvent += OnHUDAxis;

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
    }

    // Called 0th
    // Set references
    private void Awake()
    {
        // Singleton setup
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Scene should not contian multiple GridHUDManagers");
            Destroy(this);
        }

        skillContRef = FindObjectOfType<SkillController>();
        if (skillContRef == null)
        {
            Debug.LogError("GridHUDManager could not find SkillController");
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        row = 0;
        index = new int[AMOUNT_SKILLS];
        for (int i = 0; i < AMOUNT_SKILLS; ++i) { index[i] = 0; }

        parentList = new Transform[AMOUNT_SKILLS] { shapeOptionsParent, colorOptionsParent, zoomOptionsParent, scaleOptionsParent};
        unlockedLists = new bool[AMOUNT_SKILLS][] { shapesUnlocked, colorsUnlocked, zoomUnlocked, scaleUnlocked }; 

        // Hide HUD on start
        HUDstatus(false);
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
        foreach (Transform parent in parentList)
        {
            parent.gameObject.SetActive(false);
        }
    }
    /// <summary>Turns on the unlocked options in each set</summary>
    private void ActivateAllSets()
    {
        int count = 0;
        foreach (Transform parent in parentList)
        {
            ActivateSingleSet(parent, unlockedLists[count]);
            ++count;
        }
    }
    /// <summary>Helper function for ActivateAllSets to turn on all the sets</summary>
    private void ActivateSingleSet(Transform setParent, bool[] availBools)
    {
        int counter = 0;
        foreach (Transform child in setParent)
        {
            child.gameObject.SetActive(availBools[counter]);
            ++counter;
        }
        setParent.gameObject.SetActive(true);
    }

    /// <summary>Uses the skills based on what was selected</summary>
    private void ExecuteHUD()
    {
        // Index 0 is shape
        ChangeShapeSkill.Shape shape = (ChangeShapeSkill.Shape)index[SHAPE_INDEX];
        // Index 1 is color
        ChangeColorSkill.ChangeColor color = (ChangeColorSkill.ChangeColor)index[COLOR_INDEX];
        // Index 2 is zoom
        ChangeZoomSkill.ZoomLevel zoom = (ChangeZoomSkill.ZoomLevel)index[ZOOM_INDEX];

        // Use the skills
        skillContRef.UseSkills(shape, color, zoom);
    }

    /// <summary>Called when the player uses the HUD navigation</summary>
    private void OnHUDAxis(Vector2 rawInput)
    {
        // Only navigate when active
        if (isHUDActive)
        {
            if (rawInput.x < 0)
            {
                OnHUDRight();
            }
            else if (rawInput.x > 0)
            {
                OnHUDLeft();
            }

            if (rawInput.y < 0)
            {
                OnColumnUp();
            }
            else if (rawInput.y > 0)
            {
                OnColumnDown();
            }
        }
    }

    /// <summary>Moves the HUD left</summary>
    private void OnHUDLeft()
    {
        if (isHUDActive && row < curMaxRow)
        {
            foreach (Transform parent in parentList)
            {
                parent.Translate(-150, 0, 0);
            }
            ++row;
        }
    }
    /// <summary>Moves the HUD right</summary>
    private void OnHUDRight()
    {
        if (isHUDActive && row > 0)
        {
            foreach (Transform parent in parentList)
            {
                parent.Translate(150, 0, 0);
            }
            --row;
        }
    }
    /// <summary>Move one column up</summary>
    private void OnColumnUp()
    {
        if (isHUDActive)
        {
            if (index[row] < curMaxIndices[row])
            {
                parentList[row].Translate(0, 150, 0);
                ++index[row];
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
                parentList[row].Translate(0, -150, 0);
                --index[row];
            }
        }
    }


    // For testing
    private void OnHackerAxis(Vector2 rawInputVector)
    {
        if (rawInputVector.y > 0)
        {
            Debug.Log("Elite Hacker detected. Unlocking hidden memes.");
            switch (row)
            {
                case SHAPE_INDEX:
                    UnlockNextShape();
                    break;
                case COLOR_INDEX:
                    UnlockNextColor();
                    break;
            }
        }

        if (rawInputVector.x > 0)
        {
            Debug.Log("Elite Hacker detected. Expanding meme library.");
            UnlockScaleAbility();
        }

        // Update the HUD
        ActivateAllSets();
    }

    /// <summary>Unlocks the scale ability</summary>
    public void UnlockScaleAbility()
    {
        if (curMaxRow < MAX_ROW && !scaleUnlocked[0])
        {
            ++curMaxRow;
            scaleUnlocked[0] = true;
            scaleUnlocked[1] = true;
        }
    }


    public void UnlockShape(ChangeShapeSkill.Shape shape)
    {

    }

    /// <summary>Unlocks the next shape. Shape order of unlock is dictated by ChangeShapeSkill.Shape</summary>
    public void UnlockNextShape()
    {
        UnlockNextThing(SHAPE_INDEX);
    }

    /// <summary>Unlocks the next color. Color order of unlock is dictated by ChangeColorSkill.ChangeColor</summary>
    public void UnlockNextColor()
    {
        UnlockNextThing(COLOR_INDEX);
    }

    /// <summary>Unlocks the next thing (ex: shape) of a specific skill with the given index</summary>
    private void UnlockNextThing(int index)
    {
        if (curMaxIndices[index] < MAX_INDICES[index])
        {
            ++curMaxIndices[index];
            unlockedLists[index][curMaxIndices[index]] = true;
        }
    }
}
