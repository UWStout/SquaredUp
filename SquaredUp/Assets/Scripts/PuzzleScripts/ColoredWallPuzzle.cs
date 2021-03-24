using UnityEngine;

public class ColoredWallPuzzle : Interactable
{
    //variables for coloredWall
    [SerializeField]
    private GameObject tilemapDisable;
    [SerializeField]
    private GameObject[] otherTileMaps;

    //override interactable
    public override void Interact()
    {
        //set other tilemaps active
        foreach(GameObject g in otherTileMaps)
        {
            g.SetActive(true);
        }
        //disable target tilemap
        tilemapDisable.SetActive(false);
    }

    //function to get tilemaps when gameobject is created
    public void SetTilemap(GameObject disable, GameObject enable1, GameObject enable2)
    {
        tilemapDisable = disable;
        otherTileMaps = new GameObject[2];
        otherTileMaps[0] = enable1;
        otherTileMaps[1] = enable2;
    }
}
