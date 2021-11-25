using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.UI;

public class MusicSystem : MonoBehaviour
{
    public Sound[] Sounds;
    public AudioMixerGroup mixerGroup;
    public AudioMixer Mixer;
    public static float volumelock;
    public Slider slider,slider2;
    void Awake() 
    {
        foreach (Sound s in Sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.loop = true;
            s.source.clip = s.Clip; 
            s.source.outputAudioMixerGroup = mixerGroup; 
        }
        //slider.value = volumelock;
        //AudioPLayer.Play(Sounds[1]);
    }
    void Update() 
    {
        slider.value = volumelock;   
        slider2.value = volumelock; 
    }

    public void Play(string name)
    {
        Sound s = Array.Find(Sounds, Sound => Sound.Name == name);
        if(s != null)
            s.source.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(Sounds, Sound => Sound.Name == name);
        if(s != null)
            s.source.Stop();
    }
    public void Death(string name)
    {
        foreach (Sound a in Sounds)
        {          
            a.source.Stop();
        }
        Sound s = Array.Find(Sounds, Sound => Sound.Name == name);
        if(s != null)
            s.source.PlayOneShot(s.source.clip);
            //s.source.PlayOneShot(Oneshots[2]);
    }
    public void SoundEffects(string name)
    {
        Sound s = Array.Find(Sounds, Sound => Sound.Name == name);
        if(s != null)
            s.source.PlayOneShot(s.source.clip);
    }
    public void SetVolume (float volume)
    { 
        Mixer.SetFloat("AllVolume", volume);
        if(volume <= -40)
        {
            Mixer.SetFloat("AllVolume", -80);
        }
        volumelock = volume;
    }
    public void MuteAll()
    {

        Mixer.SetFloat("AllVolume",-80);
        volumelock = -80;
    }

}
