using System;
using UnityEngine;

namespace RSkoi_ParticleEditor {
    partial class ParticleEditor {
        private static void SetSpeedProperty(ParticleSystem ps, SpeedProperty sp) {
            var cbp = ps.colorBySpeed;
            var rbp = ps.rotationBySpeed;
            var sbp = ps.sizeBySpeed;

            cbp.enabled = sp.ColorEnabled;
            #region Color
            ParticleSystem.MinMaxGradient startColor = new ParticleSystem.MinMaxGradient();
            MinMaxGradient mng = sp.ColorGradient;
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
            var range = cbp.range;
            range.x = sp.ColorRangeXY[0];
            range.y = sp.ColorRangeXY[1];
            cbp.range = range;
            cbp.color = startColor;
            #endregion Color

            var lX = sbp.x;
            var lY = sbp.y;
            var lZ = sbp.z;
            rbp.enabled = sp.RotationEnabled;
            rbp.separateAxes = sp.RotationSeparateAxes;
            if (rbp.separateAxes) {
                lX = rbp.x;
                lX.constant = sp.RotationXYZ[0];
                rbp.x = lX;
                lY = rbp.y;
                lY.constant = sp.RotationXYZ[1];
                rbp.y = lY;
                lZ = rbp.z;
                lZ.constant = sp.RotationXYZ[2];
                rbp.z = lZ;
            } else {
                lZ = rbp.z;
                lZ.constant = sp.RotationConst.Constant;
                rbp.z = lZ;
            }

            sbp.enabled = sp.SizeEnabled;
            sbp.separateAxes = sp.SizeSeparateAxes;
            var size = sbp.size;
            size.mode = sp.SizeConst.Mode;
            if (sbp.separateAxes) {
                if (size.mode == ParticleSystemCurveMode.TwoConstants) {
                    size.constantMin = sp.SizeXYZ[0];
                    size.constantMax = sp.SizeXYZ[1];
                    lY = sbp.y;
                    lY.constantMin = sp.SizeXYZ[2];
                    lY.constantMax = sp.SizeXYZ[3];
                    sbp.y = lY;
                    lZ = sbp.z;
                    lZ.constantMin = sp.SizeXYZ[4];
                    lZ.constantMax = sp.SizeXYZ[5];
                    sbp.z = lZ;
                }
            } else {
                if (size.mode == ParticleSystemCurveMode.TwoConstants) {
                    size.constantMin = sp.SizeConst.ConstantMin;
                    size.constantMax = sp.SizeConst.ConstantMax;
                }
            }
            sbp.size = size;
        }
    }
}