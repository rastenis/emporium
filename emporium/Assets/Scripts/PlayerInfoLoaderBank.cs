﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using SocketIO;
using UnityStandardAssets.ImageEffects;

public class PlayerInfoLoaderBank : MonoBehaviour {

    public static string UserUsername;
    public static int UserDollars;
    public static int UserLastOnline;
    public static int UserPlotSize;
    GlobalControl globalcontr;
    Database db;
    HelperScripts helperscript;
    AssignTiles assigner;
    

    SocketIOComponent socket;
    
    // Use this for initialization
    void Start () {

        db = gameObject.GetComponent<Database>();

        assigner = gameObject.GetComponent<AssignTiles>();

        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();

        globalcontr = GameObject.Find("GlobalObject").GetComponent<GlobalControl>();

   
        socket.On("connectedToNode", GetStats);
        socket.On("RETRIEVE_STATS", ReceiveStats);
        socket.On("RECEIVE_TILES", ReceiveTileData);
        socket.On("RECEIVE_TILE_INFORMATION", ReceiveTileInformation);
        socket.On("RECEIVE_INVENTORY", ReceiveInventory);
        


    }
	
	// Update is called once per frame

 
    void LoadEverythingAndSetUI()
    {
        Text moneytext = GameObject.Find("MoneyEdit").GetComponent<Text>();
        Text usertext = GameObject.Find("PlayingAsEdit").GetComponent<Text>();

        moneytext.text = Database.UserDollars.ToString();
        usertext.text = GlobalControl.Uname;
     

        RotationScript rotscript = GameObject.Find("Main Camera").GetComponent<RotationScript>();
        rotscript.SetCurrentRotCenter(lygusnelygusPlot());//also sets ground transform

        GameObject.Find("Ground").transform.localScale = new Vector3(Database.UserPlotSize, 0.1f, Database.UserPlotSize) ;


    }
    bool lygusnelygusPlot()
    {
        bool lygnelyg=true;

        if(Database.UserPlotSize % 2==0) //lyginis plot dimensions
        {
            lygnelyg = true;
        }
        else if(Database.UserPlotSize % 2 == 1)//NElyginis plot dimensions
        {
            lygnelyg = false;
        }
        return lygnelyg;
    }

    void GetStats(SocketIOEvent evt)
    {

        Dictionary<string, string>  data = new Dictionary<string, string>();
        data["Uname"] = GlobalControl.Uname;
        
       

        if (globalcontr.ConnectedOnceNoDupeStatRequests)
        {

         
        }
        else {
            socket.Emit("GET_STATS", new JSONObject(data));  //get stats
            Debug.Log("asking for stats");
            globalcontr.ConnectedOnceNoDupeStatRequests = bool.Parse(evt.data.GetField("ConnectedOnceNoDupeStatRequests").ToString());
        }
        
        
    }
    void ReceiveStats(SocketIOEvent evt)
    {
        Debug.Log("got stats!");
       

        Database.UserUsername = GlobalControl.Uname;
        Database.UserDollars = int.Parse(evt.data.GetField("dollars").ToString());
        Database.UserPlotSize = int.Parse(evt.data.GetField("plotsize").ToString());

        string lastOnline = Regex.Replace(evt.data.GetField("lastonline").ToString(), "[^0-9]", "");
        Database.UserLastOnline=int.Parse(lastOnline);

    


        LoadEverythingAndSetUI();

        Dictionary<string, string> data = new Dictionary<string, string>();
        data["Uname"] = GlobalControl.Uname;
        Debug.Log("asking for tile data");

        socket.Emit("GET_TILE_INFORMATION", new JSONObject(data));
        socket.Emit("GET_TILE_DATA", new JSONObject(data));




    }

    void ReceiveTileData(SocketIOEvent evt)
    {
      
        
        assigner.AssignReceivedTiles(evt);

    }

    void ReceiveTileInformation(SocketIOEvent evt)
    {

        
        assigner.AssignTileInformation(evt);

    }

    void ReceiveInventory(SocketIOEvent evt)
    {


        assigner.AssignInventory(evt);

    }


}
