using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace RSkoi_ParticleEditor {
    partial class ParticleEditor {
		private static float maxRateOverTime = 30.0F;
		private static float maxRateOverDistance = 30.0F;

		private void SetupToolbarEmission(GUIStyle tgStyle, GUIStyle bStyle, GUIStyle lStyle) {
			var emis = selectedPS.emission;

			// module enabled
			emis.enabled = GUI.Toggle(new Rect(10, 20, 120, 20), emis.enabled, "Module enabled", tgStyle);

			// rate over time
			GUI.Label(new Rect(10, 40, 100, 20), "Rate over time:");
			var emisRot = emis.rateOverTime;
			emisRot.constant = GUI.HorizontalSlider(new Rect(10, 60, 150, 20), emisRot.constant, 0.0F, maxRateOverTime);
			emis.rateOverTime = emisRot;
			float emisRotParse;
			emisRot = emis.rateOverTime;
			float.TryParse(GUI.TextField(new Rect(170, 50, 50, 20), emisRot.constant.ToString()), out emisRotParse);
			emisRot.constant = emisRotParse;
			emis.rateOverTime = emisRot;

			// rate over distance
			GUI.Label(new Rect(10, 90, 120, 20), "Rate over distance:");
			var emisRod = emis.rateOverDistance;
			emisRod.constant = GUI.HorizontalSlider(new Rect(10, 110, 150, 20), emisRod.constant, 0.0F, maxRateOverDistance);
			emis.rateOverDistance = emisRod;
			emisRod = emis.rateOverDistance;
			float.TryParse(GUI.TextField(new Rect(170, 100, 50, 20), emisRod.constant.ToString()), out emisRotParse);
			emisRod.constant = emisRotParse;
			emis.rateOverDistance = emisRod;

			if (emisRod.constant != 0.0F) {
				GUI.Label(new Rect(10, 130, 250, 50), "Distance-based emission only works when using World Space Simulation Space.", lStyle);
			}

			// bursts
			GUI.Label(new Rect(10, 170, 250, 20), "Bursts (Time; Min; Max; Cycles; Interval):");

			ParticleSystem.Burst[] bursts = new ParticleSystem.Burst[emis.burstCount];
			emis.GetBursts(bursts);
			List<ParticleSystem.Burst> lBursts = bursts.ToList();

			// add new
			if (GUI.Button(new Rect(265, 170, 20, 20), "+")) {
				lBursts.Add(new ParticleSystem.Burst());
			}

			GUIStyle bsStyle = new GUIStyle(GUI.skin.button);
			var bnormal = bsStyle.normal;
			var bactive = bsStyle.active;
			var bhover = bsStyle.hover;
			bnormal.textColor = Color.red;
			bactive.textColor = Color.red;
			bhover.textColor = Color.red;

			for (int i = 0; i < emis.burstCount; i++) {
				GUI.Box(new Rect(10, 200 + i * 40, 280, 30), "");

				ParticleSystem.Burst curBurst = lBursts[i];

				float burstParse;
				// time
				float.TryParse(GUI.TextField(new Rect(15, 205 + i * 40, 45, 20), bursts[i].time.ToString()), out burstParse);
				curBurst.time = burstParse;

				// min
				short burstParseShort;
				short.TryParse(GUI.TextField(new Rect(65, 205 + i * 40, 45, 20), bursts[i].minCount.ToString()), out burstParseShort);
				curBurst.minCount = burstParseShort;

				// max
				short.TryParse(GUI.TextField(new Rect(115, 205 + i * 40, 45, 20), bursts[i].maxCount.ToString()), out burstParseShort);
				curBurst.maxCount = burstParseShort;

				// cycle
				int burstParseInt;
				int.TryParse(GUI.TextField(new Rect(165, 205 + i * 40, 45, 20), bursts[i].cycleCount.ToString()), out burstParseInt);
				curBurst.cycleCount = burstParseInt;

				// interval
				float.TryParse(GUI.TextField(new Rect(215, 205 + i * 40, 45, 20), bursts[i].repeatInterval.ToString()), out burstParse);
				curBurst.repeatInterval = burstParse;

				lBursts[i] = curBurst;
			}

			// disgusting, but this is the easiest way to go about this
			for (int i = 0; i < emis.burstCount; i++) {
				if (GUI.Button(new Rect(265, 205 + i * 40, 20, 20), "X", bsStyle)) {
					lBursts.RemoveAt(i);
				}
			}

			emis.SetBursts(lBursts.ToArray());
		}
    }
}