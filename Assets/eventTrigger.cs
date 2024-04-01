using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player triggered event");
        }
    }

    private void OnTriggerExit(Collider other)
    {
      if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player exited event");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
