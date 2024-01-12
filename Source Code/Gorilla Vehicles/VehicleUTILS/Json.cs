using System;
using System.Collections.Generic;
using System.Text;

namespace Gorilla_Vehicles.VehicleUTILS
{
    public class Json
    {
        [System.Serializable]
        public class CarJson
        {
            public string Author;
            public float torque = 125000;
            public float MaxSteeringAngle = 60f;
            public float SpeedMultiplyer = 50000f;
            public float Breakforce = 1000f;
            public string Version = "";
        }
    }
}
