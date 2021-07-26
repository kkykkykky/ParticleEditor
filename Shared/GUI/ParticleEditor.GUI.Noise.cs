using System;
using UnityEngine;

namespace RSkoi_ParticleEditor {
	partial class ParticleEditor {
		private static bool noiseStrengthTwoConst = false;
		private static float maxStrength = 20.0F;
		private static float maxFrequency = 20.0F;
		private static float maxScroll = 5.0F;

		private void SetupToolbarNoise(GUIStyle tgStyle) {
			var noise = selectedPS.noise;

			// module enabled
			noise.enabled = GUI.Toggle(new Rect(10, 20, 120, 20), noise.enabled, "Module enabled", tgStyle);

			// quality
			GUI.Label(new Rect(155, 20, 100, 20), "Quality:");
			if (GUI.Button(new Rect(205, 20, 90, 20), getNoiseQualityToText(noise.quality))) {
				switch (noise.quality) {
					case ParticleSystemNoiseQuality.Low:
						noise.quality = ParticleSystemNoiseQuality.Medium;
						break;
					case ParticleSystemNoiseQuality.Medium:
						noise.quality = ParticleSystemNoiseQuality.High;
						break;
					default:
						//case ParticleSystemNoiseQuality.High:
						noise.quality = ParticleSystemNoiseQuality.Low;
						break;
				}
			}

			// strength
			GUI.Label(new Rect(10, 40, 100, 20), "Strength:");

				// seperate axis
			noise.separateAxes = GUI.Toggle(new Rect(10, 60, 100, 20), noise.separateAxes, "Separate axes");

			var strength = noise.strength;
			float noiseParse;
			noiseStrengthTwoConst = GUI.Toggle(new Rect(225, 60, 150, 50), (strength.mode == ParticleSystemCurveMode.TwoConstants) ? true : false, "random\nbetween\n2 const.");
			strength.mode = noiseStrengthTwoConst ? ParticleSystemCurveMode.TwoConstants : ((strength.mode == ParticleSystemCurveMode.TwoConstants) ? ParticleSystemCurveMode.Constant : strength.mode);
			if (noise.separateAxes) {
				if (noiseStrengthTwoConst) {
					float.TryParse(GUI.TextField(new Rect(10, 85, 50, 20), strength.constantMin.ToString()), out noiseParse);
					strength.constantMin = noiseParse;
					float.TryParse(GUI.TextField(new Rect(10, 110, 50, 20), strength.constantMax.ToString()), out noiseParse);
					strength.constantMax = noiseParse;

					var sy = noise.strengthY;
					float.TryParse(GUI.TextField(new Rect(70, 85, 50, 20), sy.constantMin.ToString()), out noiseParse);
					sy.constantMin = noiseParse;
					float.TryParse(GUI.TextField(new Rect(70, 110, 50, 20), sy.constantMax.ToString()), out noiseParse);
					sy.constantMax = noiseParse;
					noise.strengthY = sy;

					var sz = noise.strengthZ;
					float.TryParse(GUI.TextField(new Rect(130, 85, 50, 20), sz.constantMin.ToString()), out noiseParse);
					sz.constantMin = noiseParse;
					float.TryParse(GUI.TextField(new Rect(130, 110, 50, 20), sz.constantMax.ToString()), out noiseParse);
					sz.constantMax = noiseParse;
					noise.strengthZ = sz;
				} else {
					float.TryParse(GUI.TextField(new Rect(10, 85, 50, 20), strength.constant.ToString()), out noiseParse);
					strength.constant = noiseParse;

					var sy = noise.strengthY;
					float.TryParse(GUI.TextField(new Rect(70, 85, 50, 20), sy.constant.ToString()), out noiseParse);
					sy.constant = noiseParse;
					noise.strengthY = sy;

					var sz = noise.strengthZ;
					float.TryParse(GUI.TextField(new Rect(130, 85, 50, 20), sz.constant.ToString()), out noiseParse);
					sz.constant = noiseParse;
					noise.strengthZ = sz;
				}
			} else {
				if (noiseStrengthTwoConst) {
					strength.constantMin = GUI.HorizontalSlider(new Rect(10, 80, 150, 20), strength.constantMin, 0.0F, maxStrength);
					float.TryParse(GUI.TextField(new Rect(170, 70, 50, 20), strength.constantMin.ToString()), out noiseParse);
					strength.constantMin = noiseParse;

					strength.constantMax = GUI.HorizontalSlider(new Rect(10, 110, 150, 20), strength.constantMax, 0.0F, maxStrength);
					float.TryParse(GUI.TextField(new Rect(170, 100, 50, 20), strength.constantMax.ToString()), out noiseParse);
					strength.constantMax = noiseParse;
				} else {
					strength.constant = GUI.HorizontalSlider(new Rect(10, 80, 150, 20), strength.constant, 0.0F, maxStrength);
					float.TryParse(GUI.TextField(new Rect(170, 70, 50, 20), strength.constant.ToString()), out noiseParse);
					strength.constant = noiseParse;
				}
			}
			noise.strength = strength;

			// frequency
			GUI.Label(new Rect(10, 140, 100, 20), "Frequency:");

			noise.frequency = GUI.HorizontalSlider(new Rect(10, 160, 150, 20), noise.frequency, 0.0F, maxFrequency);
			float.TryParse(GUI.TextField(new Rect(170, 150, 50, 20), noise.frequency.ToString()), out noiseParse);
			noise.frequency = noiseParse;

			// damping
			noise.damping = GUI.Toggle(new Rect(225, 150, 120, 20), noise.damping, "Damping");

			// scroll speed
			GUI.Label(new Rect(10, 185, 100, 20), "Scroll speed:");
			var scroll = noise.scrollSpeed;
			scroll.constant = GUI.HorizontalSlider(new Rect(10, 205, 150, 20), scroll.constant, -maxScroll, maxScroll);
			float.TryParse(GUI.TextField(new Rect(170, 195, 50, 20), scroll.constant.ToString()), out noiseParse);
			scroll.constant = noiseParse;
			noise.scrollSpeed = scroll;

			// octaves
			GUI.Label(new Rect(10, 230, 100, 20), "Octaves:");
			noise.octaveCount = (int)GUI.HorizontalSlider(new Rect(10, 250, 150, 20), noise.octaveCount, 1, 4);
			float.TryParse(GUI.TextField(new Rect(170, 240, 50, 20), noise.octaveCount.ToString()), out noiseParse);
			noise.octaveCount = (int)noiseParse;

			if (noise.octaveCount == 1) {
				GUI.enabled = false;
			}
			// octave multiplier
			GUI.Label(new Rect(10, 280, 150, 20), "Octave multiplier:");
			noise.octaveMultiplier = GUI.HorizontalSlider(new Rect(10, 300, 150, 20), noise.octaveMultiplier, 0, 1);
			float.TryParse(GUI.TextField(new Rect(170, 290, 50, 20), noise.octaveMultiplier.ToString()), out noiseParse);
			noise.octaveMultiplier = noiseParse;

			// octave scale
			GUI.Label(new Rect(10, 330, 100, 20), "Octave scale:");
			noise.octaveScale = GUI.HorizontalSlider(new Rect(10, 350, 150, 20), noise.octaveScale, 1, 4);
			float.TryParse(GUI.TextField(new Rect(170, 340, 50, 20), noise.octaveScale.ToString()), out noiseParse);
			noise.octaveScale = noiseParse;

			GUI.enabled = true;
		}

		private static string getNoiseQualityToText(ParticleSystemNoiseQuality q) {
			switch (q) {
				case ParticleSystemNoiseQuality.Low:
					return "Low (1D)";
				case ParticleSystemNoiseQuality.Medium:
					return "Medium (2D)";
				default:
					//case ParticleSystemNoiseQuality.High:
					return "High (3D)";
			}
		}
	}
}