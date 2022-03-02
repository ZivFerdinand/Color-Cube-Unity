using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioPlayer audioPlayer;
    private void OnEnable()
    {
        audioPlayer = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioPlayer>();
    
    }
    
    // public void Update()
    // { 
    //     audioPlayer.Play("1");
    // }
}