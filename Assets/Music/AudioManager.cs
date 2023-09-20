using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyAudio
{
public class AudioManager : MonoBehaviour
{
// 存储音频的字典
    private static Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    public String[] audioName;
    public AudioClip[] audioClip;
    
    // 加载音频资源
    private void Awake()
    {
        for (int i = 0; i < audioClip.Length; i++)
        {
                //print("d");
                if (!audioClips.ContainsKey(audioName[i]))
                    audioClips.Add(audioName[i],audioClip[i]);
        }
    }

    private void Start()
    {

    }

    public static void LoadAudio(string audioName, AudioClip audioClip)
    {
        if (!audioClips.ContainsKey(audioName))
        {
            audioClips.Add(audioName, audioClip);
        }
        else
        {
            Debug.LogWarning("An audio clip with the name " + audioName + " already exists.");
        }
    }

    // 播放音频
    public static void PlayAudio(string audioName)
    {
        if (audioClips.TryGetValue(audioName, out AudioClip audioClip))
        {
            //AudioSource.PlayClipAtPoint(audioClip, Vector3.zero);
            GameObject audioObject = new GameObject("AudioObject");
            AudioSource audioSource = audioObject.AddComponent<AudioSource>();
            
            audioSource.clip = audioClip;
            
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Audio clip with the name " + audioName + " not found.");
        }
    }
    public static void StopAudio(string audioName)
    {
        if (audioClips.TryGetValue(audioName, out AudioClip audioClip))
        {
            //AudioSource.PlayClipAtPoint(audioClip, Vector3.zero);
            GameObject audioObject = new GameObject("AudioObject");
            AudioSource audioSource = audioObject.AddComponent<AudioSource>();
            
            audioSource.clip = audioClip;
            
            audioSource.Stop();
        }
        else
        {
            Debug.LogWarning("Audio clip with the name " + audioName + " not found.");
        }
    }
}    
}

