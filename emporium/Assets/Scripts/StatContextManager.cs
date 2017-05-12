﻿using UnityEngine;

public class StatContextManager : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    private bool useHit = false;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        RaycastHit lasthit = hit;
        useHit = true;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            try
            {
                if (hit.transform.gameObject == lasthit.transform.gameObject) //same thing
                {
                    useHit = false;
                }
            }
            catch { }

            if (hit.collider.tag == "Ground" || DisabledObjectsGameScene.Instance.BuyMenuPanel.activeSelf || DisabledObjectsGameScene.Instance.PressContextPanel.activeSelf)
            {
                if (DisabledObjectsGameScene.Instance.StatsContextPanel.activeSelf)
                {
                    ContextManager.Instance.CloseStatPanel();
                }
                try
                {
                    if (lasthit.transform.gameObject.GetComponent<BuildingScript>().SelectionGlowObject.activeSelf)
                    {
                        lasthit.transform.gameObject.GetComponent<BuildingScript>().SelectionGlowObject.SetActive(false);
                    }
                }
                catch
                {
                }
            }
            else if (hit.transform.tag == "Building" && !DisabledObjectsGameScene.Instance.alertPanel.activeSelf && !DisabledObjectsGameScene.Instance.BuyMenuPanel.activeSelf)
            {
                ContextManager.Instance.ShowStats(hit.transform.gameObject);
                if (!hit.transform.gameObject.GetComponent<BuildingScript>().SelectionGlowObject.activeSelf)
                {
                    hit.transform.gameObject.GetComponent<BuildingScript>().SelectionGlowObject.SetActive(true);
                }

                try
                {
                    if (useHit)
                    {
                        if (lasthit.transform.gameObject.GetComponent<BuildingScript>().SelectionGlowObject.activeSelf)
                        {
                            lasthit.transform.gameObject.GetComponent<BuildingScript>().SelectionGlowObject.SetActive(false);
                        }
                    }
                }
                catch
                {
                }
            }
        }
        else
        {
            if (DisabledObjectsGameScene.Instance.StatsContextPanel.activeSelf)
            {
                ContextManager.Instance.CloseStatPanel();
            }
            try
            {
                if (lasthit.transform.gameObject.GetComponent<BuildingScript>().SelectionGlowObject.activeSelf)
                {
                    lasthit.transform.gameObject.GetComponent<BuildingScript>().SelectionGlowObject.SetActive(false);
                }
            }
            catch
            {
            }
        }
    }

    private void LateUpdate()
    {
        if (DisabledObjectsGameScene.Instance.StatsContextPanel.activeSelf)
        {
            DisabledObjectsGameScene.Instance.StatsContextPanel.transform.position = new Vector3(Input.mousePosition.x + 100, Input.mousePosition.y - 40, Input.mousePosition.z);
        }
    }
}