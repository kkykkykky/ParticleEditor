using System;
using UnityEngine;

namespace RSkoi_ParticleEditor {
	partial class ParticleEditor {
		private static bool forceOverLifetimeTwoConst = false;
		private static float maxLifetimeVelLimit = 20.0F;
		private static float maxLifetimeRotation = 6.2831853072F;
		private static float maxLifetimeSize = 10.0F;
		private static bool sizeOverLifetimeTwoConst = false;

		private void SetupToolbarLifetime(GUIStyle tgStyle) {
			// velocity over lifetime
			GUI.Box(new Rect(5, 20, 285, 80), "Velocity:");

			var vol = selectedPS.velocityOverLifetime;
				// module enabled
			vol.enabled = GUI.Toggle(new Rect(10, 30, 120, 20), vol.enabled, "Enabled", tgStyle);

				// XYZ
			GUI.Label(new Rect(10, 50, 50, 20), "XYZ:");

			float volParse;
			float.TryParse(GUI.TextField(new Rect(10, 70, 50, 20), vol.x.constant.ToString()), out volParse);
			var lX = vol.x;
			lX.constant = volParse;
			vol.x = lX;

			float.TryParse(GUI.TextField(new Rect(70, 70, 50, 20), vol.y.constant.ToString()), out volParse);
			var lY = vol.y;
			lY.constant = volParse;
			vol.y = lY;

			float.TryParse(GUI.TextField(new Rect(130, 70, 50, 20), vol.z.constant.ToString()), out volParse);
			var lZ = vol.z;
			lZ.constant = volParse;
			vol.z = lZ;

				// space
			GUI.Label(new Rect(230, 50, 100, 20), "Space:");
			if (GUI.Button(new Rect(230, 70, 50, 20), (vol.space == ParticleSystemSimulationSpace.Local) ? "Local" : "World")) {
				switch (vol.space) {
					case ParticleSystemSimulationSpace.Local:
						vol.space = ParticleSystemSimulationSpace.World;
						break;
					default:
						//case ParticleSystemSimulationSpace.World:
						vol.space = ParticleSystemSimulationSpace.Local;
						break;
				}
			}

			// limit velocity over lifetime
			GUI.Box(new Rect(5, 110, 285, 160), "Limit velocity:");

			var lvol = selectedPS.limitVelocityOverLifetime;
				// module enabled
			lvol.enabled = GUI.Toggle(new Rect(10, 120, 120, 20), lvol.enabled, "Enabled", tgStyle);

				// separate axes
			lvol.separateAxes = GUI.Toggle(new Rect(75, 140, 100, 20), lvol.separateAxes, "Separate axes");

			var limit = lvol.limit;
			forceOverLifetimeTwoConst = GUI.Toggle(new Rect(225, 140, 70, 50), (limit.mode == ParticleSystemCurveMode.TwoConstants), "random\nbetween\n2 const.");
			limit.mode = forceOverLifetimeTwoConst ? ParticleSystemCurveMode.TwoConstants : ((limit.mode == ParticleSystemCurveMode.TwoConstants) ? ParticleSystemCurveMode.Constant : limit.mode);
			if (lvol.separateAxes) {
				GUI.Label(new Rect(10, 140, 50, 20), "XYZ:");
				if (forceOverLifetimeTwoConst) {
					float.TryParse(GUI.TextField(new Rect(10, 160, 50, 20), lvol.limitX.constantMin.ToString()), out volParse);
					lX = lvol.limitX;
					lX.constantMin = volParse;
					float.TryParse(GUI.TextField(new Rect(10, 185, 50, 20), lvol.limitX.constantMax.ToString()), out volParse);
					lX.constantMax = volParse;
					lvol.limitX = lX;

					float.TryParse(GUI.TextField(new Rect(70, 160, 50, 20), lvol.limitY.constantMin.ToString()), out volParse);
					lY = lvol.limitY;
					lY.constantMin = volParse;
					float.TryParse(GUI.TextField(new Rect(70, 185, 50, 20), lvol.limitY.constantMax.ToString()), out volParse);
					lY.constantMax = volParse;
					lvol.limitY = lY;

					float.TryParse(GUI.TextField(new Rect(130, 160, 50, 20), lvol.limitZ.constantMin.ToString()), out volParse);
					lZ = lvol.limitZ;
					lZ.constantMin = volParse;
					float.TryParse(GUI.TextField(new Rect(130, 185, 50, 20), lvol.limitZ.constantMax.ToString()), out volParse);
					lZ.constantMax = volParse;
					lvol.limitZ = lZ;
				} else {
					float.TryParse(GUI.TextField(new Rect(10, 160, 50, 20), lvol.limitX.constant.ToString()), out volParse);
					lX = lvol.limitX;
					lX.constant = volParse;
					lvol.limitX = lX;

					float.TryParse(GUI.TextField(new Rect(70, 160, 50, 20), lvol.limitY.constant.ToString()), out volParse);
					lY = lvol.limitY;
					lY.constant = volParse;
					lvol.limitY = lY;

					float.TryParse(GUI.TextField(new Rect(130, 160, 50, 20), lvol.limitZ.constant.ToString()), out volParse);
					lZ = lvol.limitZ;
					lZ.constant = volParse;
					lvol.limitZ = lZ;
				}
			} else {
				GUI.Label(new Rect(10, 140, 70, 20), "Constants:");
				if (forceOverLifetimeTwoConst) {
					limit.constantMin = GUI.HorizontalSlider(new Rect(10, 170, 150, 20), limit.constantMin, 0.0F, maxLifetimeVelLimit);
					float.TryParse(GUI.TextField(new Rect(170, 160, 50, 20), limit.constantMin.ToString()), out volParse);
					limit.constantMin = volParse;

					limit.constantMax = GUI.HorizontalSlider(new Rect(10, 195, 150, 20), limit.constantMax, 0.0F, maxLifetimeVelLimit);
					float.TryParse(GUI.TextField(new Rect(170, 185, 50, 20), limit.constantMax.ToString()), out volParse);
					limit.constantMax = volParse;
				} else {
					limit.constant = GUI.HorizontalSlider(new Rect(10, 170, 150, 20), limit.constant, 0.0F, maxLifetimeVelLimit);
					float.TryParse(GUI.TextField(new Rect(170, 160, 50, 20), limit.constant.ToString()), out volParse);
					limit.constant = volParse;
				}
			}
			lvol.limit = limit;

				// space
			GUI.Label(new Rect(230, 220, 100, 20), "Space:");
			if (GUI.Button(new Rect(230, 240, 50, 20), (lvol.space == ParticleSystemSimulationSpace.Local) ? "Local" : "World")) {
				switch (lvol.space) {
					case ParticleSystemSimulationSpace.Local:
						lvol.space = ParticleSystemSimulationSpace.World;
						break;
					default:
						//case ParticleSystemSimulationSpace.World:
						lvol.space = ParticleSystemSimulationSpace.Local;
						break;
				}
			}

				// dampen
			GUI.Label(new Rect(10, 220, 70, 20), "Dampen:");

			lvol.dampen = GUI.HorizontalSlider(new Rect(10, 240, 150, 20), lvol.dampen, 0.0F, 1.0F);
			float.TryParse(GUI.TextField(new Rect(170, 230, 50, 20), lvol.dampen.ToString()), out volParse);
			lvol.dampen = volParse;

			// force over lifetime
			GUI.Box(new Rect(5, 280, 285, 80), "Force:");

			var fol = selectedPS.forceOverLifetime;
				// module enabled
			fol.enabled = GUI.Toggle(new Rect(10, 290, 120, 20), fol.enabled, "Enabled", tgStyle);

				// XYZ
			GUI.Label(new Rect(10, 310, 50, 20), "XYZ:");

			float.TryParse(GUI.TextField(new Rect(10, 330, 50, 20), fol.x.constant.ToString()), out volParse);
			lX = fol.x;
			lX.constant = volParse;
			fol.x = lX;

			float.TryParse(GUI.TextField(new Rect(70, 330, 50, 20), fol.y.constant.ToString()), out volParse);
			lY = fol.y;
			lY.constant = volParse;
			fol.y = lY;

			float.TryParse(GUI.TextField(new Rect(130, 330, 50, 20), fol.z.constant.ToString()), out volParse);
			lZ = fol.z;
			lZ.constant = volParse;
			fol.z = lZ;

				// space
			GUI.Label(new Rect(230, 310, 100, 20), "Space:");
			if (GUI.Button(new Rect(230, 330, 50, 20), (fol.space == ParticleSystemSimulationSpace.Local) ? "Local" : "World")) {
				switch (fol.space) {
					case ParticleSystemSimulationSpace.Local:
						fol.space = ParticleSystemSimulationSpace.World;
						break;
					default:
						//case ParticleSystemSimulationSpace.World:
						fol.space = ParticleSystemSimulationSpace.Local;
						break;
				}
			}

			// color over lifetime
			GUI.Box(new Rect(5, 370, 285, 140), "Color:");

			var col = selectedPS.colorOverLifetime;
				// module enabled
			col.enabled = GUI.Toggle(new Rect(10, 380, 120, 20), col.enabled, "Enabled", tgStyle);

			var colr = col.color;
			if (GUI.Button(new Rect(210, 400, 80, 20), getStartColorModeText(colr.mode))) {
				switch (colr.mode) {
					case ParticleSystemGradientMode.Gradient:
						colr.mode = ParticleSystemGradientMode.TwoGradients;
						break;
					case ParticleSystemGradientMode.TwoGradients:
						colr.mode = ParticleSystemGradientMode.Gradient;
						break;
				}
			}

				// gradient
			Color sColor;
			GradientAlphaKey transp;
			float colParse;
			switch (colr.mode) {
				case ParticleSystemGradientMode.Gradient:
					if (colr.gradient != null) {
						sColor = colr.gradient.colorKeys[0].color;
						transp = colr.gradient.alphaKeys[0];
					} else {
						sColor = new Color();
						transp = new GradientAlphaKey();
					}

					float.TryParse(GUI.TextField(new Rect(10, 400, 40, 20), sColor.r.ToString()), out colParse);
					sColor.r = colParse;

					float.TryParse(GUI.TextField(new Rect(60, 400, 40, 20), sColor.g.ToString()), out colParse);
					sColor.g = colParse;

					float.TryParse(GUI.TextField(new Rect(110, 400, 40, 20), sColor.b.ToString()), out colParse);
					sColor.b = colParse;

					float.TryParse(GUI.TextField(new Rect(160, 400, 40, 20), transp.alpha.ToString()), out colParse);
					transp.alpha = colParse;
					transp.time = 0F;

					GradientColorKey[] colors = new GradientColorKey[2];
					colors[0] = new GradientColorKey(sColor, 0F);

					GradientAlphaKey[] transparents = new GradientAlphaKey[2];
					transparents[0] = transp;

					if (colr.gradient != null) {
						sColor = colr.gradient.colorKeys[1].color;
						transp = colr.gradient.alphaKeys[1];
					} else {
						sColor = new Color();
						transp = new GradientAlphaKey();
					}

					float.TryParse(GUI.TextField(new Rect(10, 425, 40, 20), sColor.r.ToString()), out colParse);
					sColor.r = colParse;

					float.TryParse(GUI.TextField(new Rect(60, 425, 40, 20), sColor.g.ToString()), out colParse);
					sColor.g = colParse;

					float.TryParse(GUI.TextField(new Rect(110, 425, 40, 20), sColor.b.ToString()), out colParse);
					sColor.b = colParse;

					float.TryParse(GUI.TextField(new Rect(160, 425, 40, 20), transp.alpha.ToString()), out colParse);
					transp.alpha = colParse;
					transp.time = 1F;

					colors[1] = new GradientColorKey(sColor, 1F);

					transparents[1] = transp;

					Gradient g = new Gradient {
						colorKeys = colors,
						alphaKeys = transparents
					};
					colr.gradient = g;
					break;
				case ParticleSystemGradientMode.TwoGradients:
					// first gradient
					if (colr.gradientMin != null) {
						sColor = colr.gradientMin.colorKeys[0].color;
						transp = colr.gradientMin.alphaKeys[0];
					} else {
						sColor = new Color();
						transp = new GradientAlphaKey();
					}

					float.TryParse(GUI.TextField(new Rect(10, 400, 40, 20), sColor.r.ToString()), out colParse);
					sColor.r = colParse;

					float.TryParse(GUI.TextField(new Rect(60, 400, 40, 20), sColor.g.ToString()), out colParse);
					sColor.g = colParse;

					float.TryParse(GUI.TextField(new Rect(110, 400, 40, 20), sColor.b.ToString()), out colParse);
					sColor.b = colParse;

					float.TryParse(GUI.TextField(new Rect(160, 400, 40, 20), transp.alpha.ToString()), out colParse);
					transp.alpha = colParse;
					transp.time = 0F;

					GradientColorKey[] colors2Grad = new GradientColorKey[2];
					colors2Grad[0] = new GradientColorKey(sColor, 0F);

					GradientAlphaKey[] transparents2Grad = new GradientAlphaKey[2];
					transparents2Grad[0] = transp;

					if (colr.gradientMin != null) {
						sColor = colr.gradientMin.colorKeys[1].color;
						transp = colr.gradientMin.alphaKeys[1];
					} else {
						sColor = new Color();
						transp = new GradientAlphaKey();
					}

					float.TryParse(GUI.TextField(new Rect(10, 425, 40, 20), sColor.r.ToString()), out colParse);
					sColor.r = colParse;

					float.TryParse(GUI.TextField(new Rect(60, 425, 40, 20), sColor.g.ToString()), out colParse);
					sColor.g = colParse;

					float.TryParse(GUI.TextField(new Rect(110, 425, 40, 20), sColor.b.ToString()), out colParse);
					sColor.b = colParse;

					float.TryParse(GUI.TextField(new Rect(160, 425, 40, 20), transp.alpha.ToString()), out colParse);
					transp.alpha = colParse;
					transp.time = 1F;

					colors2Grad[1] = new GradientColorKey(sColor, 1F);

					transparents2Grad[1] = transp;

					Gradient g2Grad = new Gradient {
						colorKeys = colors2Grad,
						alphaKeys = transparents2Grad
					};
					colr.gradientMin = g2Grad;

					// second gradient
					if (colr.gradientMax != null) {
						sColor = colr.gradientMax.colorKeys[0].color;
						transp = colr.gradientMax.alphaKeys[0];
					} else {
						sColor = new Color();
						transp = new GradientAlphaKey();
					}

					float.TryParse(GUI.TextField(new Rect(10, 455, 40, 20), sColor.r.ToString()), out colParse);
					sColor.r = colParse;

					float.TryParse(GUI.TextField(new Rect(60, 455, 40, 20), sColor.g.ToString()), out colParse);
					sColor.g = colParse;

					float.TryParse(GUI.TextField(new Rect(110, 455, 40, 20), sColor.b.ToString()), out colParse);
					sColor.b = colParse;

					float.TryParse(GUI.TextField(new Rect(160, 455, 40, 20), transp.alpha.ToString()), out colParse);
					transp.alpha = colParse;
					transp.time = 0F;

					colors2Grad = new GradientColorKey[2];
					colors2Grad[0] = new GradientColorKey(sColor, 0F);

					transparents2Grad = new GradientAlphaKey[2];
					transparents2Grad[0] = transp;

					if (colr.gradientMax != null) {
						sColor = colr.gradientMax.colorKeys[1].color;
						transp = colr.gradientMax.alphaKeys[1];
					} else {
						sColor = new Color();
						transp = new GradientAlphaKey();
					}

					float.TryParse(GUI.TextField(new Rect(10, 480, 40, 20), sColor.r.ToString()), out colParse);
					sColor.r = colParse;

					float.TryParse(GUI.TextField(new Rect(60, 480, 40, 20), sColor.g.ToString()), out colParse);
					sColor.g = colParse;

					float.TryParse(GUI.TextField(new Rect(110, 480, 40, 20), sColor.b.ToString()), out colParse);
					sColor.b = colParse;

					float.TryParse(GUI.TextField(new Rect(160, 480, 40, 20), transp.alpha.ToString()), out colParse);
					transp.alpha = colParse;
					transp.time = 1F;

					colors2Grad[1] = new GradientColorKey(sColor, 1F);

					transparents2Grad[1] = transp;

					g2Grad = new Gradient {
						colorKeys = colors2Grad,
						alphaKeys = transparents2Grad
					};
					colr.gradientMax = g2Grad;
					break;
			}
			col.color = colr;

			// rotation over lifetime
			GUI.Box(new Rect(5, 520, 285, 90), "Rotation:");

			var rol = selectedPS.rotationOverLifetime;
				// module enabled
			rol.enabled = GUI.Toggle(new Rect(10, 530, 120, 20), rol.enabled, "Enabled", tgStyle);

				// separate axes
			rol.separateAxes = GUI.Toggle(new Rect(220, 560, 70, 40), rol.separateAxes, "Separate\naxes");

			if (rol.separateAxes) {
				GUI.Label(new Rect(10, 560, 50, 20), "XYZ:");
				float.TryParse(GUI.TextField(new Rect(10, 580, 50, 20), ((float)(rol.x.constant * 180 / Math.PI)).ToString()), out volParse);
				lX = rol.x;
				lX.constant = (float)(volParse * Math.PI / 180);
				rol.x = lX;

				float.TryParse(GUI.TextField(new Rect(70, 580, 50, 20), ((float)(rol.y.constant * 180 / Math.PI)).ToString()), out volParse);
				lY = rol.y;
				lY.constant = (float)(volParse * Math.PI / 180);
				rol.y = lY;

				float.TryParse(GUI.TextField(new Rect(130, 580, 50, 20), ((float)(rol.z.constant * 180 / Math.PI)).ToString()), out volParse);
				lZ = rol.z;
				lZ.constant = (float)(volParse * Math.PI / 180);
				rol.z = lZ;
			} else {
				lZ = rol.z;
				GUI.Label(new Rect(10, 560, 70, 20), "Constant:");
				lZ.constant = GUI.HorizontalSlider(new Rect(10, 580, 150, 20), lZ.constant, -maxLifetimeRotation, maxLifetimeRotation);
				float.TryParse(GUI.TextField(new Rect(170, 570, 50, 20), ((float)(lZ.constant * 180 / Math.PI)).ToString()), out volParse);
				lZ.constant = (float)(volParse * Math.PI / 180);
				rol.z = lZ;
			}

			// size over lifetime
			GUI.Box(new Rect(5, 620, 285, 110), "Size:");

			var sol = selectedPS.sizeOverLifetime;
				// module enabled
			sol.enabled = GUI.Toggle(new Rect(10, 630, 120, 20), sol.enabled, "Enabled", tgStyle);

				// separate axes
			sol.separateAxes = GUI.Toggle(new Rect(70, 660, 100, 20), sol.separateAxes, "Separate axes");

			sizeOverLifetimeTwoConst = GUI.Toggle(new Rect(225, 660, 70, 50), (sol.size.mode == ParticleSystemCurveMode.TwoConstants), "random\nbetween\n2 const.");
			var solSize = sol.size;
			solSize.mode = sizeOverLifetimeTwoConst ? ParticleSystemCurveMode.TwoConstants : ((solSize.mode == ParticleSystemCurveMode.TwoConstants) ? ParticleSystemCurveMode.Curve : solSize.mode);
			if (sol.separateAxes) {
				GUI.Label(new Rect(10, 660, 50, 20), "XYZ:");
				if (sizeOverLifetimeTwoConst) {
					float.TryParse(GUI.TextField(new Rect(10, 680, 50, 20), solSize.constantMin.ToString()), out volParse);
					solSize.constantMin = volParse;
					float.TryParse(GUI.TextField(new Rect(10, 705, 50, 20), solSize.constantMax.ToString()), out volParse);
					solSize.constantMax = volParse;
					
					float.TryParse(GUI.TextField(new Rect(70, 680, 50, 20), sol.y.constantMin.ToString()), out volParse);
					lY = sol.y;
					lY.constantMin = volParse;
					float.TryParse(GUI.TextField(new Rect(70, 705, 50, 20), sol.y.constantMax.ToString()), out volParse);
					lY.constantMax = volParse;
					sol.y = lY;

					float.TryParse(GUI.TextField(new Rect(130, 680, 50, 20), sol.z.constantMin.ToString()), out volParse);
					lZ = sol.z;
					lZ.constantMin = volParse;
					float.TryParse(GUI.TextField(new Rect(130, 705, 50, 20), sol.z.constantMax.ToString()), out volParse);
					lZ.constantMax = volParse;
					sol.z = lZ;
				}
			} else {
				GUI.Label(new Rect(10, 660, 70, 20), "Constants:");
				if (sizeOverLifetimeTwoConst) {
					solSize.constantMin = GUI.HorizontalSlider(new Rect(10, 680, 150, 20), solSize.constantMin, 0.0F, maxLifetimeSize);
					float.TryParse(GUI.TextField(new Rect(170, 670, 50, 20), solSize.constantMin.ToString()), out volParse);
					solSize.constantMin = volParse;

					solSize.constantMax = GUI.HorizontalSlider(new Rect(10, 705, 150, 20), solSize.constantMax, 0.0F, maxLifetimeSize);
					float.TryParse(GUI.TextField(new Rect(170, 695, 50, 20), solSize.constantMax.ToString()), out volParse);
					solSize.constantMax = volParse;
				}
			}
			sol.size = solSize;
		}
    }
}