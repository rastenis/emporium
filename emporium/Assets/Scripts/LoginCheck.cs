﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using SocketIO;
using UnityStandardAssets.ImageEffects;


public class LoginCheck : MonoBehaviour
{
    public string inputusername;
    public string inputpassword;


    public Color NormalTextColor, RedTextColor, BlueTextColor;

    public SocketIOComponent socket;

    // Use this for initialization
    void Start()
    {

        socket = DisabledObjectsMain.Instance.socket;
        socket.On("PASS_CHECK_CALLBACK", OnLoginCheckCallback);

    }




    void CheckLoginDetails(string username, string password)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["Uname"] = username;
        data["Upass"] = password;
        socket.Emit("CHECK_LOGIN", new JSONObject(data));
    }

    private void OnLoginCheckCallback(SocketIOEvent evt)
    {



        LoginorCreate(int.Parse(evt.data.GetField("passStatus").ToString()));
    }



    public void LogInCh(string un, string pass)
    {
        CheckLoginDetails(un, pass);


    }


    string JsonToString(string target, string s)
    {

        string[] newString = Regex.Split(target, s);

        return newString[1];

    }

    void LoginorCreate(int stat)
    {
        if (stat == 0)
        {  //pass incorrect
            Reaskforlogininfo();
        }
        else if (stat == 1)
        { //pass correct

            proceedToGameScene();

        }
        else if (stat == 2)
        { //user not in DB
            NoUser();
        }
        else if (stat == 3) //reimplement kad net neparodytu main screen jei pareina connection is kito PC;
        {
            Debug.Log("user already logged in.");
            //user already logged in from another PC.
            //TODO: Flash "logged in already" here, and reask for password

        }


    }
    void proceedToGameScene()
    {
        StartCoroutine(LVLLoadCamEffect());
        SceneManager.LoadScene("GameScene");
    }
    void Reaskforlogininfo()
    {

        Debug.Log("REASKING FOR LOGIN INFO");
        GlobalControl.Instance.Pass = null;
        GlobalControl.Instance.Uname = null;
        GlobalControl.Instance.Logincount = 1;
        DisabledObjectsMain.Instance.UnamePassInputField.GetComponent<InputField>().text = "";
        DisabledObjectsMain.Instance.UnamePassText.GetComponent<Text>().text = "Enter your username:";

        StartCoroutine(NotifyText(GameObject.Find("UnamePassText"), "Wrong Password.", RedTextColor));
    }

    void NoUser()
    {

        Debug.Log("asking to create a user");
        GlobalControl.Instance.Pass = null;
        GlobalControl.Instance.Uname = null;
        GlobalControl.Instance.Logincount = 1;
        DisabledObjectsMain.Instance.UnamePassInputField.GetComponent<InputField>().text = "";

        StartCoroutine(NotifyText(GameObject.Find("UnamePassText"), "No user found.", BlueTextColor));

    }

    IEnumerator NotifyText(GameObject txtobject, string notification, Color notificationColor)
    {
        string Oldtext = txtobject.GetComponent<Text>().text;



        txtobject.GetComponent<Text>().text = notification;
        txtobject.GetComponent<Text>().color = notificationColor;


        yield return new WaitForSeconds(1.5f);


        txtobject.GetComponent<Text>().text = Oldtext;
        txtobject.GetComponent<Text>().color = NormalTextColor;
    }


    IEnumerator LVLLoadCamEffect()
    {

        while (Camera.main.GetComponent<Bloom>().bloomThreshold > 0.01)
        {
            //fadeoutas
            yield return new WaitForSeconds(0.001f);
            Globals.Instance.cameraBloom.bloomThreshold -= 0.01f;
        }
    }



}
