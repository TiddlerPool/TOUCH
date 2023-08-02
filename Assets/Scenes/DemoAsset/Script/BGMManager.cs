using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using AnimationTween;

public class BGMManager : MonoBehaviour
{
    public class DynamicMusic
    {
        public AudioSource Source;
        public float From;
        public float To;
        public float Duration;
    }
    public DynamicMusic BGM_up;
    public DynamicMusic BGM_down;
    public DynamicMusic Phantom_up;
    public DynamicMusic Phantom_down;
    public DynamicMusic Hammer_up;
    public DynamicMusic Hammer_down;

    public Transform Player;
    public Hammer Boss;
    void Start()
    {

    }

    void Update()
    {
        if (Boss.TargetList.Count != 0)
        {
            if (Boss.TargetList.Contains(Player))
            {
                StopAllCoroutines();
                StartCoroutine("TweenVolume", Hammer_up);
                StartCoroutine("TweenVolume", BGM_down);
                StartCoroutine("TweenVolume", Phantom_down);
            }
        }
    }

    private IEnumerator TweenVolume(DynamicMusic music)
    {
        float _time = 0;
        float volume;
        yield return null;
        if (_time < music.Duration)
        {
            volume = Mathf.Lerp(music.From, music.To, Tween.EaseInOut(_time / music.Duration));
            music.Source.volume = volume;
            yield return null;
            _time += Time.deltaTime;
            yield return null;
        }

       
    }
}
