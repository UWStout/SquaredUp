using UnityEngine;

public class ColoredWallPuzzle : Interactable
{
    [SerializeField]
    private GameObject tilemapDisable;
    [SerializeField]
    private GameObject[] otherTileMaps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        foreach(GameObject g in otherTileMaps)
        {
            g.SetActive(true);
        }
        tilemapDisable.SetActive(false);
    }

    public void SetTilemap(GameObject disable, GameObject enable1, GameObject enable2)
    {
        tilemapDisable = disable;
        otherTileMaps = new GameObject[2];
        otherTileMaps[0] = enable1;
        otherTileMaps[1] = enable2;
    }
}
