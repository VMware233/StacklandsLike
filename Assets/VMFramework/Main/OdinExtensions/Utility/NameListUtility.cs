using System;
using System.Collections.Generic;
using System.Linq;
using VMFramework.Core;
using Sirenix.OdinInspector;

public static class NameListUtility
{
    public static IEnumerable<ValueDropdownItem> GetAllBaseTypesNameList(this IEnumerable<object> objects, 
        bool includingInterfaces, bool includingGeneric)
    {
        var initialTypes = new HashSet<Type>();

        foreach (var o in objects)
        {
            initialTypes.Add(o.GetType());
        }

        var resultTypes = new HashSet<Type>();

        foreach (var type in initialTypes.LevelOrderTraverse(true,
                     t => t.GetBaseTypes(includingInterfaces, false)))
        {
            if (type.IsGenericType && includingGeneric == false)
            {
                continue;
            }

            resultTypes.Add(type);
        }

        return resultTypes.Select(t => new ValueDropdownItem(t.FullName?.Replace(".", "/"), t));
    }

    public static IEnumerable<ValueDropdownItem> GetDerivedTypesNameList(this Type baseType, 
        bool includingSelf, bool includingInterfaces, bool includingGeneric, bool includingAbstract)
    {
        var resultTypes = new HashSet<Type>();

        foreach (var type in baseType.GetDerivedClasses(includingSelf, false))
        {
            if (type.IsGenericType && includingGeneric == false)
            {
                continue;
            }

            if (type.IsInterface && includingInterfaces == false)
            {
                continue;
            }
            
            if (type.IsAbstract && includingAbstract == false)
            {
                continue;
            }

            resultTypes.Add(type);
        }

        return resultTypes.Select(t => new ValueDropdownItem(t.FullName?.Replace(".", "/"), t));
    }
}
