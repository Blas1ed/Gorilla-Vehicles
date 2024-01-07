using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.XR.Interaction.Toolkit;

// Don't roast my code pls its pretty messy
// Also hi AHauntedArmy lol if you looking at this
namespace Gorilla_Vehicles.VehicleUTILS
{
    public class VehicleScript : MonoBehaviour
    {
        public List<WheelCollider> Wheels = new List<WheelCollider>();
        public List<Transform> WheelMeshes = new List<Transform>();
        public GameObject GetInVehicleCollider = new GameObject();
        public Rigidbody rb = new Rigidbody();
        public float maxMotorTorque = 3000f;
        public float maxSteeringAngle = 60f;
        public float VehicleSpeedMutiplyer = 500f;
        public bool SelectedVehicle = false;
        public GameObject GorillaPlayer = new GameObject();

        private float motorInput;
        public float breakForce;
        private float steeringInput;
        private float currentbreakforce;

        public void Awake()
        {
            GorillaPlayer = GameObject.Find("GorillaPlayer");

            rb = gameObject.GetComponent<Rigidbody>();

            rb.centerOfMass = new Vector3(0, 0, 0);
        }
        public void Update()
        {
            if (SelectedVehicle)
            {
                Plugin.Instance.SelectedVehicle = true;
                motorInput = ControllerInputPoller.instance.rightControllerIndexFloat - ControllerInputPoller.instance.leftControllerIndexFloat;
                steeringInput = ControllerInputPoller.instance.rightControllerPrimary2DAxis.x;
                currentbreakforce = ControllerInputPoller.instance.rightControllerSecondaryButton ? breakForce : 0f;
            }
            else
            {
                Plugin.Instance.SelectedVehicle = false;
                motorInput = 0f;
            }
        }

        public void FixedUpdate()
        {         
                ApplyMotorTorque();     
                ApplySteeringAngle();
                UpdateWheels();
                ApplyBreakForce();
        }

        void ApplyMotorTorque()
        {
            Wheels[0].motorTorque = motorInput * VehicleSpeedMutiplyer * Time.deltaTime;
            Wheels[1].motorTorque = motorInput * VehicleSpeedMutiplyer * Time.deltaTime;
        }

        void ApplyBreakForce()
        {
            Wheels[0].brakeTorque = currentbreakforce;
            Wheels[1].brakeTorque = currentbreakforce;
        }

        void ApplySteeringAngle()
        {
            float steerAngle = maxSteeringAngle * steeringInput;
            Wheels[0].steerAngle = steerAngle;
            Wheels[1].steerAngle = steerAngle;
        }

        private void UpdateWheels()
        {
            UpdateSingleWheel(Wheels[0], WheelMeshes[0]);
            UpdateSingleWheel(Wheels[1], WheelMeshes[1]);
            UpdateSingleWheel(Wheels[2], WheelMeshes[2]);
            UpdateSingleWheel(Wheels[3], WheelMeshes[3]);
        }

        private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
        {
            Vector3 pos;
            Quaternion rot
    ; wheelCollider.GetWorldPose(out pos, out rot);
            wheelTransform.rotation = rot;
        }
    }
}
