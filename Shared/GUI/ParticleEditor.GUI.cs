using System;
using System.Collections.Generic;
using UnityEngine;
using Studio;

namespace RSkoi_ParticleEditor {
    public partial class ParticleEditor {
		public static ParticleSystem parentPS;
		private static ParticleSystem selectedPS;
		private static ObjectCtrlInfo objectCtrl;
		private static int selectedPSIndex = 0;
		private static ParticleSystem[] children;

		//private static string shortcut = "^P";

		private static Rect editorWindow = new Rect(100, 0, 600, 395);
        private static bool showWindow = false;

        private static Vector2 scroll = Vector2.zero;
        private static int maxScrollHeight = 200;

        private static bool pauseChildren = false;
		private static bool resetSimChildren = false;

		private static Vector2 scrollToolbar = Vector2.zero;
		private static Vector2 scrollToolbarInner = Vector2.zero;
		private static int currentToolbar = 0;
		private static string[] toolbarStrings = { "General", "Shape", "Renderer", "Emission", "Noise", "Lifetime Mod.", "Speed Mod." };

        private static float maxSpeed = 4.0F;

		private void InitGUI() {
			// ctrl+p to show window
			// requires exactly one item to be selected
			if (!showWindow && GUIShortcut.Value.IsDown() /*Event.current.Equals(Event.KeyboardEvent(shortcut))*/ && Singleton<Studio.Studio>.Instance.treeNodeCtrl.selectNodes.Length == 1) {
                showWindow = true;
                TreeNodeObject[] selectNodes = Singleton<Studio.Studio>.Instance.treeNodeCtrl.selectNodes;
                if (Singleton<Studio.Studio>.Instance.dicInfo.TryGetValue(selectNodes[0], out ObjectCtrlInfo objectCtrlInfo)) {
                    if (objectCtrlInfo is OCIItem ociItem) {
                        parentPS = ociItem.objectItem.GetComponentInChildren<ParticleSystem>();
						objectCtrl = objectCtrlInfo;
                    }
                }
            } else if (showWindow && Singleton<Studio.Studio>.Instance.treeNodeCtrl.selectNodes.Length != 1) {
                showWindow = false;
            }

            // show editor window
            if (showWindow) {
                var _editorWindow = GUI.Window(4335, editorWindow, EditorWindowWork, "Particle Editor");
                editorWindow.x = _editorWindow.x;
                editorWindow.y = _editorWindow.y;

                // Prevent clicks from going through
                if (editorWindow.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y))) {
                    Input.ResetInputAxes();
                }
            }
        }

		private void EditorWindowWork(int id) {
			GUIStyle lStyle = new GUIStyle(GUI.skin.label);
			lStyle.normal.textColor = Color.red;

			GUIStyle bsStyle = new GUIStyle(GUI.skin.button);
			bsStyle.normal.textColor = Color.red;
			bsStyle.active.textColor = Color.red;
			bsStyle.hover.textColor = Color.red;

			GUIStyle tgStyle = new GUIStyle(GUI.skin.toggle);
			tgStyle.normal.textColor = Color.red;
			tgStyle.hover.textColor = Color.red;

			if (parentPS != null)
            {
				// list of particle systems including children
				children = parentPS.gameObject.GetComponentsInChildren<ParticleSystem>();
				selectedPS = (selectedPSIndex < children.Length) ? children[selectedPSIndex] : children[selectedPSIndex = 0];
				int totalHeight = (children != null) ? ((children.Length * 20 + children.Length * 4) + 25 >= maxScrollHeight ? (children.Length * 20 + children.Length * 4) + 25 : maxScrollHeight) : maxScrollHeight;
				scroll = GUI.BeginScrollView(new Rect(350, 30, 230, maxScrollHeight), scroll, new Rect(0, 0, 200, totalHeight), false, true);

				GUI.Box(new Rect(0, 0, 230, totalHeight), "Particle Systems");

				List<string> childrenNames = new List<string>();
				for (int i = 0; i < children.Length; i++)
				{
					string add = i != 0 ? i.ToString() : "root";
					childrenNames.Add(children[i].name + " (" + add + ")");
				}

				GUIStyle bStyle = new GUIStyle(GUI.skin.button)
				{
					fixedWidth = 200,
					fixedHeight = 20
				};
				selectedPSIndex = GUILayout.SelectionGrid(selectedPSIndex, childrenNames.ToArray(), 1, bStyle, GUILayout.Width(200), GUILayout.Height(totalHeight));

				GUI.EndScrollView();

				// pause button
				pauseChildren = GUI.Toggle(new Rect(20, 50, 120, 20), pauseChildren, "all children");
				if (GUI.Button(new Rect(20, 30, 80, 20), (selectedPS.isPlaying) ? "Pause" : "Resume"))
				{
					if (selectedPS.isPlaying)
					{
						selectedPS.Pause(pauseChildren);
					}
					else
					{
						selectedPS.Play(pauseChildren);
					}
				}

				// simulation speed
				GUI.Label(new Rect(120, 20, 50, 20), "Speed:");
				ParticleSystem.MainModule main = selectedPS.main;
				main.simulationSpeed = GUI.HorizontalSlider(new Rect(120, 40, 150, 20), selectedPS.main.simulationSpeed, 0.0F, maxSpeed);
				float simSpeedParse;
				float.TryParse(GUI.TextField(new Rect(280, 30, 50, 20), selectedPS.main.simulationSpeed.ToString()), out simSpeedParse);
				main.simulationSpeed = simSpeedParse;

				// toolbar
				scrollToolbar = GUI.BeginScrollView(new Rect(20, 80, 310, 40), scrollToolbar, new Rect(0, 0, 700, 20), true, false);
				currentToolbar = GUI.Toolbar(new Rect(0, 0, 700, 20), currentToolbar, toolbarStrings);
				GUI.EndScrollView();

				SetupToolbar(currentToolbar, new GUIStyle[] { lStyle, bsStyle, tgStyle });

				// time
				GUI.Label(new Rect(350, 240, 60, 20), "Time:");
				GUI.TextField(new Rect(350, 260, 60, 20), selectedPS.time.ToString());

				// seed
				GUI.Label(new Rect(420, 240, 60, 20), "Seed:");
				if (selectedPS.isPlaying)
				{
					GUI.Label(new Rect(350, 280, 300, 20), "seed can only be set on paused systems", lStyle);
					GUI.TextField(new Rect(420, 260, 100, 20), selectedPS.randomSeed.ToString());
				}
				else
				{
					uint seedParse;
					uint.TryParse(GUI.TextField(new Rect(420, 260, 100, 20), selectedPS.randomSeed.ToString()), out seedParse);
					selectedPS.randomSeed = seedParse;
				}

				// save button
				if (GUI.Button(new Rect(440, 355, 140, 20), "Save", bsStyle))
				{
					WorkSave();
				}

				// clear
				if (GUI.Button(new Rect(350, 355, 80, 20), "Clear", bsStyle))
				{
					selectedPS.Clear();
				}

				// reset
				resetSimChildren = GUI.Toggle(new Rect(350, 330, 80, 20), resetSimChildren, "all children");
				if (GUI.Button(new Rect(350, 310, 80, 20), "Reset Sim.", bsStyle))
				{
					selectedPS.Simulate(0.0F, resetSimChildren);
				}
			}
			else
            {
				GUI.Label(new Rect(20, 30, 300, 20), "No valid Particle System object selected");
			}

			// close button
			if (GUI.Button(new Rect(500, 5, 80, 20), "Close", bsStyle)) {
				showWindow = false;
			}

			GUI.DragWindow(new Rect(0, 0, 10000, 20));
		}

		private void SetupToolbar(int index, GUIStyle[] styles) {
			int currentToolbarHeight = 250;
			ParticleSystem.MainModule main = selectedPS.main;

			switch (index) {
				case 0:
					currentToolbarHeight = 480;
					break;
				case 1:
					currentToolbarHeight = 430 - offsetScrollSizeShape(selectedPS.shape);
					break;
				case 2:
					currentToolbarHeight = 540 - offsetScrollSizeRenderer(selectedPS.gameObject.GetComponent<ParticleSystemRenderer>());
					break;
				case 3:
					currentToolbarHeight += ((selectedPS.emission.burstCount > 0) ? selectedPS.emission.burstCount - 1 : 0) * 40;
					break;
				case 4:
					currentToolbarHeight = 370;
					break;
				case 5:
					currentToolbarHeight = 740;
					break;
				case 6:
					currentToolbarHeight = 470;
					break;
			}

			scrollToolbarInner = GUI.BeginScrollView(new Rect(20, 125, 310, 250), scrollToolbarInner, new Rect(0, 0, 290, currentToolbarHeight), false, true);
			GUI.Box(new Rect(0, 0, 310, currentToolbarHeight), toolbarStrings[index]);

			// "General", "Shape", "Renderer", "Emission", "Noise", "Lifetime Mod.", "Speed Mod."
			switch (index) { // disgusting, I know
				// general
				case 0:
					SetupToolbarGeneral(main, styles[0]);
					break;
				// shape
				case 1:
					SetupToolbarShape(styles[2]);
					break;
				// renderer
				case 2:
					SetupToolbarRenderer(styles[2]);
					break;
				// emission
				case 3:
					SetupToolbarEmission(styles[2], styles[1], styles[0]);
					break;
				// noise
				case 4:
					SetupToolbarNoise(styles[2]);
					break;
				// lifetime mod
				case 5:
					SetupToolbarLifetime(styles[2]);
					break;
				// speed mod
				case 6:
					SetupToolbarSpeed(styles[2]);
					break;
			}

			GUI.EndScrollView();
		}

		private string getStartColorModeText(ParticleSystemGradientMode mode) {
			switch (mode) {
				case ParticleSystemGradientMode.Color:
					return "RGBA";
				case ParticleSystemGradientMode.Gradient:
					return "Gradient";
				case ParticleSystemGradientMode.TwoColors:
					return "2xRGBA";
				case ParticleSystemGradientMode.TwoGradients:
					return "2xGradient";
				default:
					return "RGBA";
			}
		}
	}
}