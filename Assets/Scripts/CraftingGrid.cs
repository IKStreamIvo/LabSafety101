using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CraftingGrid : MonoBehaviour {

	[SerializeField] private GameObject worldButtonPrefab;
	[SerializeField] private CraftingGridSlot[] slots;
	[SerializeField] private int width = 3;
	[SerializeField] private int height = 3;
	private bool[] usedSlots;
	private int eventIndex = 0;
    [SerializeField] private Vector3 buttonOffset;

    private void Start() {
		usedSlots = new bool[width*height];
		CheckRecipe();
	}

	public void PlaceObject(int slot){
		usedSlots[slot] = true;
		CheckRecipe();
	}
	public void PlaceObject(GameObject slot){
		int index = GetIndexByGameObject(slot);
		PlaceObject(index);
	}

	public void RemoveObject(GameObject slot){
		int index = GetIndexByGameObject(slot);
		RemoveObject(index);
	}
	public void RemoveObject(int slot){
		usedSlots[slot] = false;
		CheckRecipe();
	}


	public bool CheckRecipe(){
		//	0	1	2
		//	3	4	5
		//	6	7	8
		for (int i = 0; i < slots.Length; i++){
			//check for ocupied slot
			//get that object
			//loop through possible recipes of that object
			bool ocupied = usedSlots[i];
			if(ocupied){
				//TODO: check for last col and stuff so that you do get the correct references
				CraftingGridSlot slot = slots[i];
				int up = i - width; //dont need this? 
				int left = i - 1; //dont need this?
				int right = i + 1;
				int down = i + width;
				if(right < slots.Length){
					if(usedSlots[right]){
						//Add button
						/*Vector3 btnPos = (slot.transform.position + slots[right].transform.position) / 2f + buttonOffset;
						GameObject worldButton = Instantiate(worldButtonPrefab, btnPos, Quaternion.identity);
						WorldButton btn = worldButton.GetComponent<WorldButton>();*/
						//btn.Setup(CraftThing, eventIndex++);
					}
				}
			}
		}
		return false;
	}
    public void CheckRecipe(int target, bool state) {
        bool foundAtLeastOne = false;
        if(target != 0) {
            if(target % width != 0 && usedSlots[target - 1]) { //check left
                slots[target - 1].CanCraft(state);
                foundAtLeastOne = state;
            } else {
                slots[target - 1].CanCraft(false);
            }
        }
        if(target != width * height-1) {
            if(usedSlots[target + 1] && (target + 1) % width != 0) { //check right
                slots[target + 1].CanCraft(state);
                foundAtLeastOne = state;
            } else {
                slots[target + 1].CanCraft(false);
            }
        }
        slots[target].CanCraft(foundAtLeastOne);
    }

    public void CraftThing(int tag){
		
	}

	public int GetIndexByGameObject(GameObject slotGO){
		for (int i = 0; i < slots.Length; i++){
			CraftingGridSlot slot = slots[i];
			if(slot.gameObject == slotGO){
				return i;
			}
		}
		return -1;
	}
	public CraftingGridSlot GetSlotByIndex(int index){
		return slots[index];
	}
}