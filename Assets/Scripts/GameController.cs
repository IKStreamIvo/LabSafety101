using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public enum PickupType{
		Default, Craftable
	} 

	public delegate void HighlightAction(PickupType type, bool state);
    public static event HighlightAction OnHighlighted;

	public static void Highlight(PickupType type, bool state){
		if(OnHighlighted != null){
			OnHighlighted(type, state);
		}
	}
}
