﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePlayerPrefs : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        PlayerPrefs.DeleteAll();
    }
}