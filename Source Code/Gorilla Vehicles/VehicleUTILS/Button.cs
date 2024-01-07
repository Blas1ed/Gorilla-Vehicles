using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Gorilla_Vehicles.VehicleUTILS
{
    // Don't roast my code pls its pretty messy
    // Also hi AHauntedArmy lol if you looking at this
    public class Button : MonoBehaviour
    {

        public bool EnterKey = false;
        public bool Up = false;
        public bool Delayed = false;

        public void OnTriggerEnter(Collider other)
        {
            if (other.name == "RightHandTriggerCollider")
            {
                if (!Delayed)
                {
                if (!EnterKey) 
                {
                    Plugin.Instance.UpdateCurrentTab(Up);
                }
                else
                {
                    Plugin.Instance.EnterCurrentFunction();
                    }

                    StartCoroutine(StartCooldown());
                }

                RedButton(true);
                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(67, false, 0.10f);
            }
        }

        public void OnTriggerExit(Collider other) 
        {
            if (other.name == "RightHandTriggerCollider")
            {
                RedButton(false);
            }
        }

        public void RedButton(bool on)
        {
            switch (on)
            {
                case true:
                    gameObject.GetComponent<Renderer>().material = MainUtils.RedMat; 
                    break;
                case false:
                    gameObject.GetComponent<Renderer>().material = MainUtils.GreenMat;
                    break;
            }

        }

        public IEnumerator StartCooldown()
        {
            Delayed = true;
            yield return new WaitForSeconds(0.3f);
            Delayed = false;
        }
    }
}
