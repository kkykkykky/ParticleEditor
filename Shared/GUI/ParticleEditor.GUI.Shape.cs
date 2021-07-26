using System;
using UnityEngine;

namespace RSkoi_ParticleEditor {
	partial class ParticleEditor {
		private static Vector2 scrollShapeToolbar = Vector2.zero;
		private static string[] toolbarShapeStrings = { "Sphere", "Hemisph.", "Cone", "Box", "Circle", "Edge" };

		private static float maxShapeRadius = 10.0F;
		private static float maxArcSpeed = 20.0F;

		private void SetupToolbarShape(GUIStyle tgStyle) {
			var shape = selectedPS.shape;

			// module enabled
			shape.enabled = GUI.Toggle(new Rect(10, 20, 120, 20), shape.enabled, "Module enabled", tgStyle);

			// shape
			scrollShapeToolbar = GUI.BeginScrollView(new Rect(10, 40, 275, 40), scrollShapeToolbar, new Rect(0, 0, 400, 20), true, false);
			shape.shapeType = (ParticleSystemShapeType)convertShapeToAvailable(true, GUI.Toolbar(new Rect(0, 0, 400, 20), convertShapeToAvailable(false, (int)shape.shapeType), toolbarShapeStrings));
			GUI.EndScrollView();

			float shParse;
			if (shape.shapeType != ParticleSystemShapeType.Box) {
				// radius
				GUI.Label(new Rect(10, 90, 50, 20), "Radius:");

				shape.radius = GUI.HorizontalSlider(new Rect(10, 110, 150, 20), shape.radius, 0.0F, maxShapeRadius);
				float.TryParse(GUI.TextField(new Rect(170, 100, 50, 20), shape.radius.ToString()), out shParse);
				shape.radius = shParse;
			} else {
				// box xyz
				GUI.Label(new Rect(10, 90, 100, 20), "Box XYZ:");

#if KK
				var box = shape.box;
#elif AI || HS2
				var box = shape.scale;
#endif
				float.TryParse(GUI.TextField(new Rect(10, 110, 50, 20), box.x.ToString()), out shParse);
				box.x = shParse;

				float.TryParse(GUI.TextField(new Rect(70, 110, 50, 20), box.y.ToString()), out shParse);
				box.y = shParse;

				float.TryParse(GUI.TextField(new Rect(130, 110, 50, 20), box.z.ToString()), out shParse);
				box.z = shParse;

#if KK
				shape.box = box;
#elif AI || HS2
				shape.scale = box;
#endif
			}

			// aling to direction
			shape.alignToDirection = GUI.Toggle(new Rect(10, 130, 150, 20), shape.alignToDirection, "align to direction");

			// random direction
			GUI.Label(new Rect(10, 160, 200, 20), "Random direction amount:");

			shape.randomDirectionAmount = GUI.HorizontalSlider(new Rect(10, 180, 150, 20), shape.randomDirectionAmount, 0.0F, 1.0F);
			float.TryParse(GUI.TextField(new Rect(170, 170, 50, 20), shape.randomDirectionAmount.ToString()), out shParse);
			shape.randomDirectionAmount = shParse;

			// spherize amount
			GUI.Label(new Rect(10, 210, 200, 20), "Sphere direction amount:");

			shape.sphericalDirectionAmount = GUI.HorizontalSlider(new Rect(10, 230, 150, 20), shape.sphericalDirectionAmount, 0.0F, 1.0F);
			float.TryParse(GUI.TextField(new Rect(170, 220, 50, 20), shape.sphericalDirectionAmount.ToString()), out shParse);
			shape.sphericalDirectionAmount = shParse;

			if (shape.shapeType == ParticleSystemShapeType.Cone) {
				// angle
				GUI.Label(new Rect(10, 260, 50, 20), "Angle:");

				shape.angle = GUI.HorizontalSlider(new Rect(10, 280, 150, 20), shape.angle, 0.0F, 90.0F);
				float.TryParse(GUI.TextField(new Rect(170, 270, 50, 20), shape.angle.ToString()), out shParse);
				shape.angle = shParse;
			}
			int offset = (shape.shapeType == ParticleSystemShapeType.Circle) ? 50 : 0;


			if (shape.shapeType != ParticleSystemShapeType.SingleSidedEdge && (
				shape.shapeType == ParticleSystemShapeType.Cone || shape.shapeType == ParticleSystemShapeType.Circle)) {
				// arc
				GUI.Label(new Rect(10, 310 - offset, 50, 20), "Arc:");

				shape.arc = GUI.HorizontalSlider(new Rect(10, 330 - offset, 150, 20), shape.arc, 0.0F, 360.0F);
				float.TryParse(GUI.TextField(new Rect(170, 320 - offset, 50, 20), shape.arc.ToString()), out shParse);
				shape.arc = shParse;

				// spread
				GUI.Label(new Rect(10, 360 - offset, 100, 20), "Spread:");
				// mode (random and loop)
				shape.arcMode = GUI.Toggle(new Rect(225, 370 - offset, 100, 50), (shape.arcMode == ParticleSystemShapeMultiModeValue.Loop), "Loop\nmode")
					? ParticleSystemShapeMultiModeValue.Loop : ParticleSystemShapeMultiModeValue.Random;

				shape.arcSpread = GUI.HorizontalSlider(new Rect(10, 380 - offset, 150, 20), shape.arcSpread, 0.0F, 1.0F);
				float.TryParse(GUI.TextField(new Rect(170, 370 - offset, 50, 20), shape.arcSpread.ToString()), out shParse);
				shape.arcSpread = shParse;

				if (shape.arcMode == ParticleSystemShapeMultiModeValue.Loop) {
					GUI.Label(new Rect(10, 390 - offset, 100, 20), "Speed:");
					var arcSpeed = shape.arcSpeed;
					arcSpeed.constant = GUI.HorizontalSlider(new Rect(10, 410 - offset, 150, 20), arcSpeed.constant, 0.0F, maxArcSpeed);
					float.TryParse(GUI.TextField(new Rect(170, 400 - offset, 50, 20), arcSpeed.constant.ToString()), out shParse);
					arcSpeed.constant = shParse;
					shape.arcSpeed = arcSpeed;
				}
			} else if (shape.shapeType == ParticleSystemShapeType.SingleSidedEdge) {
				// spread
				GUI.Label(new Rect(10, 260, 100, 20), "Spread:");
				// mode (random and loop)
				shape.radiusMode = GUI.Toggle(new Rect(225, 270, 100, 50), (shape.radiusMode == ParticleSystemShapeMultiModeValue.Loop) , "Loop\nmode")
					? ParticleSystemShapeMultiModeValue.Loop : ParticleSystemShapeMultiModeValue.Random;

				shape.radiusSpread = GUI.HorizontalSlider(new Rect(10, 280, 150, 20), shape.radiusSpread, 0.0F, 1.0F);
				float.TryParse(GUI.TextField(new Rect(170, 270, 50, 20), shape.radiusSpread.ToString()), out shParse);
				shape.radiusSpread = shParse;

				if (shape.radiusMode == ParticleSystemShapeMultiModeValue.Loop) {
					GUI.Label(new Rect(10, 290, 100, 20), "Speed:");
					var radiusSpeed = shape.radiusSpeed;
					radiusSpeed.constant = GUI.HorizontalSlider(new Rect(10, 310, 150, 20), radiusSpeed.constant, 0.0F, maxArcSpeed);
					float.TryParse(GUI.TextField(new Rect(170, 300, 50, 20), radiusSpeed.constant.ToString()), out shParse);
					radiusSpeed.constant = shParse;
					shape.radiusSpeed = radiusSpeed;
				}
			}
		}

		private static int convertShapeToAvailable(bool toEnum, int shape) {
			if (toEnum) {
				switch (shape) {
					case 0:
						return (int)ParticleSystemShapeType.Sphere;
					case 1:
						return (int)ParticleSystemShapeType.Hemisphere;
					case 2:
						return (int)ParticleSystemShapeType.Cone;
					case 3:
						return (int)ParticleSystemShapeType.Box;
					case 4:
						return (int)ParticleSystemShapeType.Circle;
					case 5:
						return (int)ParticleSystemShapeType.SingleSidedEdge;
					default:
						return (int)ParticleSystemShapeType.Sphere;
				}
			} else {
				switch (shape) {
					case (int)ParticleSystemShapeType.Sphere:
						return 0;
					case (int)ParticleSystemShapeType.Hemisphere:
						return 1;
					case (int)ParticleSystemShapeType.Cone:
						return 2;
					case (int)ParticleSystemShapeType.Box:
						return 3;
					case (int)ParticleSystemShapeType.Circle:
						return 4;
					case (int)ParticleSystemShapeType.SingleSidedEdge:
						return 5;
					default:
						return 0;
				}
			}
		}

		private static int offsetScrollSizeShape(ParticleSystem.ShapeModule shape) {
			switch (shape.shapeType) {
				case ParticleSystemShapeType.Box:
				case ParticleSystemShapeType.Hemisphere:
				case ParticleSystemShapeType.Sphere:
					return 180;
				case ParticleSystemShapeType.Circle:
					return 50;
				case ParticleSystemShapeType.SingleSidedEdge:
					return 100;
				default:
					return 0;
			}
		}
	}
}
