using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSoundEffect : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource _audioSource;
    [SerializeField] public AudioClip buttonSoundEffect;
    [SerializeField] public AudioClip buttonPressedSoundEffect;

    public void pressed()
    {
        _audioSource.PlayOneShot(buttonPressedSoundEffect);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _audioSource.PlayOneShot(buttonSoundEffect);
    }
}
