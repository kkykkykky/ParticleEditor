using System;
using UnityEngine;
using BepInEx;
using BepInEx.Logging;
using KKAPI.Studio.SaveLoad;
using BepInEx.Configuration;
using KKAPI.Utilities;

namespace RSkoi_ParticleEditor {
    [BepInPlugin(GUID, Game+"Particle Editor", Version)]
    [BepInDependency(KKAPI.KoikatuAPI.GUID, KKAPI.KoikatuAPI.VersionConst)]
    public partial class ParticleEditor : BaseUnityPlugin {
        private const string PluginNameInternal = "ParticleEditor";

        private const string GUID = "com.rskoi.particleeditor";
        public const string Version = "1.0.1";

        internal static new ManualLogSource Logger;

        internal static ConfigEntry<KeyboardShortcut> GUIShortcut { get; private set; }

        internal void Awake() {
            Logger = base.Logger;

            GUIShortcut = Config.Bind("Keyboard Shortcuts", "Show ParticleEditor GUI", new KeyboardShortcut(KeyCode.P, KeyCode.LeftControl), new ConfigDescription("Bring up the main GUI window of ParticleEditor.", null, new ConfigurationManagerAttributes { Order = 1 }));

            StudioSaveLoadApi.RegisterExtraBehaviour<ParticleEditorSceneController>(GUID);
        }

        void OnGUI() {
            InitGUI();
        }
    }
}