using UnityEngine;

public class PlacementArea : MonoBehaviour {
    [SerializeField] private bool specificType;
    [SerializeField] private GameController.PickupType acceptsType;
    public Vector3 placePosition {get; private set;}
    public bool ocupied {get; private set;}
    [SerializeField] private MeshRenderer highlightObject;
    [SerializeField] private Color targetedColor;
    private Color defaultColor;
    private bool targeted;
    private bool prevTargetState;

    public virtual void Start() {
        GameController.OnHighlighted += Highlight;

        defaultColor = highlightObject.material.color;
    }

    public virtual void PlaceObject(PickupObject target){
        target.transform.SetParent(transform);
        target.transform.localPosition = placePosition + new Vector3(0f, -target.bounds.center.y / 2f, 0f);
        target.transform.localRotation = target.originalRotation;
        ocupied = true;
        Highlight(false);
    }

    public virtual void RemoveObject(PickupObject target){
        ocupied = false;
        //Highlight(true);
    }

    public void Highlight(GameController.PickupType type, bool state){
        if(specificType){
            if(acceptsType != type){
                return;
            }
        }
        if(!ocupied){
            highlightObject.enabled = state;
        }
    }
    public void Highlight(bool state){
        highlightObject.enabled = state;
    }

    private void Update() {
        if(highlightObject.enabled){
            if(targeted ){
                Material mat = highlightObject.material;
                mat.color = targetedColor;
                highlightObject.material = mat;
            }else{
                Material mat = highlightObject.material;
                mat.color = defaultColor;
                highlightObject.material = mat;
            }
            prevTargetState = targeted;
            targeted = false; //to keep checking if we're being targeted
        }
    }

    public bool TargetHit(GameController.PickupType type){
        if(specificType){
            if(acceptsType != type){
                return false;
            }
        }
        if(!ocupied){
            if(!targeted) targeted = true;
            return true;
        }
        return false;
    }
}