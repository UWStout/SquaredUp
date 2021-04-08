using UnityEngine;

public class InstantiateLevel2Puzzle2 : MonoBehaviour
{

    [SerializeField]
    private GameObject doorPrefab;
    [SerializeField]
    private GameObject controllerPrefab;
    [SerializeField]
    private UniquePrefabInfo[] doorPrefabInfo, controllerPrefabInfo;
    private GameObject puzzleOneDoors,puzzleOneControllers;
    // Start is called before the first frame update
    void Start()
    {
        //formating gameobjects when game starts
        puzzleOneDoors = new GameObject();
        puzzleOneControllers = new GameObject();
        puzzleOneDoors.name = "PuzzleOneDoors";
        puzzleOneControllers.name = "PuzzleOneControllers";
        puzzleOneDoors.transform.parent = this.transform;
        puzzleOneControllers.transform.parent = this.transform;

        //Doors
        foreach(UniquePrefabInfo UPI in doorPrefabInfo)
        {
            //instantiate
            GameObject temp= Instantiate(doorPrefab,UPI.prefabLoc, Quaternion.Euler(0, 0, UPI.prefabRot));
            //scale
            temp.transform.localScale = new Vector3(UPI.prefabScale.x, UPI.prefabScale.y, 1);
            //parent the object
            temp.transform.parent = puzzleOneDoors.transform;
        }

        //Controllers
        foreach (UniquePrefabInfo UPI in controllerPrefabInfo)
        {
            //instantiate
            GameObject temp = Instantiate(controllerPrefab, UPI.prefabLoc, Quaternion.Euler(0, 0, UPI.prefabRot));
            //scale
            temp.transform.localScale = new Vector3(UPI.prefabScale.x, UPI.prefabScale.y, 1);
            //parent the object
            temp.transform.parent = puzzleOneControllers.transform;
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
    }
}
