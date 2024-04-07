using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RadarAnimations : MonoBehaviour
{
    private Animator animManager;
    private void Awake()
    {
        animManager = GetComponent<Animator>(); 
    }
    public void SetDirections(float horizontal, float vertical)
    {
        animManager.SetFloat("H", horizontal);
        animManager.SetFloat("V", vertical);
    }
    public void IsRadarOn(bool value)
    {
        animManager.SetBool("IsFinding", value);
    }
}
