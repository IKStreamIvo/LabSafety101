using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private void Start() {
        ControllerControl controllerResult = FindObjectOfType<ControllerControl>();
        if(controllerResult != null) {
            vrController = controllerResult.controller;
            VRMode = true;
        } else {
            VRMode = false;
        }
    }

    #region Controls
    public static bool VRMode { get; private set; }
    public static Transform vrController { get; private set; }
    #endregion 

    #region Pickups
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
    #endregion
}
