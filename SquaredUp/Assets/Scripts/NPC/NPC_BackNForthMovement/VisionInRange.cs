using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(NPC_Movement))]
public class VisionInRange : MonoBehaviour
{

    bool wasCaught = false;
    [SerializeField][Range(0,1)] float colorSpeed = 0.05f;
    [SerializeField] AudioSource alert;

    private Coroutine activeCoRoutine = null;
    bool isCoRutineActive = false;

    private NPC_Movement npcMovement;

    [SerializeField] private Transform jailCellLocation;

    //Serielaize vision cone to set locations for mesh and polygon collider
    [SerializeField] private GameObject visionCone;
    [SerializeField] private float viewWidth = 6f;
    [SerializeField] private float viewHeight = 14f;
    [SerializeField] private int rayCount = 10;
    //mesh
    Mesh mesh;

    private void Start()
    {
        //make a new mesh
        mesh = new Mesh();
        //set meshfilter of vision cone
        visionCone.GetComponent<MeshFilter>().mesh = mesh;
    }

    private void FixedUpdate()
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
            hit = Physics2D.Raycast(origin, origin + target, target.magnitude);
            // if raycast hits a collider
            if (hit.collider != null)
            {
                //set triangle corner to that point
                vertex = hit.point;
            }
            else
            {
                //set farthest distance on miss
                vertex = origin + target;
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!wasCaught) 
        {
            alert.Play();
            wasCaught = true;
            npcMovement.AllowMove(false);
            InputEvents.AdvanceDialogueEvent += FadeInOut;
            DialogueController.Instance.StartDialogue(new string[] { "HEY YOU, STOP!!!" });
            Debug.Log("change");
            // playerMovementReset.transform.localPosition = new Vector2(0, 0);
            
        }
        
        
    }


    void FadeInOut()
    {
        InputEvents.AdvanceDialogueEvent -= FadeInOut;
        wasCaught = false;
        startFade();
    }

    private void OnDestroy()
    {
        InputEvents.AdvanceDialogueEvent -= FadeInOut;
    }

    void startFade()
    {
        if (isCoRutineActive)
        {
            StopCoroutine(activeCoRoutine);
        }
        activeCoRoutine = StartCoroutine(FadeCoRutine());
    }

    IEnumerator FadeCoRutine()
    {
        Image fadeOutImage = CanvasSingleton.Instance.BlackImage;
        isCoRutineActive = true;
        Color startColor;
        startColor = fadeOutImage.color;
        startColor.a = 0;
        while (fadeOutImage.color.a < 1)
        {
            Color CurrentColor = fadeOutImage.color;
            CurrentColor.a += colorSpeed;
            fadeOutImage.color = CurrentColor;
            yield return null;
        }
        startColor.a = 1;
        fadeOutImage.color = startColor;
        PlayerMovement.Instance.transform.position = jailCellLocation.position;
        while (fadeOutImage.color.a > 0)
        {
            Color CurrentColor = fadeOutImage.color;
            CurrentColor.a -= colorSpeed;
            fadeOutImage.color = CurrentColor;
            yield return null;
        }
        startColor.a = 0;
        fadeOutImage.color = startColor;
        npcMovement.AllowMove(true);
        isCoRutineActive = false;
        yield return null;
    }
    private void Awake()
    {
        npcMovement = GetComponent<NPC_Movement>();
    }
}
