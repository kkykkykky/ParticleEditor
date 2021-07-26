using System;
using UnityEngine;

namespace RSkoi_ParticleEditor {
	partial class ParticleEditor {
		private static float maxSpeedColorRange = 20.0F;
		private static float maxSpeedRotation = 6.2831853072F;

		private void SetupToolbarSpeed(GUIStyle tgStyle) {
			// color by speed
			GUI.Box(new Rect(5, 20, 285, 220), "Color:");

			var cbs = selectedPS.colorBySpeed;
				// module enabled
			cbs.enabled = GUI.Toggle(new Rect(10, 30, 120, 20), cbs.enabled, "Enabled", tgStyle);

			var cbsColor = cbs.color;
			if (GUI.Button(new Rect(210, 60, 80, 20), getStartColorModeText(cbsColor.mode))) {
				switch (cbsColor.mode) {
					case ParticleSystemGradientMode.Gradient:
						cbsColor.mode = ParticleSystemGradientMode.TwoGradients;
						break;
					case ParticleSystemGradientMode.TwoGradients:
						cbsColor.mode = ParticleSystemGradientMode.Gradient;
						break;
				}
			}

				// gradient
			Color newColor;
			GradientAlphaKey newTransp;
			float cbsParse;
			switch (cbsColor.mode) {
				case ParticleSystemGradientMode.Gradient:
					if (cbsColor.gradient != null) {
						newColor = cbsColor.gradient.colorKeys[0].color;
						newTransp = cbsColor.gradient.alphaKeys[0];
					} else {
						newColor = new Color();
						newTransp = new GradientAlphaKey();
					}

					float.TryParse(GUI.TextField(new Rect(10, 60, 40, 20), newColor.r.ToString()), out cbsParse);
					newColor.r = cbsParse;

					float.TryParse(GUI.TextField(new Rect(60, 60, 40, 20), newColor.g.ToString()), out cbsParse);
					newColor.g = cbsParse;

					float.TryParse(GUI.TextField(new Rect(110, 60, 40, 20), newColor.b.ToString()), out cbsParse);
					newColor.b = cbsParse;

					float.TryParse(GUI.TextField(new Rect(160, 60, 40, 20), newTransp.alpha.ToString()), out cbsParse);
					newTransp.alpha = cbsParse;
					newTransp.time = 0F;

					GradientColorKey[] colors = new GradientColorKey[2];
					colors[0] = new GradientColorKey(newColor, 0F);

					GradientAlphaKey[] transparents = new GradientAlphaKey[2];
					transparents[0] = newTransp;

					if (cbsColor.gradient != null) {
						newColor = cbsColor.gradient.colorKeys[1].color;
						newTransp = cbsColor.gradient.alphaKeys[1];
					} else {
						newColor = new Color();
						newTransp = new GradientAlphaKey();
					}

					float.TryParse(GUI.TextField(new Rect(10, 85, 40, 20), newColor.r.ToString()), out cbsParse);
					newColor.r = cbsParse;

					float.TryParse(GUI.TextField(new Rect(60, 85, 40, 20), newColor.g.ToString()), out cbsParse);
					newColor.g = cbsParse;

					float.TryParse(GUI.TextField(new Rect(110, 85, 40, 20), newColor.b.ToString()), out cbsParse);
					newColor.b = cbsParse;

					float.TryParse(GUI.TextField(new Rect(160, 85, 40, 20), newTransp.alpha.ToString()), out cbsParse);
					newTransp.alpha = cbsParse;
					newTransp.time = 1F;

					colors[1] = new GradientColorKey(newColor, 1F);

					transparents[1] = newTransp;

					Gradient g = new Gradient {
						colorKeys = colors,
						alphaKeys = transparents
					};
					cbsColor.gradient = g;
					break;
				case ParticleSystemGradientMode.TwoGradients:
					// first gradient
					if (cbsColor.gradientMin != null) {
						newColor = cbsColor.gradientMin.colorKeys[0].color;
						newTransp = cbsColor.gradientMin.alphaKeys[0];
					} else {
						newColor = new Color();
						newTransp = new GradientAlphaKey();
					}

					float.TryParse(GUI.TextField(new Rect(10, 60, 40, 20), newColor.r.ToString()), out cbsParse);
					newColor.r = cbsParse;

					float.TryParse(GUI.TextField(new Rect(60, 60, 40, 20), newColor.g.ToString()), out cbsParse);
					newColor.g = cbsParse;

					float.TryParse(GUI.TextField(new Rect(110, 60, 40, 20), newColor.b.ToString()), out cbsParse);
					newColor.b = cbsParse;

					float.TryParse(GUI.TextField(new Rect(160, 60, 40, 20), newTransp.alpha.ToString()), out cbsParse);
					newTransp.alpha = cbsParse;
					newTransp.time = 0F;

					GradientColorKey[] colors2Grad = new GradientColorKey[2];
					colors2Grad[0] = new GradientColorKey(newColor, 0F);

					GradientAlphaKey[] transparents2Grad = new GradientAlphaKey[2];
					transparents2Grad[0] = newTransp;

					if (cbsColor.gradientMin != null) {
						newColor = cbsColor.gradientMin.colorKeys[1].color;
						newTransp = cbsColor.gradientMin.alphaKeys[1];
					} else {
						newColor = new Color();
						newTransp = new GradientAlphaKey();
					}

					float.TryParse(GUI.TextField(new Rect(10, 85, 40, 20), newColor.r.ToString()), out cbsParse);
					newColor.r = cbsParse;

					float.TryParse(GUI.TextField(new Rect(60, 85, 40, 20), newColor.g.ToString()), out cbsParse);
					newColor.g = cbsParse;

					float.TryParse(GUI.TextField(new Rect(110, 85, 40, 20), newColor.b.ToString()), out cbsParse);
					newColor.b = cbsParse;

					float.TryParse(GUI.TextField(new Rect(160, 85, 40, 20), newTransp.alpha.ToString()), out cbsParse);
					newTransp.alpha = cbsParse;
					newTransp.time = 1F;

					colors2Grad[1] = new GradientColorKey(newColor, 1F);

					transparents2Grad[1] = newTransp;

					Gradient g2Grad = new Gradient {
						colorKeys = colors2Grad,
						alphaKeys = transparents2Grad
					};
					cbsColor.gradientMin = g2Grad;

					// second gradient
					if (cbsColor.gradientMax != null) {
						newColor = cbsColor.gradientMax.colorKeys[0].color;
						newTransp = cbsColor.gradientMax.alphaKeys[0];
					} else {
						newColor = new Color();
						newTransp = new GradientAlphaKey();
					}

					float.TryParse(GUI.TextField(new Rect(10, 115, 40, 20), newColor.r.ToString()), out cbsParse);
					newColor.r = cbsParse;

					float.TryParse(GUI.TextField(new Rect(60, 115, 40, 20), newColor.g.ToString()), out cbsParse);
					newColor.g = cbsParse;

					float.TryParse(GUI.TextField(new Rect(110, 115, 40, 20), newColor.b.ToString()), out cbsParse);
					newColor.b = cbsParse;

					float.TryParse(GUI.TextField(new Rect(160, 115, 40, 20), newTransp.alpha.ToString()), out cbsParse);
					newTransp.alpha = cbsParse;
					newTransp.time = 0F;

					colors2Grad = new GradientColorKey[2];
					colors2Grad[0] = new GradientColorKey(newColor, 0F);

					transparents2Grad = new GradientAlphaKey[2];
					transparents2Grad[0] = newTransp;

					if (cbsColor.gradientMax != null) {
						newColor = cbsColor.gradientMax.colorKeys[1].color;
						newTransp = cbsColor.gradientMax.alphaKeys[1];
					} else {
						newColor = new Color();
						newTransp = new GradientAlphaKey();
					}

					float.TryParse(GUI.TextField(new Rect(10, 140, 40, 20), newColor.r.ToString()), out cbsParse);
					newColor.r = cbsParse;

					float.TryParse(GUI.TextField(new Rect(60, 140, 40, 20), newColor.g.ToString()), out cbsParse);
					newColor.g = cbsParse;

					float.TryParse(GUI.TextField(new Rect(110, 140, 40, 20), newColor.b.ToString()), out cbsParse);
					newColor.b = cbsParse;

					float.TryParse(GUI.TextField(new Rect(160, 140, 40, 20), newTransp.alpha.ToString()), out cbsParse);
					newTransp.alpha = cbsParse;
					newTransp.time = 1F;

					colors2Grad[1] = new GradientColorKey(newColor, 1F);

					transparents2Grad[1] = newTransp;

					g2Grad = new Gradient {
						colorKeys = colors2Grad,
						alphaKeys = transparents2Grad
					};
					cbsColor.gradientMax = g2Grad;
					break;
			}
			cbs.color = cbsColor;

				// speed range
			GUI.Label(new Rect(10, 170, 100, 20), "Speed range:");
			var range = cbs.range;
			float newX = range.x;
			newX = GUI.HorizontalSlider(new Rect(10, 190, 150, 20), newX, 0.0F, range.y - 0.01F);
			float.TryParse(GUI.TextField(new Rect(170, 180, 50, 20), newX.ToString()), out cbsParse);
			newX = cbsParse;

			float newY = range.y;
			newY = GUI.HorizontalSlider(new Rect(10, 220, 150, 20), newY, 0.0F, maxSpeedColorRange);
			float.TryParse(GUI.TextField(new Rect(170, 210, 50, 20), newY.ToString()), out cbsParse);
			newY = cbsParse;
			if (newX >= newY) {
				newX = newY - 0.01F;
			}
			range.x = newX;
			range.y = newY;
			cbs.range = range;

			// size by speed
			GUI.Box(new Rect(5, 250, 285, 110), "Size:");

			var sbp = selectedPS.sizeBySpeed;
				// module enabled
			sbp.enabled = GUI.Toggle(new Rect(10, 260, 120, 20), sbp.enabled, "Enabled", tgStyle);

				// separate axes
			sbp.separateAxes = GUI.Toggle(new Rect(70, 290, 100, 20), sbp.separateAxes, "Separate axes");

			sizeOverLifetimeTwoConst = GUI.Toggle(new Rect(225, 290, 70, 50), (sbp.size.mode == ParticleSystemCurveMode.TwoConstants), "random\nbetween\n2 const.");
			var sbpSize = sbp.size;
			sbpSize.mode = sizeOverLifetimeTwoConst ? ParticleSystemCurveMode.TwoConstants : ((sbpSize.mode == ParticleSystemCurveMode.TwoConstants) ? ParticleSystemCurveMode.Curve : sbpSize.mode);

			var lX = sbp.x;
			var lY = sbp.y;
			var lZ = sbp.z;

			if (sbp.separateAxes) {
				GUI.Label(new Rect(10, 290, 50, 20), "XYZ:");
				if (sizeOverLifetimeTwoConst) {
					float.TryParse(GUI.TextField(new Rect(10, 310, 50, 20), sbpSize.constantMin.ToString()), out cbsParse);
					sbpSize.constantMin = cbsParse;
					float.TryParse(GUI.TextField(new Rect(10, 335, 50, 20), sbpSize.constantMax.ToString()), out cbsParse);
					sbpSize.constantMax = cbsParse;

					float.TryParse(GUI.TextField(new Rect(70, 310, 50, 20), sbp.y.constantMin.ToString()), out cbsParse);
					lY = sbp.y;
					lY.constantMin = cbsParse;
					float.TryParse(GUI.TextField(new Rect(70, 335, 50, 20), sbp.y.constantMax.ToString()), out cbsParse);
					lY.constantMax = cbsParse;
					sbp.y = lY;

					float.TryParse(GUI.TextField(new Rect(130, 310, 50, 20), sbp.z.constantMin.ToString()), out cbsParse);
					lZ = sbp.z;
					lZ.constantMin = cbsParse;
					float.TryParse(GUI.TextField(new Rect(130, 335, 50, 20), sbp.z.constantMax.ToString()), out cbsParse);
					lZ.constantMax = cbsParse;
					sbp.z = lZ;
				}
			} else {
				GUI.Label(new Rect(10, 290, 70, 20), "Constants:");
				if (sizeOverLifetimeTwoConst) {
					sbpSize.constantMin = GUI.HorizontalSlider(new Rect(10, 320, 150, 20), sbpSize.constantMin, 0.0F, maxLifetimeSize);
					float.TryParse(GUI.TextField(new Rect(170, 310, 50, 20), sbpSize.constantMin.ToString()), out cbsParse);
					sbpSize.constantMin = cbsParse;

					sbpSize.constantMax = GUI.HorizontalSlider(new Rect(10, 345, 150, 20), sbpSize.constantMax, 0.0F, maxLifetimeSize);
					float.TryParse(GUI.TextField(new Rect(170, 335, 50, 20), sbpSize.constantMax.ToString()), out cbsParse);
					sbpSize.constantMax = cbsParse;
				}
			}
			sbp.size = sbpSize;

			// rotation by speed
			GUI.Box(new Rect(5, 370, 285, 90), "Rotation:");

			var rbs = selectedPS.rotationBySpeed;
				// module enabled
			rbs.enabled = GUI.Toggle(new Rect(10, 380, 120, 20), rbs.enabled, "Enabled", tgStyle);

				// separate axes
			rbs.separateAxes = GUI.Toggle(new Rect(220, 410, 70, 40), rbs.separateAxes, "Separate\naxes");

			if (rbs.separateAxes) {
				GUI.Label(new Rect(10, 410, 50, 20), "XYZ:");
				float.TryParse(GUI.TextField(new Rect(10, 430, 50, 20), ((float)(rbs.x.constant * 180 / Math.PI)).ToString()), out cbsParse);
				lX = rbs.x;
				lX.constant = (float)(cbsParse * Math.PI / 180);
				rbs.x = lX;

				float.TryParse(GUI.TextField(new Rect(70, 430, 50, 20), ((float)(rbs.y.constant * 180 / Math.PI)).ToString()), out cbsParse);
				lY = rbs.y;
				lY.constant = (float)(cbsParse * Math.PI / 180);
				rbs.y = lY;

				float.TryParse(GUI.TextField(new Rect(130, 430, 50, 20), ((float)(rbs.z.constant * 180 / Math.PI)).ToString()), out cbsParse);
				lZ = rbs.z;
				lZ.constant = (float)(cbsParse * Math.PI / 180);
				rbs.z = lZ;
			} else {
				lZ = rbs.z;
				GUI.Label(new Rect(10, 410, 70, 20), "Constant:");
				lZ.constant = GUI.HorizontalSlider(new Rect(10, 430, 150, 20), lZ.constant, -maxSpeedRotation, maxSpeedRotation);
				float.TryParse(GUI.TextField(new Rect(170, 420, 50, 20), ((float)(lZ.constant * 180 / Math.PI)).ToString()), out cbsParse);
				lZ.constant = (float)(cbsParse * Math.PI / 180);
				rbs.z = lZ;
			}
		}
	}
}