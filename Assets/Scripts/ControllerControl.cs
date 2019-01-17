using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControllerControl : MonoBehaviour {

    public Transform controller;
    [SerializeField] private LineRenderer pointerLine;
    [SerializeField] private float pointerRange;
    public Vector3 holdObjectOffset;
    private PickupObject targetPickup;
    private PickupObject heldPickup;
    private PlacementArea targetArea;
    private Container targetContainer;
    private CraftingGridSlot currCraftArea;
    private Vector3 forward;
    private float lineLength;
    private Vector3 controllerOrientation;
    [SerializeField] private TextMeshPro debugText;

    void Start () {
        UpdatePointer();
    }
	
	private void Update (){
        string debugTextConstructor = "";
		if (OVRInput.IsControllerConnected(OVRInput.Controller.RTrackedRemote)){
            UpdatePointer();
            if(currCraftArea != null) {
                if(!currCraftArea.isCrafting) {
                    CheckPointer();
                }
            } else {
                CheckPointer();
            }

            if(heldPickup != null) {
                if(OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger)) {
                    heldPickup.Release(targetArea, targetContainer);
                    GameController.Highlight(heldPickup.type, false);
                    heldPickup = null;
                }
            } else if(targetPickup != null) {
                debugTextConstructor += targetPickup + "\n";
                if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) {
					heldPickup = targetPickup;
					heldPickup.Pickup(controller.transform, holdObjectOffset);
					GameController.Highlight(heldPickup.type, true);
                }
                if(targetPickup.currentArea is CraftingGridSlot) {
                    debugTextConstructor += targetPickup.currentArea.name + "\n";
                    CraftingGridSlot craftarea = (CraftingGridSlot)targetPickup.currentArea;
                    if(craftarea.canCraft) {
                        currCraftArea = craftarea;
                        if(OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad) && !currCraftArea.isCrafting) {
                            currCraftArea.ActivateCrafting();
                        }else if(OVRInput.GetUp(OVRInput.Button.PrimaryTouchpad) && currCraftArea.isCrafting) {
                            currCraftArea.DeactivateCrafting();
                        } //else 
                    }
                }
            }
            debugTextConstructor += (currCraftArea != null).ToString() + "\n";
            if(currCraftArea != null) {
                if(targetPickup == null) {
                    currCraftArea = null;
                }
                if(OVRInput.Get(OVRInput.Button.PrimaryTouchpad)) {
                    Vector3 currentOrientation = controller.rotation.eulerAngles;
                    Vector3 difference = (currentOrientation - controllerOrientation);
                    currCraftArea.item.transform.Rotate(new Vector3(0, 0, difference.z), Space.Self);
                }
                if(OVRInput.GetUp(OVRInput.Button.PrimaryTouchpad) && currCraftArea.isCrafting) {
                    currCraftArea.DeactivateCrafting();
                    currCraftArea = null;
                }
            }

            controllerOrientation = controller.rotation.eulerAngles;            
        }

        debugText.SetText(debugTextConstructor);
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
            }
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 11))
            {
                Container waistContainer = hit.collider.GetComponent<Container>();
                if (waistContainer.TargetHit(heldPickup.type))
                    targetContainer = waistContainer;
                else
                    targetContainer = null;
            }
            else
            {
				targetArea = null;
                targetContainer = null;
            }
		}

        if(heldPickup == null) {
            RaycastHit hit;
            Ray ray = new Ray(controller.transform.position, forward);
            if(Physics.Raycast(ray, out hit, Mathf.Infinity)){
                lineLength = hit.distance;
            }
        }
	}

	private void UpdatePointer(){
		forward = controller.TransformDirection(Vector3.forward);
		pointerLine.SetPosition(0, controller.position);
		pointerLine.SetPosition(1, controller.position + forward * lineLength);
	}
}
