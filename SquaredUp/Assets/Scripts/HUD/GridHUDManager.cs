using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Grid Design HUD for Squared Up
 * 
 * 
 */


public class GridHUDManager : MonoBehaviour
{
    //private GameObject arrays that are needed
    private List<GameObject> HUD_Shape = new List<GameObject>();
    private List<GameObject> HUD_Color = new List<GameObject>();
    private List<GameObject> HUD_Zoom = new List<GameObject>();
    private List<GameObject> HUD_Scale = new List<GameObject>();
    //indexing information
    private int Row;
    private int[] index = { 0, 0, 0, 0 };
    //Misc data needed to have both menus use same inputs
    [SerializeField] private bool selectedHUD;

    // Start is called before the first frame update
    void Start()
    {
        //destroys gameobjects not in use
        if (!selectedHUD)
        {
            Destroy(gameObject);
        }
        //general startup proceedure
        Debug.Log("Startup");
        FindObjects();
        HUDstatus(false);
        Time.timeScale = 1;
        Row = 0;
    }

    //opens and closes HUD uses timescale to pause game, and know when game is paused
    public void OnOpenCloseHUD()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            HUDstatus(true);
        }
        else
        {
            CloseHUD();
            Time.timeScale = 1;
        }
    }

    //activates and deactivates HUD
    private void HUDstatus(bool status)
    {
        foreach(GameObject g in HUD_Shape)
        {
            g.SetActive(status);
        }
        foreach (GameObject g in HUD_Color)
        {
            g.SetActive(status);
        }
        foreach (GameObject g in HUD_Zoom)
        {
            g.SetActive(status);
        }
        foreach (GameObject g in HUD_Scale)
        {
            g.SetActive(status);
        }
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
        //index 0 is for shape
        switch (index[0])
        {
            case 0:
                ChangeSquare();
                break;
            case 1:
                ChangeRectangle();
                break;
            case 2:
                ChangeCircle();
                break;
            case 3:
                ChangeTriangle();
                break;
        }
        //index 1 is color
        switch (index[1])
        {
            case 0:
                ChangeDefault();
                break;
            case 1:
                ChangeGreen();
                break;
            case 2:
                ChangeRed();
                break;
            case 3:
                ChangeBlue();
                break;
        }
        //index 2 is zoom
        switch (index[2])
        {
            case 0:
                ZoomIn();
                break;
            case 1:
                ZoomOut();
                break;
        }
        //index 3 is scale
        switch (index[3])
        {
            case 0:
                ScaleUp();
                break;
            case 1:
                ScaleDown();
                break;
        }
    }
    //code to find all the objects that are child of HUD to be encompassing and easily converted into Prefab
    private void FindObjects()
    {
        foreach (Transform child in this.transform)
        {
            if (child.tag.Equals("HUDshape"))
            {
                HUD_Shape.Add(child.gameObject);
            }
            else if (child.tag.Equals("HUDcolor"))
            {
                HUD_Color.Add(child.gameObject);
            }
            else if (child.tag.Equals("HUDzoom"))
            {
                HUD_Zoom.Add(child.gameObject);
            }
            else if (child.tag.Equals("HUDscale"))
            {
                HUD_Scale.Add(child.gameObject);
            }
        }
    }
    //code to move the HUD left
    public void OnHUDLeft()
    {
        if (Time.timeScale == 0 && Row < 3)
        {
            Row++;
            foreach (GameObject g in HUD_Shape)
            {
                g.transform.Translate(-150, 0, 0);
            }
            foreach (GameObject g in HUD_Color)
            {
                g.transform.Translate(-150, 0, 0);
            }
            foreach (GameObject g in HUD_Zoom)
            {
                g.transform.Translate(-150, 0, 0);
            }
            foreach (GameObject g in HUD_Scale)
            {
                g.transform.Translate(-150, 0, 0);
            }
        }
    }
    //code to move the HUD right
    public void OnHUDRight()
    {
        if (Time.timeScale == 0 && Row > 0)
        {
            Row--;
            foreach (GameObject g in HUD_Shape)
            {
                g.transform.Translate(150, 0, 0);
            }
            foreach (GameObject g in HUD_Color)
            {
                g.transform.Translate(150, 0, 0);
            }
            foreach (GameObject g in HUD_Zoom)
            {
                g.transform.Translate(150, 0, 0);
            }
            foreach (GameObject g in HUD_Scale)
            {
                g.transform.Translate(150, 0, 0);
            }
        }
    }
    //code to move one column up
    public void OnColumnUp()
    {
        if (Time.timeScale == 0)
        {
            switch (Row)
            {
                case 0:
                    if (index[0] < 3) {
                        index[0]++;
                        foreach (GameObject g in HUD_Shape)
                        {
                            g.transform.Translate(0, 150, 0);
                        }
                    }
                    break;
                case 1:
                    if (index[1] < 3)
                    {
                        index[1]++;
                        foreach (GameObject g in HUD_Color)
                        {
                            g.transform.Translate(0, 150, 0);
                        }
                    }
                    break;
                case 2:
                    if (index[2] < 1)
                    {
                        index[2]++;
                        foreach (GameObject g in HUD_Zoom)
                        {
                            g.transform.Translate(0, 150, 0);
                        }
                    }
                    break;
                case 3:
                    if (index[3] < 1)
                    {
                        index[3]++;
                        foreach (GameObject g in HUD_Scale)
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
        if (Time.timeScale == 0)
        {
            switch (Row)
            {
                case 0:
                    if (index[0] > 0)
                    {
                        index[0]--;
                        foreach (GameObject g in HUD_Shape)
                        {
                            g.transform.Translate(0, -150, 0);
                        }
                    }
                    break;
                case 1:
                    if (index[1] > 0)
                    {
                        index[1]--;
                        foreach (GameObject g in HUD_Color)
                        {
                            g.transform.Translate(0, -150, 0);
                        }
                    }
                    break;
                case 2:
                    if (index[2] > 0)
                    {
                        index[2]--;
                        foreach (GameObject g in HUD_Zoom)
                        {
                            g.transform.Translate(0, -150, 0);
                        }
                    }
                    break;
                case 3:
                    if (index[3] > 0)
                    {
                        index[3]--;
                        foreach (GameObject g in HUD_Scale)
                        {
                            g.transform.Translate(0, -150, 0);
                        }
                    }
                    break;

            }
        }
    }

    /*
     * From here on out is connecting points for other code so abilities can be used
     * 
     * 
     */
    private void ChangeSquare()
    {
        Debug.Log("Square");
    }

    private void ChangeRectangle()
    {
        Debug.Log("Rectangle");
    }

    private void ChangeCircle()
    {
        Debug.Log("Circle");
    }

    private void ChangeTriangle()
    {
        Debug.Log("Triangle");
    }

    private void ChangeDefault()
    {
        Debug.Log("Default");
    }

    private void ChangeGreen()
    {
        Debug.Log("Green");
    }

    private void ChangeRed()
    {
        Debug.Log("Red");
    }

    private void ChangeBlue()
    {
        Debug.Log("blue");
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
