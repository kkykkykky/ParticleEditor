using System;
using UnityEngine;

namespace RSkoi_ParticleEditor {
    partial class ParticleEditor {
        private static void SetLifetimeProperty(ParticleSystem ps, LifetimeProperty lp) { // don't ask, I don't know why any of this works (or doesn't) either...
            var vol = ps.velocityOverLifetime;
            var lvol = ps.limitVelocityOverLifetime;
            var fol = ps.forceOverLifetime;
            var rot = ps.rotationOverLifetime;
            var col = ps.colorOverLifetime;
            var sol = ps.sizeOverLifetime;

            vol.enabled = lp.VelocityEnabled;
            var lX = vol.x;
            lX.constant = lp.VelocityXYZ[0];
            vol.x = lX;
            var lY = vol.y;
            lY.constant = lp.VelocityXYZ[1];
            vol.y = lY;
            var lZ = vol.z;
            lZ.constant = lp.VelocityXYZ[2];
            vol.z = lZ;
            vol.space = (ParticleSystemSimulationSpace)lp.VelocitySpace;

            lvol.enabled = lp.LimitVelocityEnabled;
            lvol.separateAxes = lp.LimitVelocitySeparateAxes;
            lvol.dampen = lp.LimitVelocityDampen;
            lvol.space = (ParticleSystemSimulationSpace)lp.LimitVelocitySpace;
            var limit = lvol.limit;
            limit.mode = (ParticleSystemCurveMode)lp.LimitVelocityMode;
            if (lvol.separateAxes) {
                if (limit.mode == ParticleSystemCurveMode.TwoConstants) {
                    /*lX = lvol.limitX;
                    lX.constantMin = lp.LimitVelocityXYZ[0];
                    lX.constantMax = lp.LimitVelocityXYZ[1];
                    lvol.limitX = lX;*/
                    lvol.limitX = SetMinMaxCurve(lvol.limitX, new MinMaxCurve(ParticleSystemCurveMode.TwoConstants, 0, lp.LimitVelocityXYZ[1], lp.LimitVelocityXYZ[0]));

                    /*lY = lvol.limitY;
                    lY.constantMin = lp.LimitVelocityXYZ[2];
                    lY.constantMax = lp.LimitVelocityXYZ[3];
                    lvol.limitY = lY;*/
                    lvol.limitY = SetMinMaxCurve(lvol.limitY, new MinMaxCurve(ParticleSystemCurveMode.TwoConstants, 0, lp.LimitVelocityXYZ[3], lp.LimitVelocityXYZ[2]));

                    /*lZ = lvol.limitZ;
                    lZ.constantMin = lp.LimitVelocityXYZ[4];
                    lZ.constantMax = lp.LimitVelocityXYZ[5];
                    lvol.limitZ = lZ;*/
                    lvol.limitZ = SetMinMaxCurve(lvol.limitZ, new MinMaxCurve(ParticleSystemCurveMode.TwoConstants, 0, lp.LimitVelocityXYZ[5], lp.LimitVelocityXYZ[4]));
                } else {
                    /*lX = lvol.limitX;
                    lX.constant = lp.LimitVelocityXYZ[0];
                    lvol.limitX = lX;*/
                    lvol.limitX = SetMinMaxCurve(lvol.limitX, new MinMaxCurve(ParticleSystemCurveMode.Constant, lp.LimitVelocityXYZ[0], lp.LimitVelocityXYZ[0], 0));

                    /*lY = lvol.limitY;
                    lY.constant = lp.LimitVelocityXYZ[1];
                    lvol.limitY = lY;*/
                    lvol.limitY = SetMinMaxCurve(lvol.limitY, new MinMaxCurve(ParticleSystemCurveMode.Constant, lp.LimitVelocityXYZ[1], lp.LimitVelocityXYZ[1], 0));

                    /*lZ = lvol.limitZ;
                    lZ.constant = lp.LimitVelocityXYZ[2];
                    lvol.limitZ = lZ;*/
                    lvol.limitZ = SetMinMaxCurve(lvol.limitZ, new MinMaxCurve(ParticleSystemCurveMode.Constant, lp.LimitVelocityXYZ[2], lp.LimitVelocityXYZ[2], 0));
                }
            } else {
                //limit = SetMinMaxCurve(limit, new MinMaxCurve(limit.mode, lp.LimitVelocityConst[0], (limit.mode == ParticleSystemCurveMode.TwoConstants) ? lp.LimitVelocityConst[0] : 0, lp.LimitVelocityConst[0]));
                if (limit.mode == ParticleSystemCurveMode.TwoConstants) {
                    limit.constantMin = lp.LimitVelocityConst[0];
                    limit.constantMax = lp.LimitVelocityConst[1];
                } else {
                    limit.constant = lp.LimitVelocityConst[0];
                }
            }
            lvol.limit = limit;

            fol.enabled = lp.VelocityEnabled;
            lX = fol.x;
            lX.constant = lp.ForceXYZ[0];
            fol.x = lX;
            lY = fol.y;
            lY.constant = lp.ForceXYZ[1];
            fol.y = lY;
            lZ = fol.z;
            lZ.constant = lp.ForceXYZ[2];
            fol.z = lZ;
            fol.space = (ParticleSystemSimulationSpace)lp.ForceSpace;

            col.enabled = lp.ColorEnabled;
            #region Color
            ParticleSystem.MinMaxGradient startColor = new ParticleSystem.MinMaxGradient();
            MinMaxGradient mng = lp.ColorGradient;
            startColor.mode = mng.Mode;

            GradientColorKey[] colors;
            GradientAlphaKey[] transparents;
            switch (mng.Mode) {
                case ParticleSystemGradientMode.Gradient:
                    colors = new GradientColorKey[2];
                    transparents = new GradientAlphaKey[2];

                    colors[0] = new GradientColorKey(new Color(
                        mng.Colors[0].R,
                        mng.Colors[0].G,
                        mng.Colors[0].B
                    ), 0F);

                    colors[1] = new GradientColorKey(new Color(
                        mng.Colors[1].R,
                        mng.Colors[1].G,
                        mng.Colors[1].B
                    ), 1F);

                    transparents[0] = new GradientAlphaKey(mng.Colors[0].A, 0F);
                    transparents[1] = new GradientAlphaKey(mng.Colors[1].A, 1F);

                    startColor.gradient = new Gradient {
                        colorKeys = colors,
                        alphaKeys = transparents
                    };
                    break;
                case ParticleSystemGradientMode.TwoGradients:
                    colors = new GradientColorKey[2];
                    transparents = new GradientAlphaKey[2];

                    colors[0] = new GradientColorKey(new Color(
                        mng.Colors[0].R,
                        mng.Colors[0].G,
                        mng.Colors[0].B
                    ), 0F);

                    colors[1] = new GradientColorKey(new Color(
                        mng.Colors[1].R,
                        mng.Colors[1].G,
                        mng.Colors[1].B
                    ), 1F);

                    transparents[0] = new GradientAlphaKey(mng.Colors[0].A, 0F);
                    transparents[1] = new GradientAlphaKey(mng.Colors[1].A, 1F);

                    startColor.gradientMin = new Gradient {
                        colorKeys = colors,
                        alphaKeys = transparents
                    };


                    colors = new GradientColorKey[2];
                    transparents = new GradientAlphaKey[2];

                    colors[0] = new GradientColorKey(new Color(
                        mng.Colors[2].R,
                        mng.Colors[2].G,
                        mng.Colors[2].B
                    ), 0F);

                    colors[1] = new GradientColorKey(new Color(
                        mng.Colors[3].R,
                        mng.Colors[3].G,
                        mng.Colors[3].B
                    ), 1F);

                    transparents[0] = new GradientAlphaKey(mng.Colors[2].A, 0F);
                    transparents[1] = new GradientAlphaKey(mng.Colors[3].A, 1F);

                    startColor.gradientMax = new Gradient {
                        colorKeys = colors,
                        alphaKeys = transparents
                    };
                    break;
            }
            col.color = startColor;
            #endregion Color

            rot.enabled = lp.RotationEnabled;
            rot.separateAxes = lp.RotationSeparateAxes;
            if (rot.separateAxes) {
                lX = rot.x;
                lX.constant = lp.RotationXYZ[0];
                rot.x = lX;
                lY = rot.y;
                lY.constant = lp.RotationXYZ[1];
                rot.y = lY;
                lZ = rot.z;
                lZ.constant = lp.RotationXYZ[2];
                rot.z = lZ;
            } else {
                lZ = rot.z;
                lZ.constant = lp.RotationConst.Constant;
                rot.z = lZ;
            }
            
            sol.enabled = lp.SizeEnabled;
            sol.separateAxes = lp.SizeSeparateAxes;
            var size = sol.size;
            size.mode = lp.SizeConst.Mode;
            if (sol.separateAxes) {
                if (size.mode == ParticleSystemCurveMode.TwoConstants) {
                    size.constantMin = lp.SizeXYZ[0];
                    size.constantMax = lp.SizeXYZ[1];
                    lY = sol.y;
                    lY.constantMin = lp.SizeXYZ[2];
                    lY.constantMax = lp.SizeXYZ[3];
                    sol.y = lY;
                    lZ = sol.z;
                    lZ.constantMin = lp.SizeXYZ[4];
                    lZ.constantMax = lp.SizeXYZ[5];
                    sol.z = lZ;
                }
            } else {
                if (size.mode == ParticleSystemCurveMode.TwoConstants) {
                    size.constantMin = lp.SizeConst.ConstantMin;
                    size.constantMax = lp.SizeConst.ConstantMax;
                }
            }
            sol.size = size;
        }
    }
}