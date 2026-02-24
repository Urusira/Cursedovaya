using System;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioClip getDamage;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void damage()
    {
        audioSource.PlayOneShot(getDamage);
    }
}
