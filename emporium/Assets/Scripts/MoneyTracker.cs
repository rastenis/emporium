﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyTracker : MonoBehaviour {

    float lastDollars;
    bool moneyChanging;

    // Update is called once per frame
    void Update()
    {
        if (Database.UserDollars != lastDollars)
        {
            lastDollars = Database.UserDollars;

            if (moneyChanging)
            {
                StopAllCoroutines();
                StartCoroutine(changeMoney_Effect(lastDollars));

            }else
            {

                StartCoroutine(changeMoney_Effect(lastDollars));
            }


        }



    }


    IEnumerator changeMoney_Effect(float newMoney)
    {
     


        if (newMoney == float.Parse(DisabledObjectsGameScene.moneyEdit.GetComponent<Text>().text)) { Debug.Log("error, shouldnt happen."); }
        else if(newMoney> float.Parse(DisabledObjectsGameScene.moneyEdit.GetComponent<Text>().text))
        {
            while (float.Parse(DisabledObjectsGameScene.moneyEdit.GetComponent<Text>().text) < newMoney)
            {
                yield return new WaitForSeconds(0.01f);
                DisabledObjectsGameScene.moneyEdit.GetComponent<Text>().text = Mathf.Lerp(float.Parse(DisabledObjectsGameScene.moneyEdit.GetComponent<Text>().text), newMoney, 0.1f).ToString();

            }


        }
        else if(newMoney < float.Parse(DisabledObjectsGameScene.moneyEdit.GetComponent<Text>().text))
        {

            while (float.Parse(DisabledObjectsGameScene.moneyEdit.GetComponent<Text>().text) > newMoney)
            {
                yield return new WaitForSeconds(0.01f);
                DisabledObjectsGameScene.moneyEdit.GetComponent<Text>().text = Mathf.Lerp(float.Parse(DisabledObjectsGameScene.moneyEdit.GetComponent<Text>().text), newMoney, 0.1f).ToString();

            }


        }

       
    


    }


}