﻿using System.Collections;
using UnityEngine;

/// <summary>
/// Handles the behaviour for after talking to the king.
/// </summary>
public class KingEnding : MonoBehaviour
{
    [SerializeField] private Follow camFollow = null;
    [SerializeField] private Transform kingTrans = null;
    [SerializeField] private Transform playerTrans = null;

    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private float moveTime = 5.0f;

    [SerializeField] [Range(0, 1)] private float fadeOutSpeed = 0.03f;
    [SerializeField] private string noControlsMapName = "EndGame";

    [SerializeField] private Transform playerEndPos = null;
    [SerializeField] private GameObject levelThreeEntranceWall = null;

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

        CanvasSingleton.Instance.StartFadeOutAndIn(fadeOutSpeed, OnFadedOut, OnFadedIn);
    }

    private void OnFadedOut()
    {
        kingTrans.position = kingOriginalPosition;
        playerTrans.position = playerEndPos.position;
        levelThreeEntranceWall.SetActive(false);
        camFollow.StartFollow();
    }

    private void OnFadedIn()
    {
        InputController.Instance.PopInputMap(noControlsMapName);
    }
}