using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	public static AudioManager instance;
	public AudioSource[] soundEffects;
	public AudioSource bgm;
  
  
  private void Awake() {
    instance = this;
  }
  
  public void PlaySFX(int soundToPlay) {
    soundEffects[soundToPlay].Stop();
    soundEffects[soundToPlay].pitch = Random.Range(0.8f, 1.2f);
    soundEffects[soundToPlay].Play();
  }
  
  public void PlaylevelMusic() {
    bgm.Play();
  }
  
}
