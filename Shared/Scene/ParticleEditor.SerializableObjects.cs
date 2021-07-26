using System;
using MessagePack;
using UnityEngine;

namespace RSkoi_ParticleEditor {
    public partial class ParticleEditor {
        [Serializable]
        [MessagePackObject]
        public class PSProperty {
            [Key("ID")]
            public int ID;
            [Key("Time")]
            public float Time;
            [Key("Paused")]
            public bool Paused;
            [Key("Seed")]
            public uint Seed;
            [Key("AutoSeed")]
            public bool AutoSeed;
            [Key("Speed")]
            public float Speed;
            [Key("Children")]
            public PSProperty[] Children;
            [Key("General")]
            public GeneralProperty General;
            [Key("Shape")]
            public ShapeProperty Shape;
            [Key("Renderer")]
            public RendererProperty Renderer;
            [Key("Emission")]
            public EmissionProperty Emission;
            [Key("Noise")]
            public NoiseProperty Noise;
            [Key("Lifetime")]
            public LifetimeProperty Lifetime;
            [Key("SpeedP")]
            public SpeedProperty SpeedP;

            public PSProperty(int id, float time, bool paused, uint seed, bool autoseed, float speed, PSProperty[] children, GeneralProperty general,
                ShapeProperty shape, RendererProperty renderer, EmissionProperty emission, NoiseProperty noise, LifetimeProperty lifetime, SpeedProperty speedP) {
                ID = id;
                Time = time;
                Paused = paused;
                Seed = seed;
                AutoSeed = autoseed;
                Speed = speed;
                Children = children;
                General = general;
                Shape = shape;
                Renderer = renderer;
                Emission = emission;
                Noise = noise;
                Lifetime = lifetime;
                SpeedP = speedP;
            }
        }

        [Serializable]
        [MessagePackObject]
        public class SpeedProperty {
            [Key("ColorEnabled")]
            public bool ColorEnabled;
            [Key("ColorGradient")]
            public MinMaxGradient ColorGradient;
            [Key("ColorRangeXY")]
            public float[] ColorRangeXY;

            [Key("RotationEnabled")]
            public bool RotationEnabled;
            [Key("RotationSeparateAxes")]
            public bool RotationSeparateAxes;
            [Key("RotationXYZ")]
            public float[] RotationXYZ;
            [Key("RotationConst")]
            public MinMaxCurve RotationConst;

            [Key("SizeEnabled")]
            public bool SizeEnabled;
            [Key("SizeSeparateAxes")]
            public bool SizeSeparateAxes;
            [Key("SizeXYZ")]
            public float[] SizeXYZ;
            [Key("SizeConst")]
            public MinMaxCurve SizeConst;

            public SpeedProperty(bool colorEnabled, MinMaxGradient colorGradient, float[] colorRangeXY, bool rotationEnabled, bool rotationSeparateAxes, float[] rotationXYZ,
                MinMaxCurve rotationConst, bool sizeEnabled, bool sizeSeparateAxes, float[] sizeXYZ, MinMaxCurve sizeConst) {
                ColorEnabled = colorEnabled;
                ColorGradient = colorGradient;
                ColorRangeXY = colorRangeXY;
                RotationEnabled = rotationEnabled;
                RotationSeparateAxes = rotationSeparateAxes;
                RotationXYZ = rotationXYZ;
                RotationConst = rotationConst;
                SizeEnabled = sizeEnabled;
                SizeSeparateAxes = sizeSeparateAxes;
                SizeXYZ = sizeXYZ;
                SizeConst = sizeConst;
            }
        }

        [Serializable]
        [MessagePackObject]
        public class LifetimeProperty {
            [Key("VelocityEnabled")]
            public bool VelocityEnabled;
            [Key("VelocityXYZ")]
            public float[] VelocityXYZ;
            [Key("VelocitySpace")]
            public int VelocitySpace;

            [Key("LimitVelocityEnabled")]
            public bool LimitVelocityEnabled;
            [Key("LimitVelocitySeparateAxes")]
            public bool LimitVelocitySeparateAxes;
            [Key("LimitVelocitySpace")]
            public int LimitVelocitySpace;
            [Key("LimitVelocityMode")]
            public int LimitVelocityMode;
            [Key("LimitVelocityXYZ")]
            public float[] LimitVelocityXYZ;
            [Key("LimitVelocityConst")]
            public float[] LimitVelocityConst;
            [Key("LimitVelocityDampen")]
            public float LimitVelocityDampen;

            [Key("ForceEnabled")]
            public bool ForceEnabled;
            [Key("ForceXYZ")]
            public float[] ForceXYZ;
            [Key("ForceSpace")]
            public int ForceSpace;

            [Key("ColorEnabled")]
            public bool ColorEnabled;
            [Key("ColorGradient")]
            public MinMaxGradient ColorGradient;

            [Key("RotationEnabled")]
            public bool RotationEnabled;
            [Key("RotationSeparateAxes")]
            public bool RotationSeparateAxes;
            [Key("RotationXYZ")]
            public float[] RotationXYZ;
            [Key("RotationConst")]
            public MinMaxCurve RotationConst;

            [Key("SizeEnabled")]
            public bool SizeEnabled;
            [Key("SizeSeparateAxes")]
            public bool SizeSeparateAxes;
            [Key("SizeXYZ")]
            public float[] SizeXYZ;
            [Key("SizeConst")]
            public MinMaxCurve SizeConst;

            public LifetimeProperty(bool velocityEnabled, float[] velocityXYZ, int velocitySpace, bool limitVelocityEnabled, bool limitVelocitySeparateAxes,
                int limitVelocitySpace, int limitVelocityMode, float[] limitVelocityXYZ, float[] limitVelocityConst, float limitVelocityDampen, bool forceEnabled,
                float[] forceXYZ, int forceSpace, bool colorEnabled, MinMaxGradient colorGradient, bool rotationEnabled, bool rotationSeparateAxes, float[] rotationXYZ,
                MinMaxCurve rotationConst, bool sizeEnabled, bool sizeSeparateAxes, float[] sizeXYZ, MinMaxCurve sizeConst) {
                VelocityEnabled = velocityEnabled;
                VelocityXYZ = velocityXYZ;
                VelocitySpace = velocitySpace;
                LimitVelocityEnabled = limitVelocityEnabled;
                LimitVelocitySeparateAxes = limitVelocitySeparateAxes;
                LimitVelocitySpace = limitVelocitySpace;
                LimitVelocityMode = limitVelocityMode;
                LimitVelocityXYZ = limitVelocityXYZ;
                LimitVelocityConst = limitVelocityConst;
                LimitVelocityDampen = limitVelocityDampen;
                ForceEnabled = forceEnabled;
                ForceXYZ = forceXYZ;
                ForceSpace = forceSpace;
                ColorEnabled = colorEnabled;
                ColorGradient = colorGradient;
                RotationEnabled = rotationEnabled;
                RotationSeparateAxes = rotationSeparateAxes;
                RotationXYZ = rotationXYZ;
                RotationConst = rotationConst;
                SizeEnabled = sizeEnabled;
                SizeSeparateAxes = sizeSeparateAxes;
                SizeXYZ = sizeXYZ;
                SizeConst = sizeConst;
            }
        }

        [Serializable]
        [MessagePackObject]
        public class NoiseProperty {
            [Key("Enabled")]
            public bool Enabled;
            [Key("Quality")]
            public int Quality;
            [Key("SeparateAxes")]
            public bool SeparateAxes;
            [Key("StrengthMode")]
            public int StrengthMode;
            [Key("StrengthXYZ")]
            public float[] StrengthXYZ;
            [Key("StrengthConst")]
            public float[] StrengthConst;
            [Key("Frequency")]
            public float Frequency;
            [Key("Damping")]
            public bool Damping;
            [Key("ScrollSpeed")]
            public float ScrollSpeed;
            [Key("Octaves")]
            public int Octaves;
            [Key("OctaveMult")]
            public float OctaveMult;
            [Key("OctaveScale")]
            public float OctaveScale;

            public NoiseProperty(bool enabled, int quality, bool separateAxes, int strengthMode, float[] strengthXYZ, float[] strengthConst,
                float frequency, bool damping, float scrollSpeed, int octaves, float octaveMult, float octaveScale) {
                Enabled = enabled;
                Quality = quality;
                SeparateAxes = separateAxes;
                StrengthMode = strengthMode;
                StrengthXYZ = strengthXYZ;
                StrengthConst = strengthConst;
                Frequency = frequency;
                Damping = damping;
                ScrollSpeed = scrollSpeed;
                Octaves = octaves;
                OctaveMult = octaveMult;
                OctaveScale = octaveScale;
            }
        }

        [Serializable]
        [MessagePackObject]
        public class EmissionProperty {
            [Key("Enabled")]
            public bool Enabled;
            [Key("RateOTime")]
            public float RateOTime;
            [Key("RateODist")]
            public float RateODist;
            [Key("Bursts")]
            public Burst[] Bursts;

            public EmissionProperty(bool enabled, float rateOTime, float rateODist, Burst[] bursts) {
                Enabled = enabled;
                RateOTime = rateOTime;
                RateODist = rateODist;
                Bursts = bursts;
            }
        }

        [Serializable]
        [MessagePackObject]
        public class Burst {
            [Key("Time")]
            public float Time;
            [Key("Min")]
            public short Min;
            [Key("Max")]
            public short Max;
            [Key("Cycles")]
            public int Cycles;
            [Key("Interval")]
            public float Interval;

            public Burst(float time, short min, short max, int cycles, float interval) {
                Time = time;
                Min = min;
                Max = max;
                Cycles = cycles;
                Interval = interval;
            }
        }

        [Serializable]
        [MessagePackObject]
        public class RendererProperty {
            [Key("Enabled")]
            public bool Enabled;
            [Key("RenderMode")]
            public int RenderMode;
            [Key("BillboardAlignment")]
            public int BillboardAlignment;
            [Key("CameraScale")]
            public float CameraScale;
            [Key("SpeedScale")]
            public float SpeedScale;
            [Key("LengthScale")]
            public float LengthScale;
            [Key("NormalDirection")]
            public float NormalDirection;
            [Key("SortMode")]
            public int SortMode;
            [Key("SortingFudge")]
            public float SortingFudge;
            [Key("MinSize")]
            public float MinSize;
            [Key("MaxSize")]
            public float MaxSize;
            [Key("PivotX")]
            public float PivotX;
            [Key("PivotY")]
            public float PivotY;
            [Key("PivotZ")]
            public float PivotZ;

            public RendererProperty(bool enabled, int renderMode, int billboardAlignment, float cameraScale, float speedScale, float lengthScale,
                float normalDirection, int sortMode, float sortingFudge, float minSize, float maxSize, float pivotX, float pivotY, float pivotZ) {
                Enabled = enabled;
                RenderMode = renderMode;
                BillboardAlignment = billboardAlignment;
                CameraScale = cameraScale;
                SpeedScale = speedScale;
                LengthScale = lengthScale;
                NormalDirection = normalDirection;
                SortMode = sortMode;
                SortingFudge = sortingFudge;
                MinSize = minSize;
                MaxSize = maxSize;
                PivotX = pivotX;
                PivotY = pivotY;
                PivotZ = pivotZ;
            }
        }

        [Serializable]
        [MessagePackObject]
        public class ShapeProperty {
            [Key("Enabled")]
            public bool Enabled;
            [Key("Mode")]
            public int Mode;
            [Key("Radius")]
            public float Radius;
            [Key("AlignDirection")]
            public bool AlignDirection;
            [Key("RandomDirAmount")]
            public float RandomDirAmount;
            [Key("SphereDirAmount")]
            public float SphereDirAmount;
            [Key("Arc")]
            public float Arc;
            [Key("ArcAngle")]
            public float ArcAngle;
            [Key("ArcMode")]
            public int ArcMode;
            [Key("ArcSpread")]
            public float ArcSpread;
            [Key("ArcSpeed")]
            public float ArcSpeed;
            [Key("RadiusMode")]
            public int RadiusMode;
            [Key("RadiusSpread")]
            public float RadiusSpread;
            [Key("RadiusSpeed")]
            public float RadiusSpeed;
            [Key("BoxX")]
            public float BoxX;
            [Key("BoxY")]
            public float BoxY;
            [Key("BoxZ")]
            public float BoxZ;

            public ShapeProperty(bool enabled, int mode, float radius, bool alignDirection, float randomDirAmount, float sphereDirAmount, float arc,
                float arcAngle, int arcMode, float arcSpread, float arcSpeed, int radiusMode, float radiusSpread, float radiusSpeed, float boxX, float boxY,
                float boxZ) {
                Enabled = enabled;
                Mode = mode;
                Radius = radius;
                AlignDirection = alignDirection;
                RandomDirAmount = randomDirAmount;
                SphereDirAmount = sphereDirAmount;
                Arc = arc;
                ArcAngle = arcAngle;
                ArcMode = arcMode;
                ArcSpread = arcSpread;
                ArcSpeed = arcSpeed;
                RadiusMode = radiusMode;
                RadiusSpread = radiusSpread;
                RadiusSpeed = radiusSpeed;
                BoxX = boxX;
                BoxY = boxY;
                BoxZ = boxZ;
            }
        }

        [Serializable]
        [MessagePackObject]
        public class GeneralProperty {
            [Key("Duration")]
            public float Duration;
            [Key("Loop")]
            public bool Loop;
            [Key("Prewarm")]
            public bool Prewarm;
            [Key("Delay")]
            public MinMaxCurve Delay;
            [Key("StartLifetime")]
            public MinMaxCurve StartLifetime;
            [Key("StartSpeed")]
            public MinMaxCurve StartSpeed;
            [Key("StartRotation")]
            public MinMaxCurve StartRotation;
            [Key("Rotation3D")]
            public bool Rotation3D;
            [Key("RotationXYZ")]
            public MinMaxCurve[] RotationXYZ;
            [Key("RandomizeRotation")]
            public float RandomizeRotation;
            [Key("StartSize")]
            public MinMaxCurve StartSize;
            [Key("StartSize3D")]
            public bool StartSize3D;
            [Key("StartSizeXYZ")]
            public MinMaxCurve[] StartSizeXYZ;
            [Key("StartColor")]
            public MinMaxGradient StartColor;

            public GeneralProperty(float duration, bool loop, bool prewarm, MinMaxCurve delay, MinMaxCurve startLifetime, MinMaxCurve startSpeed,
                MinMaxCurve startRotation, bool rotation3D, MinMaxCurve[] rotationXYZ, float randomizeRotation, MinMaxCurve startSize, bool startSize3D,
                MinMaxCurve[] startSizeXYZ, MinMaxGradient startColor) {
                Duration = duration;
                Loop = loop;
                Prewarm = prewarm;
                Delay = delay;
                StartLifetime = startLifetime;
                StartSpeed = startSpeed;
                Rotation3D = rotation3D;
                RotationXYZ = rotationXYZ;
                StartRotation = startRotation;
                RandomizeRotation = randomizeRotation;
                StartSize = startSize;
                StartSize3D = startSize3D;
                StartSizeXYZ = startSizeXYZ;
                StartColor = startColor;
            }
        }

        [Serializable]
        [MessagePackObject]
        public class MinMaxCurve {
            [Key("Mode")]
            public ParticleSystemCurveMode Mode;
            [Key("Constant")]
            public float Constant;
            [Key("ConstantMax")]
            public float ConstantMax;
            [Key("ConstantMin")]
            public float ConstantMin;

            public MinMaxCurve(ParticleSystemCurveMode mode, float constant, float constantMax, float constantMin) {
                Mode = mode;
                Constant = constant;
                ConstantMax = constantMax;
                ConstantMin = constantMin;
            }
        }

        [Serializable]
        [MessagePackObject]
        public class MinMaxGradient {
            [Key("Mode")]
            public ParticleSystemGradientMode Mode;
            [Key("Colors")]
            public PSColor[] Colors;

            public MinMaxGradient(ParticleSystemGradientMode mode, PSColor[] colors) {
                Mode = mode;
                Colors = colors;
            }
        }

        [Serializable]
        [MessagePackObject]
        public class PSColor {
            [Key("R")]
            public float R;
            [Key("G")]
            public float G;
            [Key("B")]
            public float B;
            [Key("A")]
            public float A;

            public PSColor(float r, float g, float b, float a) {
                R = r;
                G = g;
                B = b;
                A = a;
            }
        }
    }
}