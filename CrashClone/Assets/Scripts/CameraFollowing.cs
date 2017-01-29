using UnityEngine;
using System.Collections;

public class CameraFollowing : MonoBehaviour {

    [SerializeField]
    private GameObject crashGameObject;
    private Vector3 offsetPosition;
    //private Quaternion offsetRotation;

	// Use this for initialization
	void Start () {

        offsetPosition = transform.position - crashGameObject.transform.position;
        print("Offset Position:" + offsetPosition);
        //offsetRotation = transform.rotation 
	}
	
	// Update is called once per frame
	void Update () {


        transform.position = crashGameObject.transform.position + offsetPosition;
        print("Camera position: " + transform.position);
	}
}
