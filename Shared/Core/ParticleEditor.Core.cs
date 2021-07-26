using System;
using System.Linq;
using System.Collections.Generic;
using Studio;
using UnityEngine;

namespace RSkoi_ParticleEditor {
    public partial class ParticleEditor {
        private void WorkSave() {
            int objectID = GetObjectID(objectCtrl);
            PSProperty psPr = SetupPSProperty(parentPS, objectID);

            //if (PSDict.ContainsKey(objectID)) {
                PSDict.Remove(objectID);
            //}
            PSDict.Add(objectID, psPr);
        }

        private PSProperty SetupPSProperty(ParticleSystem ps, int id) {
            ParticleSystem[] children = ps.gameObject.GetComponentsInChildren<ParticleSystem>();
            List<PSProperty> lChildren = new List<PSProperty>();

            for (int i = 1; i < children.Length; i++) {
                PSProperty psp = new PSProperty(
                    children[i].gameObject.GetInstanceID(),
                    children[i].time, children[i].isPaused,
                    children[i].randomSeed,
                    children[i].useAutoRandomSeed,
                    children[i].main.simulationSpeed,
                    null,
                    SetupGeneralProperty(children[i]),
                    SetupShapeProperty(children[i]),
                    SetupRendererProperty(children[i]),
                    SetupEmissionProperty(children[i]),
                    SetupNoiseProperty(children[i]),
                    SetupLifetimeProperty(children[i]),
                    SetupSpeedProperty(children[i])
                );
                lChildren.Add(psp);
            }

            PSProperty p = new PSProperty(
                id,
                ps.time,
                ps.isPaused,
                ps.randomSeed,
                ps.useAutoRandomSeed,
                ps.main.simulationSpeed,
                (lChildren.Count() == 0) ? null : lChildren.ToArray(),
                SetupGeneralProperty(ps),
                SetupShapeProperty(ps),
                SetupRendererProperty(ps),
                SetupEmissionProperty(ps),
                SetupNoiseProperty(ps),
                SetupLifetimeProperty(ps),
                SetupSpeedProperty(ps)
            );

            return p;
        }

        private SpeedProperty SetupSpeedProperty(ParticleSystem ps) {
            var cbp = ps.colorBySpeed;
            var rbp = ps.rotationBySpeed;
            var sbp = ps.sizeBySpeed;

            SpeedProperty sp = new SpeedProperty(
                cbp.enabled,
                SetupMinMaxGradient(cbp.color),
                new float[] { cbp.range.x, cbp.range.y },
                rbp.enabled,
                rbp.separateAxes,
                (rbp.separateAxes) ? new float[] { rbp.x.constant, rbp.y.constant, rbp.z.constant } :
                new float[0],
                SetupMinMaxCurve(rbp.z),
                sbp.enabled,
                sbp.separateAxes,
                (sbp.separateAxes) ? new float[] { sbp.size.constantMin, sbp.size.constantMax, sbp.y.constantMin, sbp.y.constantMax, sbp.z.constantMin, sbp.z.constantMax } :
                new float[0],
                SetupMinMaxCurve(sbp.size)
            );
            return sp;
        }

        private LifetimeProperty SetupLifetimeProperty(ParticleSystem ps) {
            var vol = ps.velocityOverLifetime;
            var lvol = ps.limitVelocityOverLifetime;
            var fol = ps.forceOverLifetime;
            var rot = ps.rotationOverLifetime;
            var col = ps.colorOverLifetime;
            var sol = ps.sizeOverLifetime;

            LifetimeProperty lp = new LifetimeProperty(
                vol.enabled,
                new float[] { vol.x.constant, vol.y.constant, vol.z.constant },
                (int)vol.space,
                lvol.enabled,
                lvol.separateAxes,
                (int)lvol.space,
                (int)lvol.limit.mode,
                (lvol.limit.mode == ParticleSystemCurveMode.TwoConstants) ?
                new float[] { lvol.limitX.constantMin, lvol.limitX.constantMax, lvol.limitY.constantMin, lvol.limitY.constantMax,
                    lvol.limitZ.constantMin, lvol.limitZ.constantMax } :
                new float[] { lvol.limitX.constant, lvol.limitY.constant, lvol.limitZ.constant },
                (lvol.limit.mode == ParticleSystemCurveMode.TwoConstants) ?
                new float[] { lvol.limit.constantMin, lvol.limit.constantMax } :
                new float[] { lvol.limit.constant },
                lvol.dampen,
                fol.enabled,
                new float[] { fol.x.constant, fol.y.constant, fol.z.constant },
                (int)fol.space,
                col.enabled,
                SetupMinMaxGradient(col.color),
                rot.enabled,
                rot.separateAxes,
                (rot.separateAxes) ? new float[] { rot.x.constant, rot.y.constant, rot.z.constant } :
                new float[0],
                SetupMinMaxCurve(rot.z),
                sol.enabled,
                sol.separateAxes,
                (sol.separateAxes) ? new float[] { sol.size.constantMin, sol.size.constantMax, sol.y.constantMin, sol.y.constantMax, sol.z.constantMin, sol.z.constantMax } :
                new float[0],
                SetupMinMaxCurve(sol.size)
            );
            return lp;
        }

        private NoiseProperty SetupNoiseProperty(ParticleSystem ps) {
            var noise = ps.noise;

            NoiseProperty np = new NoiseProperty(
                noise.enabled,
                (int)noise.quality,
                noise.separateAxes,
                (int)noise.strength.mode,
                (noise.strength.mode == ParticleSystemCurveMode.TwoConstants) ?
                new float[] { noise.strength.constantMin, noise.strength.constantMax, noise.strengthY.constantMin, noise.strengthY.constantMax,
                    noise.strengthZ.constantMin, noise.strengthZ.constantMax } :
                new float[] { noise.strengthX.constant, noise.strengthY.constant, noise.strengthZ.constant },
                (noise.strength.mode == ParticleSystemCurveMode.TwoConstants) ?
                new float[] { noise.strength.constantMin, noise.strength.constantMax } :
                new float[] { noise.strength.constant },
                noise.frequency,
                noise.damping,
                noise.scrollSpeed.constant,
                noise.octaveCount,
                noise.octaveMultiplier,
                noise.octaveScale
           );
            return np;
        }

        private EmissionProperty SetupEmissionProperty(ParticleSystem ps) {
            var emis = ps.emission;

            ParticleSystem.Burst[] bursts = new ParticleSystem.Burst[emis.burstCount];
            emis.GetBursts(bursts);
            List<Burst> lbursts = new List<Burst>();
            foreach (ParticleSystem.Burst b in bursts) {
                lbursts.Add(new Burst(b.time, b.minCount, b.maxCount, b.cycleCount, b.repeatInterval));
            }
            EmissionProperty emp = new EmissionProperty(
                emis.enabled,
                emis.rateOverTime.constant,
                emis.rateOverDistance.constant,
                lbursts.ToArray()
            );
            return emp;
        }

        private RendererProperty SetupRendererProperty(ParticleSystem ps) {
            var render = ps.gameObject.GetComponent<ParticleSystemRenderer>();

            RendererProperty rp = new RendererProperty(
                render.enabled,
                (int)render.renderMode,
                (int)render.alignment,
                render.cameraVelocityScale,
                render.velocityScale,
                render.lengthScale,
                render.normalDirection,
                (int)render.sortMode,
                render.sortingFudge,
                render.minParticleSize,
                render.maxParticleSize,
                render.pivot.x,
                render.pivot.y,
                render.pivot.z
            );
            return rp;
        }

        private ShapeProperty SetupShapeProperty(ParticleSystem ps) {
            ShapeProperty shp = new ShapeProperty(
                ps.shape.enabled,
                (int)ps.shape.shapeType,
                ps.shape.radius,
                ps.shape.alignToDirection,
                ps.shape.randomDirectionAmount,
                ps.shape.sphericalDirectionAmount,
                ps.shape.arc,
                ps.shape.angle,
                (int)ps.shape.arcMode,
                ps.shape.arcSpread,
                ps.shape.arcSpeed.constant,
                (int)ps.shape.radiusMode,
                ps.shape.radiusSpread,
                ps.shape.radiusSpeed.constant,
#if KK
                ps.shape.box.x,
                ps.shape.box.y,
                ps.shape.box.z
#elif AI || HS2
                ps.shape.scale.x,
                ps.shape.scale.y,
                ps.shape.scale.z
#endif
            );
            return shp;
        }

        private GeneralProperty SetupGeneralProperty(ParticleSystem ps) {
            GeneralProperty gnp = new GeneralProperty(
                ps.main.duration,
                ps.main.loop,
                ps.main.prewarm,
                SetupMinMaxCurve(ps.main.startDelay),
                SetupMinMaxCurve(ps.main.startLifetime),
                SetupMinMaxCurve(ps.main.startSpeed),
                SetupMinMaxCurve(ps.main.startRotation),
                ps.main.startRotation3D,
                new MinMaxCurve[3] { SetupMinMaxCurve(ps.main.startRotationX), SetupMinMaxCurve(ps.main.startRotationY), SetupMinMaxCurve(ps.main.startRotationZ) },
#if KK
                ps.main.randomizeRotationDirection,
#elif AI || HS2
                ps.main.flipRotation,
#endif
                SetupMinMaxCurve(ps.main.startSize),
                ps.main.startSize3D,
                new MinMaxCurve[3] { SetupMinMaxCurve(ps.main.startSizeX), SetupMinMaxCurve(ps.main.startSizeY), SetupMinMaxCurve(ps.main.startSizeZ) },
                SetupMinMaxGradient(ps.main.startColor)
            );
            return gnp;
        }

        private MinMaxCurve SetupMinMaxCurve(ParticleSystem.MinMaxCurve source) {
            MinMaxCurve mmc = new MinMaxCurve(
                source.mode,
                source.constant,
                source.constantMax,
                source.constantMin
            );
            return mmc;
        }

        private MinMaxGradient SetupMinMaxGradient(ParticleSystem.MinMaxGradient source) {
            PSColor[] psColors = null;

            Color c;
            GradientAlphaKey t;
            switch (source.mode) {
                case ParticleSystemGradientMode.Color:
                    psColors = new PSColor[1];

                    psColors[0] = new PSColor(source.color.r, source.color.g, source.color.b, source.color.a);
                    break;
                case ParticleSystemGradientMode.Gradient:
                    psColors = new PSColor[2];

                    c = source.gradient.colorKeys[0].color;
                    t = source.gradient.alphaKeys[0];
                    psColors[0] = new PSColor(c.r, c.g, c.b, t.alpha);

                    c = source.gradient.colorKeys[1].color;
                    t = source.gradient.alphaKeys[1];
                    psColors[1] = new PSColor(c.r, c.g, c.b, t.alpha);
                    break;
                case ParticleSystemGradientMode.TwoColors:
                    psColors = new PSColor[2];

                    psColors[0] = new PSColor(source.colorMin.r, source.colorMin.g, source.colorMin.b, source.colorMin.a);
                    psColors[1] = new PSColor(source.colorMax.r, source.colorMax.g, source.colorMax.b, source.colorMax.a);
                    break;
                case ParticleSystemGradientMode.TwoGradients:
                    psColors = new PSColor[4];

                    c = source.gradientMin.colorKeys[0].color;
                    t = source.gradientMin.alphaKeys[0];
                    psColors[0] = new PSColor(c.r, c.g, c.b, t.alpha);

                    c = source.gradientMin.colorKeys[1].color;
                    t = source.gradientMin.alphaKeys[1];
                    psColors[1] = new PSColor(c.r, c.g, c.b, t.alpha);

                    c = source.gradientMax.colorKeys[0].color;
                    t = source.gradientMax.alphaKeys[0];
                    psColors[2] = new PSColor(c.r, c.g, c.b, t.alpha);

                    c = source.gradientMax.colorKeys[1].color;
                    t = source.gradientMax.alphaKeys[1];
                    psColors[3] = new PSColor(c.r, c.g, c.b, t.alpha);
                    break;
            }

            MinMaxGradient scp = new MinMaxGradient(
                source.mode,
                psColors
            );
            return scp;
        }

        private static int GetObjectID(ObjectCtrlInfo oci) => Studio.Studio.Instance.dicObjectCtrl.First(x => x.Value == oci).Key;
    }
}