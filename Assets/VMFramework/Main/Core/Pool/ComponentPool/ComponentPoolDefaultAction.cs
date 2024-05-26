using UnityEngine;

namespace VMFramework.Core.Pool
{
    public static class ComponentPoolDefaultAction
    {
        public static void OnGet<TComponent>(TComponent component)
            where TComponent : Component
        {
            component.gameObject.SetActive(false);
        }
        
        public static void OnReturn<TComponent>(TComponent component)
            where TComponent : Component
        {
            component.SetActive(false);
        }
        
        public static void OnClear<TComponent>(TComponent component)
            where TComponent : Component
        {
            Object.Destroy(component.gameObject);
        }
    }
}