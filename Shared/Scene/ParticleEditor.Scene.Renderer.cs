using System;
using UnityEngine;

namespace RSkoi_ParticleEditor {
    partial class ParticleEditor {
        private static void SetRendererProperty(ParticleSystem ps, RendererProperty rp) {
            var render = ps.gameObject.GetComponent<ParticleSystemRenderer>();

            render.enabled = rp.Enabled;
            render.renderMode = (ParticleSystemRenderMode)rp.RenderMode;
            render.alignment = (ParticleSystemRenderSpace)rp.BillboardAlignment;
            render.cameraVelocityScale = rp.CameraScale;
            render.lengthScale = rp.LengthScale;
            render.velocityScale = rp.SpeedScale;
            render.normalDirection = rp.NormalDirection;
            render.sortMode = (ParticleSystemSortMode)rp.SortMode;
            render.sortingFudge = rp.SortingFudge;
            render.minParticleSize = rp.MinSize;
            render.maxParticleSize = rp.MaxSize;
            var pivot = render.pivot;
            pivot.x = rp.PivotX;
            pivot.y = rp.PivotY;
            pivot.z = rp.PivotZ;
            render.pivot = pivot;
        }
    }
}
