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
    public Vector3 holdObjectOffset;
	public bool throwingObjects;

	private Vector3 forward;
	private float lineLength;
	private GameObject targetObject;
	private Rigidbody targetRb;
	private Vector3 prevPos; 
	private Vector3 startPickupPos;
    private Quaternion startPickupRot;

	public TMP_Text dbgVelocity;
	public TMP_Text dbgAngularVelocity;

    void Start () {
		forward = controller.TransformDirection(Vector3.forward);
	}
	
	void Update () {
		if (OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote)){
			if(targetRb != null){
				if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)){
					targetRb.transform.SetParent(controller);

					targetRb.isKinematic = true;
					
					startPickupPos = targetRb.transform.position;
					startPickupRot = targetRb.transform.rotation;
					targetRb.transform.localPosition = holdObjectOffset;
					targetRb.transform.localEulerAngles = Vector3.zero;
					pointerLine.enabled = false;
				}else if(OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger)){
					targetRb.transform.SetParent(null);
					targetRb.isKinematic = false;
					if(throwingObjects){
						Vector3 currPos = targetRb.transform.position;
						targetRb.velocity = (currPos - prevPos) / Time.deltaTime * velocityMultiplier;
					}else{
						targetRb.transform.localPosition = startPickupPos;
						targetRb.transform.localRotation = startPickupRot;
						targetRb.velocity = Vector3.zero;
					}
					pointerLine.enabled = true;
				}
				if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)){
					//track velocity
					Vector3 currPos = targetRb.gameObject.transform.position;
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
			targetRb = hit.collider.attachedRigidbody;
			lineLength = hit.distance;
        }else{
			targetRb = null;
			lineLength = pointerRange;
		}
	}
}
