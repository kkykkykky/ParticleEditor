using System;
using UnityEngine;

namespace RSkoi_ParticleEditor {
    partial class ParticleEditor {
		private static float maxDuration = 20.0F;

		private static float maxStartDelay = 10.0F;
		private static bool delayStartTwoConst = false;

		private static float maxStartLifetime = 20.0F;
		private static bool lifetimeStartTwoConst = false;

		private static float maxStartSpeed = 20.0F;
		private static bool speedStartTwoConst = false;

		private static float maxStartRot = 6.2831853072F;

		private void SetupToolbarGeneral(ParticleSystem.MainModule main, GUIStyle redLabelStyle) {
			// duration
			GUI.Label(new Rect(10, 10, 60, 20), "Duration:");
			if (selectedPS.isPlaying) {
				GUI.Label(new Rect(10, 40, 300, 20), "duration can only be set on paused systems", redLabelStyle);
				GUI.HorizontalSlider(new Rect(10, 30, 150, 20), main.duration, 0.0F, maxDuration);
				GUI.TextField(new Rect(170, 20, 50, 20), main.duration.ToString());
			} else {
				main.duration = GUI.HorizontalSlider(new Rect(10, 30, 150, 20), main.duration, 0.0F, maxDuration);
				float durationParse;
				float.TryParse(GUI.TextField(new Rect(170, 20, 50, 20), main.duration.ToString()), out durationParse);
				main.duration = durationParse;
			}

			// looping
			main.loop = GUI.Toggle(new Rect(225, 10, 50, 20), main.loop, "Loop");
			// prewarm
			main.prewarm = GUI.Toggle(new Rect(225, 25, 70, 20), main.prewarm, "Prewarm");

			// start delay
			GUI.enabled = !main.prewarm;
			GUI.Label(new Rect(10, 60, 100, 20), "Start delay:");

			var delay = main.startDelay;
			float delayParse;
			delayStartTwoConst = GUI.Toggle(new Rect(225, 70, 150, 50), delayStartTwoConst, "random\nbetween\n2 const.");
			if (delayStartTwoConst) {
				delay.mode = ParticleSystemCurveMode.TwoConstants;
				delay.constantMin = GUI.HorizontalSlider(new Rect(10, 80, 150, 20), delay.constantMin, 0.0F, maxStartDelay);
				float.TryParse(GUI.TextField(new Rect(170, 70, 50, 20), delay.constantMin.ToString()), out delayParse);
				delay.constantMin = delayParse;

				delay.constantMax = GUI.HorizontalSlider(new Rect(10, 100, 150, 20), delay.constantMax, 0.0F, maxStartDelay);
				float.TryParse(GUI.TextField(new Rect(170, 90, 50, 20), delay.constantMax.ToString()), out delayParse);
				delay.constantMax = delayParse;
			} else {
				delay.mode = ParticleSystemCurveMode.Constant;
				delay.constant = GUI.HorizontalSlider(new Rect(10, 80, 150, 20), delay.constant, 0.0F, maxStartDelay);
				float.TryParse(GUI.TextField(new Rect(170, 70, 50, 20), delay.constant.ToString()), out delayParse);
				delay.constant = delayParse;
			}
			main.startDelay = delay;
			GUI.enabled = true;

			// start lifetime
			GUI.Label(new Rect(10, 110, 100, 20), "Start lifetime:");

			var life = main.startLifetime;
			float lifeParse;
			lifetimeStartTwoConst = GUI.Toggle(new Rect(225, 120, 150, 50), (life.mode == ParticleSystemCurveMode.TwoConstants), "random\nbetween\n2 const.");
			life.mode = lifetimeStartTwoConst ? ParticleSystemCurveMode.TwoConstants : ((life.mode == ParticleSystemCurveMode.TwoConstants) ? ParticleSystemCurveMode.Constant : life.mode);
			if (lifetimeStartTwoConst) {
				life.constantMin = GUI.HorizontalSlider(new Rect(10, 130, 150, 20), life.constantMin, 0.0F, maxStartLifetime);
				float.TryParse(GUI.TextField(new Rect(170, 120, 50, 20), life.constantMin.ToString()), out lifeParse);
				life.constantMin = lifeParse;

				life.constantMax = GUI.HorizontalSlider(new Rect(10, 150, 150, 20), life.constantMax, 0.0F, maxStartLifetime);
				float.TryParse(GUI.TextField(new Rect(170, 140, 50, 20), life.constantMax.ToString()), out lifeParse);
				life.constantMax = lifeParse;
			} else {
				life.constant = GUI.HorizontalSlider(new Rect(10, 130, 150, 20), life.constant, 0.0F, maxStartLifetime);
				float.TryParse(GUI.TextField(new Rect(170, 120, 50, 20), life.constant.ToString()), out lifeParse);
				life.constant = lifeParse;
			}
			main.startLifetime = life;

			// start speed
			GUI.Label(new Rect(10, 160, 100, 20), "Start speed:");

			var speed = main.startSpeed;
			float speedParse;
			speedStartTwoConst = GUI.Toggle(new Rect(225, 170, 150, 50), (speed.mode == ParticleSystemCurveMode.TwoConstants), "random\nbetween\n2 const.");
			speed.mode = speedStartTwoConst ? ParticleSystemCurveMode.TwoConstants : ((speed.mode == ParticleSystemCurveMode.TwoConstants) ? ParticleSystemCurveMode.Constant : speed.mode);
			if (speedStartTwoConst) {
				speed.constantMin = GUI.HorizontalSlider(new Rect(10, 180, 150, 20), speed.constantMin, 0.0F, maxStartSpeed);
				float.TryParse(GUI.TextField(new Rect(170, 170, 50, 20), speed.constantMin.ToString()), out speedParse);
				speed.constantMin = speedParse;

				speed.constantMax = GUI.HorizontalSlider(new Rect(10, 200, 150, 20), speed.constantMax, 0.0F, maxStartSpeed);
				float.TryParse(GUI.TextField(new Rect(170, 190, 50, 20), speed.constantMax.ToString()), out speedParse);
				speed.constantMax = speedParse;
			} else {
				speed.constant = GUI.HorizontalSlider(new Rect(10, 180, 150, 20), speed.constant, 0.0F, maxStartSpeed);
				float.TryParse(GUI.TextField(new Rect(170, 170, 50, 20), speed.constant.ToString()), out speedParse);
				speed.constant = speedParse;
			}
			main.startSpeed = speed;

			// start rotation
			GUI.Label(new Rect(10, 210, 100, 20), "Start rotation:");

			var rot = main.startRotation;
			rot.mode = ParticleSystemCurveMode.Constant;
			float rotParse;
			main.startRotation3D = GUI.Toggle(new Rect(225, 220, 150, 20), main.startRotation3D, "3D (XYZ)");
			if (main.startRotation3D) {
				float.TryParse(GUI.TextField(new Rect(10, 230, 50, 20), ((float)(main.startRotationX.constant * 180 / Math.PI)).ToString()), out rotParse);
				var rot3D = main.startRotationX;
				rot3D.constant = (float)(rotParse * Math.PI / 180);
				main.startRotationX = rot3D;

				float.TryParse(GUI.TextField(new Rect(70, 230, 50, 20), ((float)(main.startRotationY.constant * 180 / Math.PI)).ToString()), out rotParse);
				rot3D = main.startRotationY;
				rot3D.constant = (float)(rotParse * Math.PI / 180);
				main.startRotationY = rot3D;

				float.TryParse(GUI.TextField(new Rect(130, 230, 50, 20), ((float)(main.startRotationZ.constant * 180 / Math.PI)).ToString()), out rotParse);
				rot3D = main.startRotationZ;
				rot3D.constant = (float)(rotParse * Math.PI / 180);
				main.startRotationZ = rot3D;
			} else {
				rot.constant = GUI.HorizontalSlider(new Rect(10, 230, 150, 20), rot.constant, 0.0F, maxStartRot);
				float.TryParse(GUI.TextField(new Rect(170, 220, 50, 20), ((float)(rot.constant * 180 / Math.PI)).ToString()), out rotParse);
				rot.constant = (float)(rotParse * Math.PI / 180);
				main.startRotation = rot;
			}

			// randomize rotation
			GUI.Label(new Rect(10, 250, 160, 20), "Randomize rotation:");

			float rRotParse;
#if KK
			main.randomizeRotationDirection = GUI.HorizontalSlider(new Rect(10, 270, 150, 20), main.randomizeRotationDirection, 0.0F, 1.0F);
			float.TryParse(GUI.TextField(new Rect(170, 260, 50, 20), main.randomizeRotationDirection.ToString()), out rRotParse);
			main.randomizeRotationDirection = rRotParse;
#elif AI || HS2
			main.flipRotation = GUI.HorizontalSlider(new Rect(10, 270, 150, 20), main.flipRotation, 0.0F, 1.0F);
			float.TryParse(GUI.TextField(new Rect(170, 260, 50, 20), main.flipRotation.ToString()), out rRotParse);
			main.flipRotation = rRotParse;
#endif

			// start size
			GUI.Label(new Rect(10, 300, 100, 20), "Start size:");

			var size = main.startSize;
			size.mode = ParticleSystemCurveMode.Constant;
			float sizeParse;
			main.startSize3D = GUI.Toggle(new Rect(225, 310, 150, 20), main.startSize3D, "3D (XYZ)");
			if (main.startSize3D) {
				float.TryParse(GUI.TextField(new Rect(10, 320, 50, 20), main.startSizeX.constant.ToString()), out sizeParse);
				var size3D = main.startSizeX;
				size3D.constant = sizeParse;
				main.startSizeX = size3D;

				float.TryParse(GUI.TextField(new Rect(70, 320, 50, 20), main.startSizeY.constant.ToString()), out sizeParse);
				size3D = main.startSizeY;
				size3D.constant = sizeParse;
				main.startSizeY = size3D;

				float.TryParse(GUI.TextField(new Rect(130, 320, 50, 20), main.startSizeZ.constant.ToString()), out sizeParse);
				size3D = main.startSizeZ;
				size3D.constant = sizeParse;
				main.startSizeZ = size3D;
			} else {
				size.constant = GUI.HorizontalSlider(new Rect(10, 320, 150, 20), size.constant, 0.0F, maxStartRot);
				float.TryParse(GUI.TextField(new Rect(170, 310, 50, 20), size.constant.ToString()), out sizeParse);
				size.constant = sizeParse;
				main.startSize = size;
			}

			// start color
			GUI.Label(new Rect(10, 350, 100, 20), "Start color:");

			var color = main.startColor;
			if (GUI.Button(new Rect(210, 370, 80, 20), getStartColorModeText(color.mode))) {
				switch (color.mode) {
					case ParticleSystemGradientMode.Color:
						color.mode = ParticleSystemGradientMode.Gradient;
						break;
					case ParticleSystemGradientMode.Gradient:
						color.mode = ParticleSystemGradientMode.TwoColors;
						break;
					case ParticleSystemGradientMode.TwoColors:
						color.mode = ParticleSystemGradientMode.TwoGradients;
						break;
					case ParticleSystemGradientMode.TwoGradients:
						color.mode = ParticleSystemGradientMode.Color;
						break;
				}
			}

			Color singleColor;
			GradientAlphaKey transparent;
			float colorParse;
			switch (color.mode) {
				case ParticleSystemGradientMode.Color:
					singleColor = color.color;
					float.TryParse(GUI.TextField(new Rect(10, 370, 40, 20), singleColor.r.ToString()), out colorParse);
					singleColor.r = colorParse;

					float.TryParse(GUI.TextField(new Rect(60, 370, 40, 20), singleColor.g.ToString()), out colorParse);
					singleColor.g = colorParse;

					float.TryParse(GUI.TextField(new Rect(110, 370, 40, 20), singleColor.b.ToString()), out colorParse);
					singleColor.b = colorParse;

					float.TryParse(GUI.TextField(new Rect(160, 370, 40, 20), singleColor.a.ToString()), out colorParse);
					singleColor.a = colorParse;

					color.color = singleColor;
					break;
				case ParticleSystemGradientMode.Gradient:
					if (color.gradient != null) {
						singleColor = color.gradient.colorKeys[0].color;
						transparent = color.gradient.alphaKeys[0];
					} else {
						singleColor = new Color();
						transparent = new GradientAlphaKey();
					}

					float.TryParse(GUI.TextField(new Rect(10, 370, 40, 20), singleColor.r.ToString()), out colorParse);
					singleColor.r = colorParse;

					float.TryParse(GUI.TextField(new Rect(60, 370, 40, 20), singleColor.g.ToString()), out colorParse);
					singleColor.g = colorParse;

					float.TryParse(GUI.TextField(new Rect(110, 370, 40, 20), singleColor.b.ToString()), out colorParse);
					singleColor.b = colorParse;

					float.TryParse(GUI.TextField(new Rect(160, 370, 40, 20), transparent.alpha.ToString()), out colorParse);
					transparent.alpha = colorParse;
					transparent.time = 0F;

					GradientColorKey[] colors = new GradientColorKey[2];
					colors[0] = new GradientColorKey(singleColor, 0F);

					GradientAlphaKey[] transparents = new GradientAlphaKey[2];
					transparents[0] = transparent;

					if (color.gradient != null) {
						singleColor = color.gradient.colorKeys[1].color;
						transparent = color.gradient.alphaKeys[1];
					} else {
						singleColor = new Color();
						transparent = new GradientAlphaKey();
					}

					float.TryParse(GUI.TextField(new Rect(10, 395, 40, 20), singleColor.r.ToString()), out colorParse);
					singleColor.r = colorParse;

					float.TryParse(GUI.TextField(new Rect(60, 395, 40, 20), singleColor.g.ToString()), out colorParse);
					singleColor.g = colorParse;

					float.TryParse(GUI.TextField(new Rect(110, 395, 40, 20), singleColor.b.ToString()), out colorParse);
					singleColor.b = colorParse;

					float.TryParse(GUI.TextField(new Rect(160, 395, 40, 20), transparent.alpha.ToString()), out colorParse);
					transparent.alpha = colorParse;
					transparent.time = 1F;

					colors[1] = new GradientColorKey(singleColor, 1F);

					transparents[1] = transparent;

					Gradient g = new Gradient {
						colorKeys = colors,
						alphaKeys = transparents
					};
					color.gradient = g;
					break;
				case ParticleSystemGradientMode.TwoColors:
					singleColor = color.colorMin;
					float.TryParse(GUI.TextField(new Rect(10, 370, 40, 20), singleColor.r.ToString()), out colorParse);
					singleColor.r = colorParse;

					float.TryParse(GUI.TextField(new Rect(60, 370, 40, 20), singleColor.g.ToString()), out colorParse);
					singleColor.g = colorParse;

					float.TryParse(GUI.TextField(new Rect(110, 370, 40, 20), singleColor.b.ToString()), out colorParse);
					singleColor.b = colorParse;

					float.TryParse(GUI.TextField(new Rect(160, 370, 40, 20), singleColor.a.ToString()), out colorParse);
					singleColor.a = colorParse;

					color.colorMin = singleColor;

					singleColor = color.colorMax;
					float.TryParse(GUI.TextField(new Rect(10, 395, 40, 20), singleColor.r.ToString()), out colorParse);
					singleColor.r = colorParse;

					float.TryParse(GUI.TextField(new Rect(60, 395, 40, 20), singleColor.g.ToString()), out colorParse);
					singleColor.g = colorParse;

					float.TryParse(GUI.TextField(new Rect(110, 395, 40, 20), singleColor.b.ToString()), out colorParse);
					singleColor.b = colorParse;

					float.TryParse(GUI.TextField(new Rect(160, 395, 40, 20), singleColor.a.ToString()), out colorParse);
					singleColor.a = colorParse;

					color.colorMax = singleColor;
					break;
				case ParticleSystemGradientMode.TwoGradients:
					// first gradient
					if (color.gradientMin != null) {
						singleColor = color.gradientMin.colorKeys[0].color;
						transparent = color.gradientMin.alphaKeys[0];
					} else {
						singleColor = new Color();
						transparent = new GradientAlphaKey();
					}

					float.TryParse(GUI.TextField(new Rect(10, 370, 40, 20), singleColor.r.ToString()), out colorParse);
					singleColor.r = colorParse;

					float.TryParse(GUI.TextField(new Rect(60, 370, 40, 20), singleColor.g.ToString()), out colorParse);
					singleColor.g = colorParse;

					float.TryParse(GUI.TextField(new Rect(110, 370, 40, 20), singleColor.b.ToString()), out colorParse);
					singleColor.b = colorParse;

					float.TryParse(GUI.TextField(new Rect(160, 370, 40, 20), transparent.alpha.ToString()), out colorParse);
					transparent.alpha = colorParse;
					transparent.time = 0F;

					GradientColorKey[] colors2Grad = new GradientColorKey[2];
					colors2Grad[0] = new GradientColorKey(singleColor, 0F);

					GradientAlphaKey[] transparents2Grad = new GradientAlphaKey[2];
					transparents2Grad[0] = transparent;

					if (color.gradientMin != null) {
						singleColor = color.gradientMin.colorKeys[1].color;
						transparent = color.gradientMin.alphaKeys[1];
					} else {
						singleColor = new Color();
						transparent = new GradientAlphaKey();
					}

					float.TryParse(GUI.TextField(new Rect(10, 395, 40, 20), singleColor.r.ToString()), out colorParse);
					singleColor.r = colorParse;

					float.TryParse(GUI.TextField(new Rect(60, 395, 40, 20), singleColor.g.ToString()), out colorParse);
					singleColor.g = colorParse;

					float.TryParse(GUI.TextField(new Rect(110, 395, 40, 20), singleColor.b.ToString()), out colorParse);
					singleColor.b = colorParse;

					float.TryParse(GUI.TextField(new Rect(160, 395, 40, 20), transparent.alpha.ToString()), out colorParse);
					transparent.alpha = colorParse;
					transparent.time = 1F;

					colors2Grad[1] = new GradientColorKey(singleColor, 1F);

					transparents2Grad[1] = transparent;

					Gradient g2Grad = new Gradient {
						colorKeys = colors2Grad,
						alphaKeys = transparents2Grad
					};
					color.gradientMin = g2Grad;

					// second gradient
					if (color.gradientMax != null) {
						singleColor = color.gradientMax.colorKeys[0].color;
						transparent = color.gradientMax.alphaKeys[0];
					} else {
						singleColor = new Color();
						transparent = new GradientAlphaKey();
					}

					float.TryParse(GUI.TextField(new Rect(10, 425, 40, 20), singleColor.r.ToString()), out colorParse);
					singleColor.r = colorParse;

					float.TryParse(GUI.TextField(new Rect(60, 425, 40, 20), singleColor.g.ToString()), out colorParse);
					singleColor.g = colorParse;

					float.TryParse(GUI.TextField(new Rect(110, 425, 40, 20), singleColor.b.ToString()), out colorParse);
					singleColor.b = colorParse;

					float.TryParse(GUI.TextField(new Rect(160, 425, 40, 20), transparent.alpha.ToString()), out colorParse);
					transparent.alpha = colorParse;
					transparent.time = 0F;

					colors2Grad = new GradientColorKey[2];
					colors2Grad[0] = new GradientColorKey(singleColor, 0F);

					transparents2Grad = new GradientAlphaKey[2];
					transparents2Grad[0] = transparent;

					if (color.gradientMax != null) {
						singleColor = color.gradientMax.colorKeys[1].color;
						transparent = color.gradientMax.alphaKeys[1];
					} else {
						singleColor = new Color();
						transparent = new GradientAlphaKey();
					}

					float.TryParse(GUI.TextField(new Rect(10, 450, 40, 20), singleColor.r.ToString()), out colorParse);
					singleColor.r = colorParse;

					float.TryParse(GUI.TextField(new Rect(60, 450, 40, 20), singleColor.g.ToString()), out colorParse);
					singleColor.g = colorParse;

					float.TryParse(GUI.TextField(new Rect(110, 450, 40, 20), singleColor.b.ToString()), out colorParse);
					singleColor.b = colorParse;

					float.TryParse(GUI.TextField(new Rect(160, 450, 40, 20), transparent.alpha.ToString()), out colorParse);
					transparent.alpha = colorParse;
					transparent.time = 1F;

					colors2Grad[1] = new GradientColorKey(singleColor, 1F);

					transparents2Grad[1] = transparent;

					g2Grad = new Gradient {
						colorKeys = colors2Grad,
						alphaKeys = transparents2Grad
					};
					color.gradientMax = g2Grad;
					break;
			}
			main.startColor = color;
		}
    }
}
