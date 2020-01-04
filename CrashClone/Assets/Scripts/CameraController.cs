using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


    public Transform[] views;
    public float transitionSpeed;
    Transform currentView;
    private bool moving = false;


	// Use this for initialization
	void Start () {
        currentView = views[0];
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentView = views[0];
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentView = views[1];
        }
    }

    private void LateUpdate()
    {
        if(moving)
            transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed);
    }


    //attivato al momento del click new game
    internal void TranslateToFirstCharacter()
    {
        moving = true;
        
    }

}
