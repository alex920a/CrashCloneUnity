using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClicker : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       

        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  //mettere il tag "MainCamera" al gameObject Camera nell'editor senno restituisce null 
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.transform != null)
                {
                    PrintName(hit.transform.gameObject);
                    if (hit.transform.gameObject.tag.Equals("Players"))
                        GameScript.Instance.LoadLevel(1);
                }
            }
        }
        
	}

    private void PrintName(GameObject gameObject)
    {
        print(gameObject.name);
    }
}
