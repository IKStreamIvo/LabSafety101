using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeyGuys : MonoBehaviour {
    public AudioClip clip;
    public AudioSource src;
    public void hey() {
        src.time = 0f;
        src.Play();
    }
}
