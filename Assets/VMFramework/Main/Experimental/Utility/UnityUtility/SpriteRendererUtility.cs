using System.Collections.Generic;
using System.Runtime.CompilerServices;
using VMFramework.Core;
using UnityEngine;

public static class SpriteRendererUtility
{
    #region PhysicsShape

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static List<Vector2> GetPhysicsShape(this SpriteRenderer spriteRenderer, int shapeIndex)
    {
        var shapePoints = new List<Vector2>();
        spriteRenderer.sprite.GetPhysicsShape(0, shapePoints);
        return shapePoints;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static List<Vector2> GetPhysicsShape(this SpriteRenderer spriteRenderer)
    {
        return GetPhysicsShape(spriteRenderer, 0);
    }

    #endregion

    public static void SetSpriteColor(this Component component, Color color)
    {
        var spriteRenderer = component.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null )
        {
            spriteRenderer.color = color;
        }
    }

    public static void SetSpriteAlpha(this Component component, float alpha)
    {
        var spriteRenderer = component.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.color = spriteRenderer.color.ChangeAlpha(alpha);
        }
    }

    public static void SetSprite(this Component component, Sprite sprite)
    {
        var spriteRenderer = component.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = sprite;
        }
    }

    public static Sprite GetSprite(this Component component)
    {
        var spriteRenderer = component.GetComponent<SpriteRenderer>();

        return spriteRenderer == null ? null : spriteRenderer.sprite;
    }

    public static Sprite GetSprite(this GameObject gameObject)
    {
        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        return spriteRenderer == null ? null : spriteRenderer.sprite;
    }

    public static void SetSortingOrder(this Component component, int order)
    {
        var spriteRenderer = component.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = order;
        }
    }

    public static void SetSortingLayerName(this Component component, string name)
    {
        var spriteRenderer = component.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.sortingLayerName = name;
        }
    }

    public static void SetCanvasGroupAlpha(this Component component, float alpha)
    {
        var canvasGroup = component.GetComponent<CanvasGroup>();

        if (canvasGroup != null)
        {
            canvasGroup.alpha = alpha;
        }
    }
}
