using UnityEngine;

public class CraftingGridSlot : PlacementArea {
    private CraftingGrid grid;

    public override void Start(){
        base.Start();
        grid = GetComponentInParent<CraftingGrid>();
    }

    public override void PlaceObject(PickupObject target){
        base.PlaceObject(target);
        grid.PlaceObject(gameObject);
    }

    public override void RemoveObject(PickupObject target){
        base.RemoveObject(target);
        grid.RemoveObject(gameObject);
    }
}