using System;
using UnityEngine;

namespace RSkoi_ParticleEditor {
    partial class ParticleEditor {
        private static void SetNoiseProperty(ParticleSystem.NoiseModule noise, NoiseProperty np) {
            noise.enabled = np.Enabled;
            noise.quality = (ParticleSystemNoiseQuality)np.Quality;
            noise.separateAxes = np.SeparateAxes;
            var strength = noise.strength;
            strength.mode = (ParticleSystemCurveMode)np.StrengthMode;
            if (noise.separateAxes) {
                if (strength.mode == ParticleSystemCurveMode.TwoConstants) {
					strength.constantMin = np.StrengthXYZ[0];
                    strength.constantMax = np.StrengthXYZ[1];

                    var sy = noise.strengthY;
					sy.constantMin = np.StrengthXYZ[2];
                    sy.constantMax = np.StrengthXYZ[3];
                    noise.strengthY = sy;

                    var sz = noise.strengthZ;
					sz.constantMin = np.StrengthXYZ[4];
                    sz.constantMax = np.StrengthXYZ[5];
                    noise.strengthZ = sz;
				} else {
                    strength.constant = np.StrengthXYZ[0];

                    var sy = noise.strengthY;
                    sy.constant = np.StrengthXYZ[1];
                    noise.strengthY = sy;

                    var sz = noise.strengthZ;
                    sz.constant = np.StrengthXYZ[2];
                    noise.strengthZ = sz;
                }
            } else {
                if (strength.mode == ParticleSystemCurveMode.TwoConstants) {
                    strength.constantMin = np.StrengthConst[0];
                    strength.constantMax = np.StrengthConst[1];
                } else {
                    strength.constant = np.StrengthConst[0];
                }
            }
            noise.strength = strength;
            noise.frequency = np.Frequency;
            noise.damping = np.Damping;
            var scrollSpeed = noise.scrollSpeed;
            scrollSpeed.constant = np.ScrollSpeed;
            noise.scrollSpeed = scrollSpeed;
            noise.octaveCount = np.Octaves;
            noise.octaveMultiplier = np.OctaveMult;
            noise.octaveScale = np.OctaveScale;
        }
    }
}