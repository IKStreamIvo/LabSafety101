using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public static GameController _;

    private void Start() {
        _ = this;
        canPlay = false;

        ControllerControl controllerResult = FindObjectOfType<ControllerControl>();
        if(controllerResult != null) {
            vrController = controllerResult.controller;
            VRMode = true;
        } else {
            VRMode = false;
        }

        mainCamera = Camera.main.transform;

        StartDialog();
    }

    private void Update() {
        if(OVRInput.GetDown(OVRInput.Button.Two)) {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    #region Controls
    public static bool VRMode { get; private set; }
    public static Transform mainCamera { get; private set; }
    public static Transform vrController { get; private set; }
    #endregion 

    #region Pickups
    public enum PickupType {
        Default, Craftable
    }

    public delegate void HighlightAction(PickupType type, bool state);
    public static event HighlightAction OnHighlighted;

    public static void Highlight(PickupType type, bool state) {
        OnHighlighted?.Invoke(type, state);
    }
    #endregion

    #region Dialogs
    [SerializeField] AudioSource audioPlayer;
    [SerializeField] AudioClip[] voiceLines;
    public static int currentState { private set; get; }
    private static Action[] gameFlow = {
        //Intro      pickup ipad   congrats
        Action.Idle, Action.StartGame, Action.Idle,
        // pickup beaker      release it       repeat (skip pickup)
        Action.PickupFull, Action.PlaceDown, Action.PlaceDown,
        //place empty      congrats     pour one    hey there mr   pour two
        Action.PlaceDown, Action.Idle, Action.Pour, Action.PourStop, Action.PourStop, Action.Idle,
    };
    private static Action pendingAction;
    public static bool canPlay;

    private void StartDialog() {
        currentState = 0;
        if(gameFlow.Length > currentState) {
            pendingAction = gameFlow[currentState];
            PlayAudio(currentState);
        }
    }

    public void ReportAction(Action action) {
        Debug.Log(action.ToString());
        if(pendingAction == action) {
            NextDialog();
        } else if(action == gameFlow[currentState+1]) {
            currentState++;
            NextDialog();
        }
        if(action == Action.StartGame) {
            canPlay = true;
        }
    }

    public void Win() {
        PlayAudio(12);
    }

    public void Lose() {
        PlayAudio(13);
    }

    private void NextDialog() {
        if(currentState == 0) {
            //allow ipad usage
            MenuCurser.usable = true;
        }
        currentState++;
        pendingAction = gameFlow[currentState];
        PlayAudio(currentState);
        DebugText.VRLog(pendingAction.ToString());
    }

    private IEnumerator IdleAction(float delay) {
        yield return new WaitForSeconds(delay);
        ReportAction(Action.Idle);
    }

    private void PlayAudio(int clip) {
        audioPlayer.clip = voiceLines[clip];
        audioPlayer.Play();
        if(pendingAction == Action.Idle) {
            StartCoroutine(IdleAction(voiceLines[clip].length + 1f));
        }
    }

    public enum Action {
        Idle, PickupFull, PickupEmpty, PlaceDown, Pour, PourStop,
        StartGame
    }
    #endregion
}
