using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confetti : MonoBehaviour
{
    public bool play;
    [SerializeField] private List<ParticleSystem> confetti = new List<ParticleSystem>();

    void Update()
    {
        if (play)
        {
            foreach (ParticleSystem ps in confetti)
            {
                ps.Play();
            }
            play = false;
        }
    }
}
