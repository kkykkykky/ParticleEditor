using System;
using UnityEngine;

namespace RSkoi_ParticleEditor {
    partial class ParticleEditor {
        private static void SetGeneralProperty(ParticleSystem.MainModule main, GeneralProperty gnp) {
            main.duration = gnp.Duration;
            main.loop = gnp.Loop;
            main.prewarm = gnp.Prewarm;

            /*SetMinMaxCurve(main.startDelay, gnp.Delay);
            SetMinMaxCurve(main.startLifetime, gnp.StartLifetime);
            SetMinMaxCurve(main.startSpeed, gnp.StartSpeed);
            SetMinMaxCurve(main.startRotation, gnp.StartRotation);
            main.randomizeRotationDirection = gnp.RandomizeRotation;
            SetMinMaxCurve(main.startSize, gnp.StartSize);*/

            main.startDelay = SetMinMaxCurve(main.startDelay, gnp.Delay);
            main.startLifetime = SetMinMaxCurve(main.startLifetime, gnp.StartLifetime);
            main.startSpeed = SetMinMaxCurve(main.startSpeed, gnp.StartSpeed);
            main.startRotation = SetMinMaxCurve(main.startRotation, gnp.StartRotation);
            main.startRotation3D = gnp.Rotation3D;
            main.startRotationX = SetMinMaxCurve(main.startRotationX, gnp.RotationXYZ[0]);
            main.startRotationY = SetMinMaxCurve(main.startRotationY, gnp.RotationXYZ[1]);
            main.startRotationZ = SetMinMaxCurve(main.startRotationZ, gnp.RotationXYZ[2]);
#if KK
            main.randomizeRotationDirection = gnp.RandomizeRotation;
#elif AI || HS2
            main.flipRotation = gnp.RandomizeRotation;
#endif
            main.startSize = SetMinMaxCurve(main.startSize, gnp.StartSize);
            main.startSize3D = gnp.StartSize3D;
            main.startSizeX = SetMinMaxCurve(main.startSizeX, gnp.StartSizeXYZ[0]);
            main.startSizeY = SetMinMaxCurve(main.startSizeY, gnp.StartSizeXYZ[1]);
            main.startSizeZ = SetMinMaxCurve(main.startSizeZ, gnp.StartSizeXYZ[2]);

#region StartColor
            ParticleSystem.MinMaxGradient startColor = new ParticleSystem.MinMaxGradient();
            MinMaxGradient mng = gnp.StartColor;
            startColor.mode = mng.Mode;

            GradientColorKey[] colors;
            GradientAlphaKey[] transparents;
            switch (mng.Mode) {
                case ParticleSystemGradientMode.Color:
                    startColor.color = new Color(
                        mng.Colors[0].R,
                        mng.Colors[0].G,
                        mng.Colors[0].B,
                        mng.Colors[0].A
                    );
                    break;
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
                case ParticleSystemGradientMode.TwoColors:
                    startColor.colorMin = new Color(
                        mng.Colors[0].R,
                        mng.Colors[0].G,
                        mng.Colors[0].B,
                        mng.Colors[0].A
                    );

                    startColor.colorMax = new Color(
                        mng.Colors[1].R,
                        mng.Colors[1].G,
                        mng.Colors[1].B,
                        mng.Colors[1].A
                    );
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

            main.startColor = startColor;
#endregion StartColor
        }

        private static ParticleSystem.MinMaxCurve SetMinMaxCurve(ParticleSystem.MinMaxCurve source, MinMaxCurve mmc) {
            ParticleSystem.MinMaxCurve newMmc = new ParticleSystem.MinMaxCurve {
                mode = mmc.Mode,
                constant = mmc.Constant,
                constantMax = mmc.ConstantMax,
                constantMin = mmc.ConstantMin,

                // non-saved properties
                curve = source.curve,
                curveMax = source.curveMax,
                curveMin = source.curveMin,
                curveMultiplier = source.curveMultiplier,

#if KK
#pragma warning disable CS0618 // Type or member is obsolete
                curveScalar = source.curveScalar
#pragma warning restore CS0618 // Type or member is obsolete
#endif
            };

            return newMmc;

            // this doesn't seem to work...
            /*source.mode = mmc.Mode,
            source.constant = mmc.Constant,
            source.constantMax = mmc.ConstantMax,
            source.constantMin = mmc.ConstantMin*/
        }
    }
}