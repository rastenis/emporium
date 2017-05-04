﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System.Text.RegularExpressions;
using System.Text;

public class PriceManager : MonoBehaviour {

    public float priceUpdateTimer = 999f;
    private bool firstPricesTaken = false;



	// Use this for initialization
	void Start () {
      
		
	}
	
	// Update is called once per frame
	void Update () {


        if (priceUpdateTimer>0)
        {
            priceUpdateTimer -= Time.deltaTime;
        }else
        {
            priceUpdateTimer = 30f;
            retrievePrices();
        }
	}

    private void retrievePrices()
    {

        DisabledObjectsGameScene.Instance.managerialScripts.GetComponent<SocketManager>().RetrievePrices();

    }
    public void ResolvePrices(SocketIOEvent evt)
    {

        string evtStringRows = evt.data.ToString();


        StringBuilder builder = new StringBuilder(evtStringRows);///////////////////////////////////////////////////////
        builder.Replace("rows", "Items");/////////////////////////JSON string paruosiamas konvertavimui i class object array.
        string evtStringItems = builder.ToString(); //////////////////////////////////////////////////////////////////////

        if (!firstPricesTaken)
        {
            firstPricesTaken = true;
            Database.Instance.prices = JsonHelper.FromJson<Prices>(evtStringItems);
            Database.Instance.oldPrices = Database.Instance.prices;
            Database.Instance.Prices = HelperScripts.Instance.ReassignPrices(Database.Instance.prices[0]);

        }else
        {

            Database.Instance.oldPrices = Database.Instance.prices;
            Database.Instance.prices = JsonHelper.FromJson<Prices>(evtStringItems);
            Database.Instance.Prices = HelperScripts.Instance.ReassignPrices(Database.Instance.prices[0]);
        }


    }

    public Prices SalePanelAdaptingPrices()
    {
        

        return Database.Instance.prices[0];
    }

    
}