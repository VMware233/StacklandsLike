using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using VMFramework.Core;

public sealed class TypeCollector<TType>
{
    public bool includingSelf { get; init; }
    
    public bool includingAbstract { get; init; }
    
    public bool includingInterface { get; init; }
    
    public bool includingGenericDefinition { get; init; }
    
    private readonly List<Type> types = new();
    
    public int count => types.Count;
    
    public void Collect()
    {
        types.Clear();

        foreach (var type in typeof(TType).GetDerivedClasses(includingSelf,
                     includingGenericDefinition))
        {
            if (includingAbstract == false && type.IsAbstract)
            {
                continue;
            }

            if (includingInterface == false && type.IsInterface)
            {
                continue;
            }

            types.Add(type);
        }
    }

    public IReadOnlyList<Type> GetCollectedTypes()
    {
        return types;
    }

    public IEnumerable<ValueDropdownItem<Type>> GetNameList()
    {
        if (types.Count == 0)
        {
            Collect();
        }

        foreach (var type in types)
        {
            yield return new ValueDropdownItem<Type>(type.Name, type);
        }
    }
}
