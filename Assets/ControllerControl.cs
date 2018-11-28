using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControllerControl : MonoBehaviour {

	public Transform controller;
	public LineRenderer pointerLine;
	public float pointerRange;
	public float velocityMultiplier = 5f;

	private Vector3 forward;
	private float lineLength;
	private GameObject targetObject;
	private Vector3 prevPos; 

	public TMP_Text dbgVelocity;
	public TMP_Text dbgAngularVelocity;

	void Start () {
		forward = controller.TransformDirection(Vector3.forward);
	}
	
	void Update () {
		if (OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote)){
			if(targetObject != null){
				if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)){
					targetObject.transform.SetParent(controller);
					targetObject.GetComponent<Rigidbody>().isKinematic = true;
				}else if(OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger)){
					targetObject.transform.SetParent(null);
					targetObject.GetComponent<Rigidbody>().isKinematic = false;
					Vector3 currPos = targetObject.transform.position;
					targetObject.GetComponent<Rigidbody>().velocity = (currPos - prevPos) / Time.deltaTime * velocityMultiplier;
					//targetObject.GetComponent<Rigidbody>().angularVelocity += OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RTrackedRemote);
				}
				if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)){
					//track velocity
					Vector3 currPos = targetObject.transform.position;
					prevPos = currPos;
				}
			}
			if(!OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)){
				CheckPointer();
			}
			UpdatePointer();
			DebugText();
		}
	}

    private void DebugText(){
		dbgVelocity.SetText(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTrackedRemote).ToString());
		dbgVelocity.SetText(OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RTrackedRemote).ToString());
    }

    private void UpdatePointer(){
		forward = controller.TransformDirection(Vector3.forward);
		pointerLine.SetPosition(0, controller.position);
		pointerLine.SetPosition(1, controller.position + forward * lineLength);
	}

	private void CheckPointer(){
        int layerMask = (1 << 8);
        RaycastHit hit;
        if(Physics.Raycast(controller.position, forward, out hit, pointerRange, layerMask)){
			targetObject = hit.collider.gameObject;
			lineLength = hit.distance;
        }else{
			targetObject = null;
			lineLength = pointerRange;
		}
	}
}
