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
        // Insantiation
        puzzleTwoDoors = new GameObject("PuzzleTwoDoors");
        puzzleTwoControllers = new GameObject("PuzzleTwoControllers");
        puzzleTwoTilemapControllers = new GameObject("PuzzleTwoTilemapControllers");
        puzzleTwoColorChanger = new GameObject("PuzzleTwoColorChanger");
        // Parenting
        puzzleTwoDoors.transform.parent = this.transform;
        puzzleTwoControllers.transform.parent = this.transform;
        puzzleTwoTilemapControllers.transform.parent = this.transform;
        puzzleTwoColorChanger.transform.parent = this.transform;
        // Position
        puzzleTwoDoors.transform.localPosition = Vector2.zero;
        puzzleTwoControllers.transform.localPosition = Vector2.zero;
        puzzleTwoTilemapControllers.transform.localPosition = Vector2.zero;
        puzzleTwoColorChanger.transform.localPosition = Vector2.zero;

        //Doors
        foreach (UniquePrefabInfo UPI in doorPrefabInfo)
        {
            //instantiate
            GameObject temp = Instantiate(doorPrefab, Vector3.zero, Quaternion.Euler(0, 0, UPI.prefabRot), puzzleTwoDoors.transform);
            // Position
            temp.transform.localPosition = UPI.prefabLoc;
            //scale
            temp.transform.localScale = new Vector3(UPI.prefabScale.x, UPI.prefabScale.y, 1);
        }

        //Controllers
        foreach (UniquePrefabInfo UPI in controllerPrefabInfo)
        {
            //instantiate
            GameObject temp = Instantiate(doorControllerPrefab, Vector3.zero, Quaternion.Euler(0, 0, UPI.prefabRot), puzzleTwoControllers.transform);
            // Position
            temp.transform.localPosition = UPI.prefabLoc;
            //scale
            temp.transform.localScale = new Vector3(UPI.prefabScale.x, UPI.prefabScale.y, 1);
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
            GameObject temp = Instantiate(tilemapControllerPrefab, Vector3.zero, Quaternion.Euler(0, 0, UPI.prefabRot), puzzleTwoTilemapControllers.transform);
            // Position
            temp.transform.localPosition = UPI.prefabLoc;
            //scale
            temp.transform.localScale = new Vector3(UPI.prefabScale.x, UPI.prefabScale.y, 1);

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
            GameObject temp = Instantiate(colorChangerPrefab, Vector3.zero, Quaternion.Euler(0, 0, UPI.prefabRot), puzzleTwoColorChanger.transform);
            // Position
            temp.transform.localPosition = UPI.prefabLoc;
            //scale
            temp.transform.localScale = new Vector3(UPI.prefabScale.x, UPI.prefabScale.y, 1);

            //set the interact color change so that character changes to blue on interact
            InteractColorChange ICC;
            ICC = temp.GetComponentInChildren<InteractColorChange>();
            ICC.SetColorChangeObject(ChangeColorSkill.Instance.gameObject);
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
