using System;
using UnityEngine;

namespace RSkoi_ParticleEditor {
    partial class ParticleEditor {
		private static Vector2 scrollRenderToolbar = Vector2.zero;
		private static string[] toolbarRenderStrings = { "Billboard", "Str.Billboard", "Hor.Billboard", "Ver.Billboard" };

		private static float maxStretch = 15.0F;

		private static Vector2 scrollRenderBillAlign = Vector2.zero;
		private static string[] scrollRenderBillAlignStrings = { "View", "World", "Local", "Facing" };

		private static Vector2 scrollRenderSort = Vector2.zero;
		private static string[] toolbarRenderSortStrings = { "None", "Distance", "Oldest", "Youngest" };

		private static float maxSortFudge = 20.0F;

		private static float minMaxSize = 5.0F;

		private void SetupToolbarRenderer(GUIStyle tgStyle) {
			var render = selectedPS.gameObject.GetComponent<ParticleSystemRenderer>();

			// module enabled
			render.enabled = GUI.Toggle(new Rect(10, 20, 120, 20), render.enabled, "Module enabled", tgStyle);
			// render mode
			scrollRenderToolbar = GUI.BeginScrollView(new Rect(10, 40, 275, 40), scrollRenderToolbar, new Rect(0, 0, 400, 20), true, false);
			render.renderMode = (ParticleSystemRenderMode)GUI.Toolbar(new Rect(0, 0, 400, 20), (int)render.renderMode, toolbarRenderStrings);
			GUI.EndScrollView();

			float rdParse;

			int offset = 0;
			if (render.renderMode == ParticleSystemRenderMode.Billboard) {
				offset = 70;

				// billboard alignment
				GUI.Label(new Rect(10, 90, 120, 20), "Billboard alignment:");

				scrollRenderBillAlign = GUI.BeginScrollView(new Rect(10, 110, 275, 40), scrollRenderBillAlign, new Rect(0, 0, 300, 20), true, false);
				render.alignment = (ParticleSystemRenderSpace)GUI.Toolbar(new Rect(0, 0, 300, 20), (int)render.alignment, scrollRenderBillAlignStrings);
				GUI.EndScrollView();
			} else if (render.renderMode == ParticleSystemRenderMode.Stretch) {
				offset = 150;

				// camera scale
				GUI.Label(new Rect(10, 90, 100, 20), "Camera scale:");

				render.cameraVelocityScale = GUI.HorizontalSlider(new Rect(10, 110, 150, 20), render.cameraVelocityScale, -maxStretch, maxStretch);
				float.TryParse(GUI.TextField(new Rect(170, 100, 50, 20), render.cameraVelocityScale.ToString()), out rdParse);
				render.cameraVelocityScale = rdParse;

				// speed scale
				GUI.Label(new Rect(10, 140, 100, 20), "Speed scale:");

				render.velocityScale = GUI.HorizontalSlider(new Rect(10, 160, 150, 20), render.velocityScale, -maxStretch, maxStretch);
				float.TryParse(GUI.TextField(new Rect(170, 150, 50, 20), render.velocityScale.ToString()), out rdParse);
				render.velocityScale = rdParse;

				// length scale
				GUI.Label(new Rect(10, 190, 100, 20), "Length scale:");

				render.lengthScale = GUI.HorizontalSlider(new Rect(10, 210, 150, 20), render.lengthScale, -maxStretch, maxStretch);
				float.TryParse(GUI.TextField(new Rect(170, 200, 50, 20), render.lengthScale.ToString()), out rdParse);
				render.lengthScale = rdParse;
			}

			// normal direction
			GUI.Label(new Rect(10, 90 + offset, 100, 20), "Normal direction:");

			render.normalDirection = GUI.HorizontalSlider(new Rect(10, 110 + offset, 150, 20), render.normalDirection, 0.0F, 1.0F);
			float.TryParse(GUI.TextField(new Rect(170, 100 + offset, 50, 20), render.normalDirection.ToString()), out rdParse);
			render.normalDirection = rdParse;

			// sort mode
			GUI.Label(new Rect(10, 130 + offset, 100, 20), "Sort mode:");

			scrollRenderSort = GUI.BeginScrollView(new Rect(10, 150 + offset, 275, 40), scrollRenderSort, new Rect(0, 0, 300, 20), true, false);
			render.sortMode = (ParticleSystemSortMode)GUI.Toolbar(new Rect(0, 0, 300, 20), (int)render.sortMode, toolbarRenderSortStrings);
			GUI.EndScrollView();

			// sorting fudge
			GUI.Label(new Rect(10, 200 + offset, 100, 20), "Sorting fudge:");

			render.sortingFudge = GUI.HorizontalSlider(new Rect(10, 220 + offset, 150, 20), render.sortingFudge, -maxSortFudge, maxSortFudge);
			float.TryParse(GUI.TextField(new Rect(170, 210 + offset, 50, 20), render.sortingFudge.ToString()), out rdParse);
			render.sortingFudge = rdParse;

			// min particle size
			GUI.Label(new Rect(10, 250 + offset, 100, 20), "Min. size:");

			render.minParticleSize = GUI.HorizontalSlider(new Rect(10, 270 + offset, 150, 20), render.minParticleSize, 0.0F, minMaxSize);
			float.TryParse(GUI.TextField(new Rect(170, 260 + offset, 50, 20), render.minParticleSize.ToString()), out rdParse);
			render.minParticleSize = rdParse;

			// max particle size
			GUI.Label(new Rect(10, 300 + offset, 100, 20), "Max. size:");

			render.maxParticleSize = GUI.HorizontalSlider(new Rect(10, 320 + offset, 150, 20), render.maxParticleSize, 0.0F, minMaxSize);
			float.TryParse(GUI.TextField(new Rect(170, 310 + offset, 50, 20), render.maxParticleSize.ToString()), out rdParse);
			render.maxParticleSize = rdParse;

			if (render.maxParticleSize < render.minParticleSize) {
				render.maxParticleSize = render.minParticleSize;
			} else if (render.minParticleSize > render.maxParticleSize) {
				render.maxParticleSize = render.minParticleSize;
			}

			// pivot xyz
			GUI.Label(new Rect(10, 340 + offset, 100, 20), "Pivot XYZ:");

			var pivot = render.pivot;
			float.TryParse(GUI.TextField(new Rect(10, 360 + offset, 50, 20), pivot.x.ToString()), out rdParse);
			pivot.x = rdParse;

			float.TryParse(GUI.TextField(new Rect(70, 360 + offset, 50, 20), pivot.y.ToString()), out rdParse);
			pivot.y = rdParse;

			float.TryParse(GUI.TextField(new Rect(130, 360 + offset, 50, 20), pivot.z.ToString()), out rdParse);
			pivot.z = rdParse;
			render.pivot = pivot;
		}

		private static int offsetScrollSizeRenderer(ParticleSystemRenderer render) {
			switch (render.renderMode) {
				case ParticleSystemRenderMode.VerticalBillboard:
				case ParticleSystemRenderMode.HorizontalBillboard:
					return 150;
				case ParticleSystemRenderMode.Billboard:
					return 80;
				default:
					return 0;
			}
		}
	}
}