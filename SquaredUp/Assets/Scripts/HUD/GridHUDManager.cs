using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Grid Design HUD for Squared Up
 * 
 * 
 */


public class GridHUDManager : MonoBehaviour
{
    // References to skills
    private SkillController skillContRef = null;

    //private GameObjects
    private List<List<GameObject>> HUD_Icons = new List<List<GameObject>>();
    //indexing information
    private int Row;
    private int[] index=new int[4];
    //Misc data needed to have both menus use same inputs

    private bool isHUDActive = false;


    // Called 0th
    // Set references
    private void Awake()
    {
        skillContRef = FindObjectOfType<SkillController>();
        if (skillContRef == null)
        {
            Debug.LogError("GridHUDManager could not find SkillController");
        }
    }

    // Called when the script is enabled.
    // Subscribe to events.
    private void OnEnable()
    {
        // Open HUD when the game pauses and close it when it opens
        PauseController.GamePauseEvent += OpenHUD;
        PauseController.GameUnpauseEvent += CloseHUD;
        // Navigate menu
        InputEvents.MainAxisEvent += OnHUDAxis;
    }
    // Called when the script is disabled.
    // Unsubscribe from events.
    private void OnDisable()
    {
        PauseController.GamePauseEvent -= OpenHUD;
        PauseController.GameUnpauseEvent -= CloseHUD;
        InputEvents.MainAxisEvent -= OnHUDAxis;
    }

    // Start is called before the first frame update
    private void Start()
    {  
        //general startup proceedure
        //finds all Abilities
        HUD_Icons.Add(new List<GameObject>());
        HUD_Icons.Add(new List<GameObject>());
        HUD_Icons.Add(new List<GameObject>());
        HUD_Icons.Add(new List<GameObject>());
        
        FindObjects();
        HUDstatus(false);
        Row = 0;
    }

    //opens and closes HUD uses timescale to pause game, and know when game is paused
    public void OnOpenCloseHUD()
    {
        if (!isHUDActive)
        {
            Time.timeScale = 0;
            HUDstatus(true);
        }
        else
        {
            Time.timeScale = 1;
            CloseHUD();
        }
    }

    //activates and deactivates HUD
    private void HUDstatus(bool status)
    {
        foreach(List<GameObject> l in HUD_Icons)
        {
            foreach(GameObject g in l)
            {
                g.SetActive(status);
            }
        }
        isHUDActive = status;
    }
    // Helper method to open HUD
    private void OpenHUD()
    {
        HUDstatus(true);
    }
    //helper method
    private void CloseHUD()
    {
        HUDstatus(false);
        ExecuteHUD();
    }

    //executes HUD to set character
    private void ExecuteHUD()
    {
        // Index 0 is shape
        ChangeShapeSkill.Shape shape = (ChangeShapeSkill.Shape)index[0];
        // Index 1 is color
        ChangeColorSkill.ChangeColor color = (ChangeColorSkill.ChangeColor)index[1];
        // Index 2 is zoom
        ChangeZoomSkill.ZoomLevel zoom = (ChangeZoomSkill.ZoomLevel)index[2];

        // Use the skills
        skillContRef.UseSkills(shape, color, zoom);
    }

    //code to find all the objects that are child of HUD to be encompassing and easily converted into Prefab
    private void FindObjects()
    {
        ZeroOutIndex();
        foreach (Transform child in this.transform)
        {
            if (child.tag.Equals("HUDshape"))
            {
                HUD_Icons[0].Add(child.gameObject);
                HUD_Icons[0][index[0]].transform.position = new Vector3(0, -150 * index[0],0) + this.transform.position;
                index[0]++;
            }
            else if (child.tag.Equals("HUDcolor"))
            {
                HUD_Icons[1].Add(child.gameObject);
                HUD_Icons[1][index[1]].transform.position = new Vector3 (150, -150 * index[1],0) + this.transform.position;
                index[1]++;
            }
            else if (child.tag.Equals("HUDzoom"))
            {
                HUD_Icons[2].Add(child.gameObject);
                HUD_Icons[2][index[2]].transform.position = new Vector3 (300, -150 * index[2], 0) + this.transform.position;
                index[2]++;
            }
            else if (child.tag.Equals("HUDscale"))
            {
                HUD_Icons[3].Add(child.gameObject);
                HUD_Icons[3][index[3]].transform.position = new Vector3 (450, -150 * index[3], 0) + this.transform.position;
                index[3]++;
            }
        }
        ZeroOutIndex();
    }

    private void ZeroOutIndex()
    {
        index = new int[index.Length];
        foreach(int i in index)
        {
            index[i] = 0;
        }
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

    //code to move the HUD left
    public void OnHUDLeft()
    {
        if (isHUDActive && Row < 3)
        {
            Row++;
            foreach (List<GameObject> l in HUD_Icons)
            {
                foreach (GameObject g in l)
                {
                    g.transform.Translate(-150, 0, 0);
                }
            }
        }
    }
    //code to move the HUD right
    public void OnHUDRight()
    {
        if (isHUDActive && Row > 0)
        {
            Row--;
            foreach (List<GameObject> l in HUD_Icons)
            {
                foreach (GameObject g in l)
                {
                    g.transform.Translate(150, 0, 0);
                }
            }
        }
    }
    //code to move one column up
    public void OnColumnUp()
    {
        if (isHUDActive)
        {
            switch (Row)
            {
                case 0:
                    if (index[0] < 3) {
                        index[0]++;
                        foreach (GameObject g in HUD_Icons[0])
                        {
                            g.transform.Translate(0, 150, 0);
                        }
                    }
                    break;
                case 1:
                    if (index[1] < 3)
                    {
                        index[1]++;
                        foreach (GameObject g in HUD_Icons[1])
                        {
                            g.transform.Translate(0, 150, 0);
                        }
                    }
                    break;
                case 2:
                    if (index[2] < 1)
                    {
                        index[2]++;
                        foreach (GameObject g in HUD_Icons[2])
                        {
                            g.transform.Translate(0, 150, 0);
                        }
                    }
                    break;
                case 3:
                    if (index[3] < 1)
                    {
                        index[3]++;
                        foreach (GameObject g in HUD_Icons[3])
                        {
                            g.transform.Translate(0, 150, 0);
                        }
                    }
                    break;
            }
        }
    }
    //code to move on column down
    public void OnColumnDown()
    {
        if (isHUDActive)
        {
            switch (Row)
            {
                case 0:
                    if (index[0] > 0)
                    {
                        index[0]--;
                        foreach (GameObject g in HUD_Icons[0])
                        {
                            g.transform.Translate(0, -150, 0);
                        }
                    }
                    break;
                case 1:
                    if (index[1] > 0)
                    {
                        index[1]--;
                        foreach (GameObject g in HUD_Icons[1])
                        {
                            g.transform.Translate(0, -150, 0);
                        }
                    }
                    break;
                case 2:
                    if (index[2] > 0)
                    {
                        index[2]--;
                        foreach (GameObject g in HUD_Icons[2])
                        {
                            g.transform.Translate(0, -150, 0);
                        }
                    }
                    break;
                case 3:
                    if (index[3] > 0)
                    {
                        index[3]--;
                        foreach (GameObject g in HUD_Icons[3])
                        {
                            g.transform.Translate(0, -150, 0);
                        }
                    }
                    break;

            }
        }
    }

    private void ZoomIn()
    {
        Debug.Log("Zoom In");
    }

    private void ZoomOut()
    {
        Debug.Log("Zoom Out");
    }

    private void ScaleUp()
    {
        Debug.Log("Scale Up");
    }

    private void ScaleDown()
    {
        Debug.Log("Scale Down");
    }
}
