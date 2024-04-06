using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplenishOxygenTrigger : MonoBehaviour
{
    [SerializeField]
    private OxygenBar oxygenBar;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EmptyTBCamera.Instance.Overwriten = true;
            EventManager.Instance.WhenInteractedChangeControls = false;
            EventManager.Interacted += ReplenishOxygenBar;
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
        }
    }
    private void ReplenishOxygenBar(bool value)
    {
        oxygenBar.SetOxygen(oxygenBar.GetMaxOxygen());
    }
}
