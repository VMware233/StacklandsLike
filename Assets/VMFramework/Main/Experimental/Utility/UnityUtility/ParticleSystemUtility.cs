using System.Runtime.CompilerServices;
using UnityEngine;

public static class ParticleSystemUtility
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SetSampleTexture(this ParticleSystem particleSystem, Texture2D texture)
    {
        var shapeModule = particleSystem.shape;
        shapeModule.texture = texture;
    }
}
