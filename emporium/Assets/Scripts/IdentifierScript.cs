﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class IdentifierScript : MonoBehaviour {

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
    float fadd = 1f;

    private Text connectingText;


    public bool create = false;


    //new


    InputField inputfplaceholder;
    Text Unamepasstext;

    // Use this for initialization
    void Start () {
      //TODO:  inputf = GameObject.Find("logintextfield").GetComponent<InputField>();
        globalobje = GameObject.Find("GlobalObject");
       
        logcheck = gameObject.GetComponent<LoginCheck>();
        connectingText = GameObject.Find("ConnectingText").GetComponent<Text>();

    }
	

   

    public void setPlayerInfo(string val)
    {

        InputField inpf = GameObject.Find("UnamePassInputField").GetComponent<InputField>();




        if (GlobalControl.Instance.Logincount == 1)
        {
            GlobalControl.Instance.Uname = val;
            GlobalControl.Instance.Logincount++;
            GameObject.Find("UnamePassText").GetComponent<Text>().text = "Enter your password";

        }
        else if (GlobalControl.Instance.Logincount == 2)
        {
            GlobalControl.Instance.Pass = val;
            GlobalControl.Instance.Logincount++;
            Debug.Log(GlobalControl.Instance.Logincount);

            GameObject.Find("UnamePassInputField").GetComponent<InputField>().text = string.Empty;
            
            logcheck.LogInCh(GlobalControl.Instance.Uname, GlobalControl.Instance.Pass);

           // connectingText.color = Color.black; TODO: implement this with BlinkConnecting() and wrong passwords n shit

 

            //TODO: keep flashing connecting until the scene changes?

        }


        inpf.text = string.Empty;
  
        inpf.ActivateInputField();
        inpf.Select();

    }


    private IEnumerator BlinkConnecting()
    {


        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            connectingText.color= Color.Lerp(Color.grey, Color.black, Mathf.PingPong(Time.time * 2, 1));




        }


    }
}
