using UnityEngine;

public class CraftingGridSlot : PlacementArea {
    private CraftingGrid grid;
    public bool canCraft { get; private set; }
    public bool isCrafting { get; private set; }
    [SerializeField] private Color sqrCraftableColor;

    public override void Start(){
        base.Start();
        grid = GetComponentInParent<CraftingGrid>();
    }

    public override void Update() {
        base.Update();
        if(isCrafting) {
            //move up?
            //Vector3.MoveTowards();
        }
    }

    public override void PlaceObject(PickupObject target){
        base.PlaceObject(target);
        grid.PlaceObject(gameObject);
        grid.CheckRecipe(grid.GetIndexByGameObject(gameObject), true);
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
    }
    public void DeactivateCrafting() {
        isCrafting = false;
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