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
        // Insantiation
        puzzleOneDoors = new GameObject("PuzzleOneDoors");
        puzzleOneControllers = new GameObject("PuzzleOneControllers");
        // Parenting
        puzzleOneDoors.transform.parent = this.transform;
        puzzleOneControllers.transform.parent = this.transform;
        // Position
        puzzleOneDoors.transform.localPosition = Vector3.zero;
        puzzleOneControllers.transform.localPosition = Vector3.zero;

        //Doors
        foreach(UniquePrefabInfo UPI in doorPrefabInfo)
        {
            //instantiate
            GameObject temp= Instantiate(doorPrefab, Vector3.zero, Quaternion.Euler(0, 0, UPI.prefabRot), puzzleOneDoors.transform);
            // Position
            temp.transform.localPosition = UPI.prefabLoc;
            //scale
            temp.transform.localScale = new Vector3(UPI.prefabScale.x, UPI.prefabScale.y, 1);
        }

        //Controllers
        foreach (UniquePrefabInfo UPI in controllerPrefabInfo)
        {
            //instantiate
            GameObject temp = Instantiate(controllerPrefab, Vector3.zero, Quaternion.Euler(0, 0, UPI.prefabRot), puzzleOneControllers.transform);
            // Position
            temp.transform.localPosition = UPI.prefabLoc;
            //scale
            temp.transform.localScale = new Vector3(UPI.prefabScale.x, UPI.prefabScale.y, 1);
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
