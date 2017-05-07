﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class IdentifierScript : MonoBehaviour
{

    //apsibreziami pag. objektai



    GameObject Identpanel;
    LoginCheck logincheck;
    GameObject Networkman;
    GameObject GlobalObj;
    GameObject globalobje;
    LoginCheck logcheck;


    //for fading text
    public TextMesh textmesh;
    public Color def;


    private Text connectingText;


    public bool create = false;


    //new


    InputField inputfplaceholder;
    Text Unamepasstext;

    // Use this for initialization
    void Start()
    {
        //TODO:  inputf = GameObject.Find("logintextfield").GetComponent<InputField>();
        globalobje = GameObject.Find("GlobalObject");

        logcheck = gameObject.GetComponent<LoginCheck>();
        connectingText = GameObject.Find("ConnectingText").GetComponent<Text>();

    }




    public void setPlayerInfo(string val)
    {

        InputField inpf = DisabledObjectsMain.Instance.UnamePassInputField.GetComponent<InputField>();




        if (GlobalControl.Instance.Logincount == 1)
        {
            if (val!="")
            {
                GlobalControl.Instance.Uname = val;
                GlobalControl.Instance.Logincount++;
                DisabledObjectsMain.Instance.UnamePassText.GetComponent<Text>().text = "Enter your password";

            }
            inpf.ActivateInputField();
            inpf.Select();

        }
        else if (GlobalControl.Instance.Logincount == 2)
        {
            if (val != "")
            {
                GlobalControl.Instance.Pass = val;
            GlobalControl.Instance.Logincount++;
            Debug.Log(GlobalControl.Instance.Logincount);

            inpf.text = "";


            logcheck.LogInCh(GlobalControl.Instance.Uname, GlobalControl.Instance.Pass);


            }


            inpf.ActivateInputField();
            inpf.Select();


        }


        inpf.text ="";



    }


    private IEnumerator BlinkConnecting()
    {


        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            connectingText.color = Color.Lerp(Color.grey, Color.black, Mathf.PingPong(Time.time * 2, 1));


        }


    }
}
