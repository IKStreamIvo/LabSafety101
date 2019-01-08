using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickupObject : MonoBehaviour {

	public GameController.PickupType type;
	public new Rigidbody rigidbody {get; private set;}
	public Vector3 originalPosition {get; private set;}
	public Quaternion originalRotation {get; private set;}
    public Vector3 originalScale { get; private set; }
    public Bounds bounds {get; private set;}
	public PlacementArea currentArea {get; private set;}

	private void Start() {
		rigidbody = GetComponent<Rigidbody>();

		originalPosition = transform.position;
		originalRotation = transform.rotation;
        originalScale    = transform.localScale;

        Collider[] childColls = rigidbody.GetComponentsInChildren<Collider>();
		Collider[] rootColls = rigidbody.GetComponents<Collider>();
		Collider[] colls = childColls.Concat(rootColls).ToArray();
		bounds = new Bounds (transform.position, Vector3.zero);
		foreach (Collider coll in colls){
			bounds.Encapsulate(coll.bounds);
		}
		
	}

    public void CraftingController(Vector3 offset = new Vector3()) {
        /*if(GameController.VRMode) {
            Vector3 newRot = GameController.vrController.transform.position - offset;
            transform.rotation = Quaternion.Euler(newRot);
        } else {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
        }*/
    }

	public void Release(PlacementArea target){
		if(target == null){
			transform.SetParent(null);
			transform.localPosition = originalPosition;
			transform.localRotation = originalRotation;
            transform.localScale    = originalScale;
		}else{
			target.PlaceObject(this);
			currentArea = target;
		}
	}

	public void Pickup(Transform parent, Vector3 holdObjectOffset){
		if(currentArea != null){
			currentArea.RemoveObject(this);		
		}
		transform.SetParent(parent);
		transform.localPosition = holdObjectOffset;
        transform.localRotation = originalRotation;
        transform.localScale = originalScale;
    }


}
