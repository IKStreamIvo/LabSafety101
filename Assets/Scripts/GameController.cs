using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    private void Start() {
        

        ControllerControl controllerResult = FindObjectOfType<ControllerControl>();
        if(controllerResult != null) {
            vrController = controllerResult.controller;
            VRMode = true;
        } else {
            VRMode = false;
        }

        mainCamera = Camera.main.transform;

        dialogBox = _dialogBox;
        StartDialog();
    }

    private void Update() {
        if(OVRInput.GetDown(OVRInput.Button.Two)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    #region Controls
    public static bool VRMode { get; private set; }
    public static Transform mainCamera { get; private set; }
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

    #region Dialogs
    [SerializeField] private TextMeshPro _dialogBox;
    private static TextMeshPro dialogBox;
    public static void LogDialog(string message, bool onNextLine = false) {
        string output;
        if(onNextLine) {
            output = dialogBox.text + "\n" + message;
        } else {
            output = message;
        }
        dialogBox.SetText(output);
    }

    private void StartDialog() {
        string text = "Welcome, pickup a beaker using the trigger button on your controller,";
        text += "\nthen place it on a highlighted square. Put the other beaker next to it.";
        text += "\nPress and hold the touchpad on top the controller, while pointing at one of the beakers.";
        text += "\nNow rotate your hand to start mixing! When done put the correct PH in the correct waste container.";

        LogDialog(text);
    }
    #endregion
}
