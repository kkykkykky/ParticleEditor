using System;
using System.Collections.Generic;
using UnityEngine;

namespace RSkoi_ParticleEditor {
    partial class ParticleEditor {
        private static void SetEmissionProperty(ParticleSystem.EmissionModule emis, EmissionProperty emp) {
            emis.enabled = emp.Enabled;
            var rot = emis.rateOverTime;
            rot.constant = emp.RateOTime;
            emis.rateOverTime = rot;
            var rod = emis.rateOverDistance;
            rod.constant = emp.RateODist;
            emis.rateOverDistance = rod;
            List<ParticleSystem.Burst> bursts = new List<ParticleSystem.Burst>();
            foreach (Burst b in emp.Bursts) {
                bursts.Add(new ParticleSystem.Burst(b.Time, b.Min, b.Max, b.Cycles, b.Interval));
            }
            emis.SetBursts(bursts.ToArray());
        }
    }
}