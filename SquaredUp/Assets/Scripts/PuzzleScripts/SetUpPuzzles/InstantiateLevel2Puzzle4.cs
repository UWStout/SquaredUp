using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateLevel2Puzzle4 : MonoBehaviour
{
    [SerializeField]
    private UniquePrefabInfo[] doorPrefabInfo, controllerPrefabInfo, tileMapControllerPrefabInfo, colorChangerPrefabInfo;
    [SerializeField]
    private GameObject[] Tilemaps;
    [SerializeField]
    GameObject ColorChangeScript;
    [SerializeField]
    private GameObject doorPrefab, doorControllerPrefab;
    [SerializeField]
    private GameObject tilemapControllerRedPrefab, tilemapControllerGreenPrefab, tilemapControllerBluePrefab;
    [SerializeField]
    private GameObject colorChangerPrefab;
    private GameObject puzzleTwoDoors, puzzleTwoControllers, puzzleTwoTilemapControllers, puzzleTwoColorChanger;
    // Start is called before the first frame update
    void Start()
    {
        //formating gameobjects when game starts
        puzzleTwoDoors = new GameObject();
        puzzleTwoControllers = new GameObject();
        puzzleTwoTilemapControllers = new GameObject();
        puzzleTwoColorChanger = new GameObject();
        puzzleTwoDoors.name = "PuzzleTwoDoors";
        puzzleTwoControllers.name = "PuzzleTwoControllers";
        puzzleTwoTilemapControllers.name = "PuzzleTwoTilemapControllers";
        puzzleTwoColorChanger.name = "PuzzleTwoColorChanger";
        puzzleTwoDoors.transform.parent = this.transform;
        puzzleTwoControllers.transform.parent = this.transform;
        puzzleTwoTilemapControllers.transform.parent = this.transform;
        puzzleTwoColorChanger.transform.parent = this.transform;

        //Doors
        foreach (UniquePrefabInfo UPI in doorPrefabInfo)
        {
            //instantiate
            GameObject temp = Instantiate(doorPrefab, UPI.prefabLoc, Quaternion.Euler(0, 0, UPI.prefabRot));
            //scale
            temp.transform.localScale = UPI.prefabScale;
            //parent the object
            temp.transform.parent = puzzleTwoDoors.transform;
        }

        //Controllers
        foreach (UniquePrefabInfo UPI in controllerPrefabInfo)
        {
            //instantiate
            GameObject temp = Instantiate(doorControllerPrefab, UPI.prefabLoc, Quaternion.Euler(0, 0, UPI.prefabRot));
            //scale
            temp.transform.localScale = UPI.prefabScale;
            //parent the object
            temp.transform.parent = puzzleTwoControllers.transform;
        }

        //Tilemap Controllers
        foreach (UniquePrefabInfo UPI in tileMapControllerPrefabInfo)
        {
            GameObject tilemapControllerPrefab = tilemapControllerGreenPrefab;

            switch (UPI.colorIndex)
            {
                case 1:
                    /* Default to Green
                    tilemapControllerPrefab = tilemapControllerGreenPrefab;
                    */
                    break;
                case 2:
                    tilemapControllerPrefab = tilemapControllerRedPrefab;
                    break;
                case 3:
                    tilemapControllerPrefab = tilemapControllerBluePrefab;
                    break;
            }

            //instantiate
            GameObject temp = Instantiate(tilemapControllerPrefab, UPI.prefabLoc, Quaternion.Euler(0, 0, UPI.prefabRot));
            //scale
            temp.transform.localScale = UPI.prefabScale;
            //parent the object
            temp.transform.parent = puzzleTwoTilemapControllers.transform;

            //set the prefab to have specific tilemaps enabled and disabled
            ColoredWallPuzzle cwp;
            cwp = temp.GetComponentInChildren<ColoredWallPuzzle>();
            switch (temp.name)
            {
                case "GreenOff(Clone)":
                    cwp.SetTilemap(Tilemaps[0], Tilemaps[1], Tilemaps[2]);
                    break;
                case "RedOff(Clone)":
                    cwp.SetTilemap(Tilemaps[1], Tilemaps[0], Tilemaps[2]);
                    break;
                case "BlueOff(Clone)":
                    cwp.SetTilemap(Tilemaps[2], Tilemaps[0], Tilemaps[1]);
                    break;
            }
        }
        //Color Change Items
        foreach (UniquePrefabInfo UPI in colorChangerPrefabInfo)
        {
            //instantiate
            GameObject temp = Instantiate(colorChangerPrefab, UPI.prefabLoc, Quaternion.Euler(0, 0, UPI.prefabRot));
            //scale
            temp.transform.localScale = UPI.prefabScale;
            //parent the object
            temp.transform.parent = puzzleTwoColorChanger.transform;

            //set the interact color change so that character changes to blue on interact
            InteractColorChange ICC;
            ICC = temp.GetComponentInChildren<InteractColorChange>();
            ICC.SetColorChangeObject(ColorChangeScript);
        }
    }


    //custom class for serialized information for gameobjects
    [System.Serializable]
    public class UniquePrefabInfo
    {
        [SerializeField]
        public Vector2 prefabLoc;
        [SerializeField]
        public float prefabRot;
        [SerializeField]
        public Vector2 prefabScale;
        [SerializeField]
        public int colorIndex;
    }

}
