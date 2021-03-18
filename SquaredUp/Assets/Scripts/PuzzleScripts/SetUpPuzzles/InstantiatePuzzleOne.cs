using UnityEngine;

public class InstantiatePuzzleOne : MonoBehaviour
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
        puzzleOneDoors = new GameObject();
        puzzleOneControllers = new GameObject();
        puzzleOneDoors.name = "PuzzleOneDoors";
        puzzleOneControllers.name = "PuzzleOneControllers";
        puzzleOneDoors.transform.parent = this.transform;
        puzzleOneControllers.transform.parent = this.transform;

        //Doors
        foreach(UniquePrefabInfo UPI in doorPrefabInfo)
        {
            GameObject temp= Instantiate(doorPrefab,UPI.prefabLoc, Quaternion.Euler(0, 0, UPI.prefabRot));
            temp.transform.localScale = UPI.prefabScale;
            temp.transform.parent = puzzleOneDoors.transform;
        }

        //Controllers
        foreach (UniquePrefabInfo UPI in controllerPrefabInfo)
        {
            GameObject temp = Instantiate(controllerPrefab, UPI.prefabLoc, Quaternion.Euler(0, 0, UPI.prefabRot));
            temp.transform.localScale = UPI.prefabScale;
            temp.transform.parent = puzzleOneControllers.transform;
        }
    }

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
