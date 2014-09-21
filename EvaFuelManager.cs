using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace EvaFuel
{
    [KSPAddon(KSPAddon.Startup.SpaceCentre, true)]
    public class EvaFuelManager : MonoBehaviour
    {
        public void Awake()
        {
            GameEvents.onCrewOnEva.Add(this.onEvaStart);
            GameEvents.onCrewBoardVessel.Add(this.onEvaEnd);
        }

        public void onEvaStart(GameEvents.FromToAction<Part, Part> data)
        {
            double fuel = data.from.RequestResource("MonoPropellant", 5);
            if (fuel < 5)
            {
                data.to.RequestResource("EVA Propellant", 5 - fuel);
            }
        }

        public void onEvaEnd(GameEvents.FromToAction<Part, Part> data)
        {
            double fuelleft = data.from.RequestResource("EVA Propellant", 5);
            data.to.RequestResource("MonoPropellant", -fuelleft);
        }
    }
}
