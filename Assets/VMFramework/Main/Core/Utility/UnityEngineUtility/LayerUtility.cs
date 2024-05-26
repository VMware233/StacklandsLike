using System.Runtime.CompilerServices;
using UnityEngine;

public static class LayerUtility
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ToLayerMaskInt(this int layerIndex)
    {
        return 1 << layerIndex;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LayerMask ToLayerMask(this int layerIndex)
    {
        return 1 << layerIndex;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LayerMask AddLayer(this LayerMask layerMask, string layerName)
    {
        return layerMask | (1 << LayerMask.NameToLayer(layerName));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LayerMask AddLayer(this LayerMask layerMask, int layerIndex)
    {
        return layerMask | (1 << layerIndex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LayerMask RemoveLayer(this LayerMask layerMask, string layerName)
    {
        return layerMask & ~(1 << LayerMask.NameToLayer(layerName));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LayerMask RemoveLayer(this LayerMask layerMask, int layerIndex)
    {
        return layerMask & ~(1 << layerIndex);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLayerRef(this ref LayerMask layerMask, string layerName)
    {
        layerMask.value |= 1 << LayerMask.NameToLayer(layerName);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddLayerRef(this ref LayerMask layerMask, int layerIndex)
    {
        layerMask.value |= 1 << layerIndex;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void RemoveLayerRef(this ref LayerMask layerMask, string layerName)
    {
        layerMask.value &= ~(1 << LayerMask.NameToLayer(layerName));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void RemoveLayerRef(this ref LayerMask layerMask, int layerIndex)
    {
        layerMask.value &= ~(1 << layerIndex);
    }
}
