using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplenishOxygenTrigger : MonoBehaviour
{
    [SerializeField]
    private OxygenBar oxygenBar;
    [SerializeField]
    private GameObject oxygenMessage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EmptyTBCamera.Instance.Overwriten = true;
            EventManager.Instance.WhenInteractedChangeControls = false;
            EventManager.Interacted += ReplenishOxygenBar;
            oxygenMessage.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EmptyTBCamera.Instance.Overwriten = false;
            ObjectDirectionChecker.Radar.PlayerOnObject(false);
            EventManager.Instance.WhenInteractedChangeControls = true;
            EventManager.Interacted -= ReplenishOxygenBar;
            oxygenMessage.SetActive(false);

        }
    }
    private void ReplenishOxygenBar(bool value)
    {
        oxygenBar.SetOxygen(oxygenBar.GetMaxOxygen());
    }
}
