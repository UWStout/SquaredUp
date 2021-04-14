using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(NPC_MovementLoop))]
public class VsionInRangeRepeat : MonoBehaviour
{

    bool wasCaught = false;
    [SerializeField] Image fadeOutImage = null;
    [SerializeField] [Range(0, 1)] float colorSpeed = 0.05f;

    private Coroutine activeCoRoutine = null;
    bool isCoRutineActive = false;

    private NPC_MovementLoop npcMovement;

    public GameObject player;
    // public GameObject playerMovementReset;

    [SerializeField] private Transform jailCellLocation;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!wasCaught)
        {
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
        player.transform.position = jailCellLocation.position;
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
        npcMovement = GetComponent<NPC_MovementLoop>();
    }
}
