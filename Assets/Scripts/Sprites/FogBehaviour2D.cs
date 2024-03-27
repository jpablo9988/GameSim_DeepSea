using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FogBehaviour2D : MonoBehaviour
{
    [SerializeField]
    private Color _startingColor = Color.white;

    [SerializeField]
    private float _minValue = 3.0f, _maxValue = 10.0f;

    [SerializeField]
    private Transform _target;

    private Color _fogColor;

    private float _currAlpha;

    [SerializeField]
    private SpriteRenderer _material;

    private Color _currColor;


    private void Awake()
    {
        _material = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        _fogColor = RenderSettings.fogColor;
        _currAlpha = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_material.isVisible)
            CalculateAlphaFromTarget();
    }

    private void CalculateAlphaFromTarget()
    {
        float distance = Vector3.Distance(this.transform.position, _target.position);
        distance = Mathf.Clamp(distance, _minValue, _maxValue);
        distance -= this._minValue;


        _currAlpha = 1 - (distance / (_maxValue - _minValue));
        _currColor = Color.Lerp(_fogColor, _startingColor, _currAlpha);

        _currColor.a = _currAlpha;
        _material.color = _currColor;

    }
}
