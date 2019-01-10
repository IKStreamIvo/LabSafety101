using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCControl : MonoBehaviour {

	#region Camera Control
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15f;
	public float sensitivityY = 15f;

	public float minimumX = -360f;
	public float maximumX = 360f;

	public float minimumY = -60f;
	public float maximumY = 60f;

	private float rotationY = 0f;
    #endregion
    public bool lockCursor;
	public new Camera camera;
	public float pointerRange;
    public Vector3 holdObjectOffset;
	private PickupObject targetPickup;
	private PickupObject heldPickup;
	private PlacementArea targetArea;
    private CraftingGridSlot currCraftArea;

    private void Start() {
        if (lockCursor) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
	}
	
	private void Update() {
        if(!Input.GetMouseButton(1)) {
            MouseRotation();
        }
        if(targetPickup == null || !Input.GetMouseButton(1)) {
            CheckPointer();
        }

        if(heldPickup != null) {
            if(Input.GetMouseButtonUp(0)) {
                heldPickup.Release(targetArea);
                GameController.Highlight(heldPickup.type, false);
                heldPickup = null;
            }
        }else if(targetPickup != null) {
			if(Input.GetMouseButtonDown(0)) {
				heldPickup = targetPickup;
				heldPickup.Pickup(camera.transform, holdObjectOffset);
				GameController.Highlight(heldPickup.type, true);
			}
            if(targetPickup.currentArea is CraftingGridSlot) {
                currCraftArea = (CraftingGridSlot)targetPickup.currentArea;
                if(currCraftArea.canCraft) {
                    if(Input.GetMouseButtonDown(1)) {
                        currCraftArea.ActivateCrafting();
                    } else if(Input.GetMouseButtonUp(1)) {
                        currCraftArea.DeactivateCrafting();
                    }
                }
            } 
        }

        if(currCraftArea != null) {
            if(Input.GetMouseButton(1)) {
                currCraftArea.item.transform.Rotate(new Vector3(0f, 0f, -Input.GetAxis("Mouse X") * sensitivityX), Space.Self);
            }
            if(Input.GetMouseButtonUp(1)) {
                currCraftArea.DeactivateCrafting();
                currCraftArea = null;
            }
        }
		
	}

	private void CheckPointer(){
		if(heldPickup == null){ //interactables
			int layerMask = (1 << 8);
			RaycastHit hit;
			Ray ray = camera.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
			if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask)){
				targetPickup = hit.collider.attachedRigidbody.GetComponent<PickupObject>();
			}else{
				targetPickup = null;
			}
		}
		
		if(heldPickup != null){ //placement areas
			int layerMask = (1 << 9);
			RaycastHit hit;
			Ray ray = camera.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
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

	private void MouseRotation(){
		if (axes == RotationAxes.MouseXAndY){
			float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}else if (axes == RotationAxes.MouseX){
			transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
		}else{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}
	}
}
