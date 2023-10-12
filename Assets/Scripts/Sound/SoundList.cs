using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundList : MonoBehaviour
{
    [SerializeField] List<AudioClip> audioList = new List<AudioClip>();
    private AudioSource source;
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void PlayAtRandom()
    {
        int randomNR = Random.Range(audioList.Count-1, 0);
        source.clip = audioList[randomNR];
        source.Play();
    }
}
