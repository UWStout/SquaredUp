using UnityEngine;

[RequireComponent(typeof(IGuardMovement))]
public class VisionInRange : MonoBehaviour
{
    [SerializeField] private AudioSource alert = null;
    [SerializeField] private Transform jailCellLocation = null;
    [SerializeField] private GameObject visionCone = null;
    [SerializeField] private Transform visionOffset = null;
    [SerializeField] private string[] voiceLines = new string[] { "HEY YOU! STOP!!!" };
    [SerializeField] [Range(0, 1)] private float colorSpeed = 0.05f;
    [SerializeField] private float viewWidth = 6f;
    [SerializeField] private float viewHeight = 14f;
    [SerializeField] private int rayCount = 10;
    [SerializeField] private LayerMask layerMask = 1;

    private IGuardMovement npcMovement = null;
    private Mesh mesh = null;
    private bool wasCaught = false;

    // Called 0th
    // Set references
    private void Awake()
    {
        npcMovement = GetComponent<IGuardMovement>();
    }
    // Called 1st
    // Initialization
    private void Start()
    {
        //make a new mesh
        mesh = new Mesh();
        //set meshfilter of vision cone
        visionCone.GetComponent<MeshFilter>().mesh = mesh;
    }
    // Called when this component is destroyed
    // Unsubscribe from ALL events
    private void OnDestroy()
    {
        InputEvents.AdvanceDialogueEvent -= FadeInOut;
    }
    // Called at a fixed interval of time
    // Do physics calculations
    void FixedUpdate()
    {
        //view width start spot
        float viewLeft = (viewWidth / 2);
        //set origin for function
        Vector3 origin = Vector3.zero;
        //set xincrease amount
        float Xincrease = viewWidth / rayCount;
        //set up
        Vector3[] vertices = new Vector3[rayCount + 2];
        //set up
        Vector2[] uv = new Vector2[vertices.Length];
        //set up triangles
        int[] triangles = new int[rayCount * 3];
        //set first vertex
        vertices[0] = origin;
        //indexing
        int vertexIndex = 1;
        int triangleIndex = 0;
        //loop through each triangle
        for (int iHit = 0; iHit <= rayCount; iHit++)
        {
            //get target
            Vector3 target = new Vector3(viewLeft, -viewHeight, 0f);
            //set up vertex
            Vector3 vertex;

            //ray cast
            RaycastHit2D hit;
            hit = Physics2D.Raycast(origin + visionOffset.position, visionOffset.rotation * target, target.magnitude, layerMask);
            // if raycast hits a collider
            if (hit.collider != null)
            {
                //set triangle corner to that point
                vertex = Quaternion.Inverse(visionOffset.rotation) * (hit.point - (Vector2)visionOffset.position);
            }
            else
            {
                //set farthest distance on miss
                vertex = target;
            }
            //add vertex to the index
            vertices[vertexIndex] = vertex;
            //if there is a full triangle
            if (iHit > 0)
            {
                //build triangles;
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }
            //incrmemnt index
            vertexIndex++;
            //decrease by the xincrease to build collider from right to left
            viewLeft -= Xincrease;
        }
        //make an array to send to 2D collider
        Vector2[] vertices2D = new Vector2[vertices.Length];
        //for each loop to build 2D collider array
        int i2D = 0;
        foreach (Vector3 temp in vertices)
        {
            vertices2D[i2D] = (Vector2)temp;
            i2D++;
        }
        //build everything
        visionCone.GetComponent<PolygonCollider2D>().points = vertices2D;
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateBounds();
    }

    // Called when the trigger on this object is involved with a collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!wasCaught && collision.CompareTag("Player"))
        {
            alert.Play();
            wasCaught = true;
            npcMovement.AllowMove(false);
            InputEvents.AdvanceDialogueEvent += FadeInOut;
            DialogueController.Instance.StartDialogue(voiceLines);
        }
    }


    /// <summary>Start fading out the screen. When faded out, move the player. After faded back in let the guard move again.</summary>
    private void FadeInOut()
    {
        InputEvents.AdvanceDialogueEvent -= FadeInOut;
        wasCaught = false;
        CanvasSingleton.Instance.StartFadeOutAndIn(colorSpeed, MovePlayerToJail, AllowNPCMoveAgain);
    }
    /// <summary>Set the player to be at the jail.</summary>
    private void MovePlayerToJail()
    {
        PlayerMovement.Instance.SetPlayerPosition(jailCellLocation.position);
    }
    /// <summary>Let the guard move again.</summary>
    private void AllowNPCMoveAgain()
    {
        npcMovement.AllowMove(true);
    }
}
