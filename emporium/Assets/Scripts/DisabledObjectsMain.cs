﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class DisabledObjectsMain : MonoBehaviour
{

    public static DisabledObjectsMain Instance;

    public GameObject SubmitButton;
    public GameObject UnamePassInputField;
    public GameObject UnamePassText;
    public GameObject MainCanvas;
    public GameObject titleText;
    public GameObject LoginButton;
    public GameObject BugReportPanel;
    public GameObject BugReportButton;
    public GameObject RegisterPanel;
    public GameObject ReportInputField;
    public MenuMusic Menumusic;

    public SocketIOComponent socket;


    public Color blueish;
    public Color defaultcolor;
    // Use this for initialization
    void Start()
    {

        SubmitButton.SetActive(false);
        UnamePassInputField.SetActive(false);
        UnamePassText.SetActive(false);




    }


    void Awake()
    {
        Instance = this;

    }
}
