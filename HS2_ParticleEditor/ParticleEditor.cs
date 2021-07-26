using System;
using BepInEx;

namespace RSkoi_ParticleEditor
{
    [BepInProcess("StudioNEOV2.exe")]
    public partial class ParticleEditor : BaseUnityPlugin
    {
        private const string Game = "HS2 ";
    }
}
