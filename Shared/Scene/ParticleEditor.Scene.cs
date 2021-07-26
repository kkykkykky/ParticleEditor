using System;
using System.Timers;
using System.Collections.Generic;
using MessagePack;
using UnityEngine;
using ExtensibleSaveFormat;
using KKAPI.Utilities;
using KKAPI.Studio.SaveLoad;
using Studio;

namespace RSkoi_ParticleEditor {
    public partial class ParticleEditor {
        private static readonly SortedDictionary<int, PSProperty> PSDict = new SortedDictionary<int, PSProperty>();

        public class ParticleEditorSceneController : SceneCustomFunctionController {
            private static void ResetAttributes() {
                showWindow = false;
                pauseChildren = false;

                parentPS = null;
                selectedPS = null;
                objectCtrl = null;
                selectedPSIndex = 0;
                children = null;
            }

            private void SetTimer(ParticleSystem ps, float time) {
                Timer aTimer = new Timer(50);
                aTimer.Elapsed += (s, e) => {
                    if (ps.time >= time /*&& ps.time < ps.time + 0.05*/) {
                        ps.Pause(false);
                        aTimer.Stop();
                        aTimer.Dispose();
                    }
                };
                aTimer.AutoReset = true;
                aTimer.Enabled = true;
            }

            private void SetChildren(ParticleSystem[] rootChildren, PSProperty[] children) {
                if (rootChildren.Length < 2 || children.Length < 2 || rootChildren.Length != children.Length) {
                    return;
                }

                for (int i = 0; i < children.Length; i++) {
                    SetProperties(rootChildren[i], children[i]);
                }
            }

            private void SetProperties(ParticleSystem ps, PSProperty psp) {
                ps.Pause();
                ps.randomSeed = psp.Seed;
                ps.useAutoRandomSeed = psp.AutoSeed;
                ps.Clear();
                ps.Play();

                var main = ps.main;
                main.simulationSpeed = psp.Speed;
                if (psp.Paused) {
                    SetTimer(ps, psp.Time);
                }

                SetGeneralProperty(main, psp.General);
                SetShapeProperty(ps.shape, psp.Shape);
                SetRendererProperty(ps, psp.Renderer);
                SetEmissionProperty(ps.emission, psp.Emission);
                SetNoiseProperty(ps.noise, psp.Noise);
                SetLifetimeProperty(ps, psp.Lifetime);
                SetSpeedProperty(ps, psp.SpeedP);
            }

            protected override void OnSceneSave() {
                var data = new PluginData();

                if (PSDict.Count > 0) {
                    data.data.Add(nameof(PSDict), MessagePackSerializer.Serialize(PSDict));
                } else {
                    data.data.Add(nameof(PSDict), null);
                }

                SetExtendedData(data);
            }

            protected override void OnSceneLoad(SceneOperationKind operation, ReadOnlyDictionary<int, ObjectCtrlInfo> loadedItems) {
                var data = GetExtendedData();

                if (data == null) {
                    return;
                }

                if (operation == SceneOperationKind.Clear || operation == SceneOperationKind.Load) {
                    ResetAttributes();
                    PSDict.Clear();
                }

                if (data.data.TryGetValue(nameof(PSDict), out var psProperties) && psProperties != null) {
                    foreach (var loadedProperty in MessagePackSerializer.Deserialize<SortedDictionary<int, PSProperty>>((byte[])psProperties)) {
                        if (loadedItems.TryGetValue(loadedProperty.Key, out ObjectCtrlInfo objectCtrlInfo) && objectCtrlInfo is OCIItem ociItem) {
                            PSDict.Add(GetObjectID(objectCtrlInfo), loadedProperty.Value);

                            // root
                            SetProperties(ociItem.objectItem.GetComponentInChildren<ParticleSystem>(), loadedProperty.Value);
                            // children
                            List<ParticleSystem> lp = new List<ParticleSystem>(ociItem.objectItem.GetComponentInChildren<ParticleSystem>().gameObject.GetComponentsInChildren<ParticleSystem>());
                            lp.RemoveAt(0);
                            SetChildren(lp.ToArray(), loadedProperty.Value.Children);
                        }
                    }
                }
            }
        }
    }
}