using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugText : MonoBehaviour {
    private static TextMeshPro log;
    public static void VRLog(string text) {
        log.SetText(log.text + "\n" + text);
    }

    private void Awake(){
        log = GetComponent<TextMeshPro>();
    }
}
