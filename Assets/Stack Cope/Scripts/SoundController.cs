using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource _click;
    private float _defoultPitch;
    void Start()
    {
        _click = GetComponent<AudioSource>();
        _defoultPitch = 0.76f;
    }

    internal void Click(float mod)
    {
        _click.pitch += mod * 1.5f;
        Debug.Log("Pitch:"+_click.pitch);
        _click.Play();
    }
    internal void Click()
    {
        _click.pitch = _defoultPitch;
        Debug.Log("DefoultPitch:"+_click.pitch);
        _click.Play();
    }
}
