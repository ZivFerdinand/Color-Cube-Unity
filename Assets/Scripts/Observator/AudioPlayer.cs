using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [System.Serializable]
    public struct SFX
    {
        public string name;
        public AudioClip clip;
    }

    [SerializeField] private AudioSource source;
    public SFX[] soundEffects;
    private Dictionary<string, AudioClip> sfxDict = new Dictionary<string, AudioClip>();

    private void Start() 
    {
        sfxDict.Clear();
        for(int i=0;i<soundEffects.Length;i++)
        {
            Debug.Log(soundEffects[i].name);
            sfxDict.Add(soundEffects[i].name, soundEffects[i].clip);
        }
    }
    public void Play(string sfxName)
    {
        source.clip = sfxDict[sfxName];

        source.Play();
    }
}