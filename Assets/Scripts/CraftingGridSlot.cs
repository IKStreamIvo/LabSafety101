using UnityEngine;

public class CraftingGridSlot : PlacementArea {
    private CraftingGrid grid;
    public bool canCraft { get; private set; }
    public bool isCrafting { get; private set; }
    [SerializeField] private Color sqrCraftableColor;
    [SerializeField] private Vector3 craftOffset;
    [SerializeField] private float craftActivateMoveSpeed;
    private Vector3 targetPos;
    private Vector3 controllerOffset;

    public override void Start(){
        base.Start();
        grid = GetComponentInParent<CraftingGrid>();
    }

    public override void Update() {
        base.Update();
        if(ocupied) {
            if(isCrafting) {
                item.CraftingController(controllerOffset);
                if(item.transform.position != targetPos) {
                    item.transform.position = Vector3.MoveTowards(item.transform.position, targetPos, Time.deltaTime * craftActivateMoveSpeed);
                }
            } else {
                if(item.transform.position != targetPos) {
                    item.transform.position = Vector3.MoveTowards(item.transform.position, targetPos, Time.deltaTime * craftActivateMoveSpeed);
                }
            }
        }
    }

    public override void PlaceObject(PickupObject target){
        base.PlaceObject(target);
        grid.PlaceObject(gameObject);
        grid.CheckRecipe(grid.GetIndexByGameObject(gameObject), true);
        targetPos = target.transform.position;
    }

    public override void RemoveObject(PickupObject target){
        base.RemoveObject(target);
        grid.RemoveObject(gameObject);
        canCraft = false;
        grid.CheckRecipe(grid.GetIndexByGameObject(gameObject), false);
    }

    public void ActivateCrafting() {
        if(!canCraft) return;
        isCrafting = true;
        targetPos = item.gameObject.transform.position + new Vector3(0f, -item.bounds.center.y / 2f, 0f);
        if(GameController.VRMode) {
            controllerOffset = GameController.vrController.transform.position;
        }
    }
    public void DeactivateCrafting() {
        isCrafting = false;
        targetPos = item.transform.position - new Vector3(0f, -item.bounds.center.y / 2f, 0f);
        item.transform.localRotation = item.originalRotation;
    }

    public void CanCraft(bool state) {
        if(ocupied) {
            canCraft = state;
            if(state) {
                sqrRenderer.color = sqrCraftableColor;
            } else {
                sqrRenderer.color = sqrOcupiedColor;
            }
        } else {
            canCraft = false;
        }
    }
}