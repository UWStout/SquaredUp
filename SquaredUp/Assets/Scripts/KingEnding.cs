using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingEnding : MonoBehaviour
{
    [SerializeField] private Follow camFollow = null;
    [SerializeField] private Transform kingTrans = null;
    [SerializeField] private Transform playerTrans = null;

    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float moveTime = 5.0f;
    [SerializeField] private Vector3 playerEndPos = Vector3.zero;

    [SerializeField] private float fadeOutTime = 1.0f;
    [SerializeField] private string noControlsMapName = "EndGame";


    private Vector3 kingOriginalPosition = Vector3.zero;


    public void StartEnding()
    {
        camFollow.StopFollow();
        InputController.Instance.SwitchInputMap(noControlsMapName);
        StartCoroutine(MoveCoroutine());
    }


    private IEnumerator MoveCoroutine()
    {
        kingOriginalPosition = kingTrans.position;

        float t = 0;
        while (t < moveTime)
        {
            kingTrans.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            playerTrans.Translate(Vector3.right * moveSpeed * Time.deltaTime);

            t += Time.deltaTime;
            yield return null;
        }

        CanvasSingleton.Instance.StartFadeOutAndIn(fadeOutTime, Teleport, RestoreInput);
    }

    private void Teleport()
    {
        kingTrans.position = kingOriginalPosition;
        playerTrans.position = playerEndPos;
        camFollow.StopFollow();
    }

    private void RestoreInput()
    {
        InputController.Instance.PopInputMap(noControlsMapName);
    }
}
