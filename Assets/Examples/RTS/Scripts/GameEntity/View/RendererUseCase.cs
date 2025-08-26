using System.Collections.Generic;
using UnityEngine;

namespace RTSGame
{
    public static class RendererUseCase
    {
        public static void SetMaterial(IEnumerable<Renderer> renderers, Material material)
        {
            foreach (Renderer renderer in renderers)
                renderer.material = material;
        }
    }
}