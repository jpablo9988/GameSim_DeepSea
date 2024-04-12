using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSightManager : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;
    private bool isOn = true;
    void Update()
    {
        if (isOn)
        {
            this.transform.eulerAngles = new Vector3(0, 0 , -playerTransform.eulerAngles.y);
        }
    }

    // Update is called once per frame
    public void IsActive(bool value)
    {
        isOn = value;
    }
}
