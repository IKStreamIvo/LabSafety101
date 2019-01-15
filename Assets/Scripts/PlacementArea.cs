using UnityEngine;

public class PlacementArea : MonoBehaviour {
    [SerializeField] private bool isContainer = false;
    [SerializeField] private bool specificType;
    [SerializeField] private GameController.PickupType acceptsType;
    public Vector3 placePosition {get; private set;}
    public bool ocupied {get; private set;}
    [SerializeField] private MeshRenderer highlightObject;
    [SerializeField] private Color targetedColor;
    private Color defaultColor;
    private bool targeted;
    private bool prevTargetState;
    public PickupObject item { get; private set; }
    public Color sqrDefaultColor { get; private set; }
    [SerializeField] public Color sqrOcupiedColor;
    public SpriteRenderer sqrRenderer { get; private set; }

    public virtual void Start() {
        GameController.OnHighlighted += Highlight;

        defaultColor = highlightObject.material.color;
        sqrRenderer = GetComponentInChildren<SpriteRenderer>();
        sqrDefaultColor = sqrRenderer.color;
    }

    public virtual void PlaceObject(PickupObject target){
        item = target;
        target.transform.SetParent(transform);
        target.transform.localPosition = placePosition + new Vector3(0f, -target.bounds.center.y / 2f, 0f);
        target.transform.localRotation = target.originalRotation;
        ocupied = true;
        Highlight(false);
        sqrRenderer.color = sqrOcupiedColor;
    }

    public virtual void RemoveObject(PickupObject target){
        ocupied = false;
        item = null;
        sqrRenderer.color = sqrDefaultColor;
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

    public virtual void Update() {
        if(highlightObject.enabled){
            if(targeted){
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

    public virtual bool TargetHit(GameController.PickupType type){
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