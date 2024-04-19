using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distance_Checker : MonoBehaviour
{
    public GameObject Cube;
    public GameObject Controller;
    public float distance;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(Cube.transform.position,Controller.transform.position);
    }
}
