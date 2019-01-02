using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControllerControl : MonoBehaviour {

    [SerializeField] private Transform controller;
    [SerializeField] private LineRenderer pointerLine;
    [SerializeField] private float pointerRange;
    public Vector3 holdObjectOffset;
    private PickupObject targetPickup;
    private PickupObject heldPickup;
    private PlacementArea targetArea;
    private Vector3 forward;
    private float lineLength;

    void Start () {
	}
	
	private void Update (){
		if (OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote)){
            UpdatePointer();
            CheckPointer();

            if(heldPickup != null) {
                if(OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger)) {
                    heldPickup.Release(targetArea);
                    GameController.Highlight(heldPickup.type, false);
                    heldPickup = null;
                    //pointerLine.enabled = true;
                    pointerLine.startColor = Color.red;
                }
            }else if(targetPickup != null){
				if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)){
					heldPickup = targetPickup;
					heldPickup.Pickup(controller.transform, holdObjectOffset);
					GameController.Highlight(heldPickup.type, true);
                    //pointerLine.enabled = false;
                    pointerLine.startColor = Color.blue;
                }
                if(targetPickup.currentArea is CraftingGridSlot) {
                    CraftingGridSlot craftArea = (CraftingGridSlot)targetPickup.currentArea;
                    if(craftArea.canCraft) {
                        if(OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad)) {
                            craftArea.ActivateCrafting();
                        }
                    }
                }
            }
		}
	}

	private void CheckPointer(){
		if(heldPickup == null){ //interactables
			int layerMask = (1 << 8);
			RaycastHit hit;
            Ray ray = new Ray(controller.transform.position, forward);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)){
                lineLength = hit.distance;
                targetPickup = hit.collider.attachedRigidbody.GetComponent<PickupObject>();
			}else{
				targetPickup = null;
                lineLength = pointerRange;
            }
		}
		
		if(heldPickup != null){ //placement areas
			int layerMask = (1 << 9);
			RaycastHit hit;
            Ray ray = new Ray(controller.transform.position, forward);
			if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)){
				PlacementArea area = hit.collider.GetComponent<PlacementArea>();
				if(area.TargetHit(heldPickup.type)){
					targetArea = area;
				}else{
					targetArea = null;
				}
			}else{
				targetArea = null;
			}
		}
	}

	private void UpdatePointer(){
		forward = controller.TransformDirection(Vector3.forward);
		pointerLine.SetPosition(0, controller.position);
		pointerLine.SetPosition(1, controller.position + forward * lineLength);
	}
}
