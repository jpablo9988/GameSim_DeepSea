using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakingAnimations : MonoBehaviour
{
    private bool shaking = false;
    [SerializeField]
    private float distance = 1.0f;
    [SerializeField]
    private float frequency = 1.0f;
    private float startingX;
    public void ShakeObject(bool isShaking, float distance = 0, float frequency = 0)
    {
        if (isShaking)
        {
            startingX = this.transform.position.x;
            float aux_d, aux_f;

            if (distance > 0) aux_d = distance;
            else aux_d = this.distance;
            if (frequency > 0) aux_f = frequency;
            else aux_f = this.frequency;

            shaking = isShaking;
            StartCoroutine(Shake(aux_d, aux_f));
        }
        else
        {
            shaking = isShaking;
        }
    }
    private IEnumerator Shake (float distance, float frequency)
    {
        Vector3 currPosition = this.transform.position;
        while (shaking)
        {
            currPosition.x = startingX + Mathf.Sin(Time.time * frequency) * distance;
            this.transform.position = currPosition;
            yield return null;
        }
        currPosition.x = startingX;
        this.transform.position = currPosition;
        yield return null;
    }
}
