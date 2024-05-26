using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class SubRectangles
{
    [ShowInInspector]
    private Dictionary<int, List<Vector2Int>> xLinesDict = new();

    [Button]
    public void AddPoint(Vector2Int point)
    {
        if (xLinesDict.TryGetValue(point.y, out var lines) == false)
        {
            lines = new List<Vector2Int>();
            xLinesDict.Add(point.y, lines);
            lines.Add(new Vector2Int(point.x, point.x));
        }
        else
        {
            for (int i = 0; i < lines.Count; i++)
            {
                var range = lines[i];
                if (point.x > range.y + 1)
                {
                    if (i == lines.Count - 1)
                    {
                        lines.Add(new Vector2Int(point.x, point.x));
                        return;
                    }

                    continue;
                }

                if (point.x == range.y + 1)
                {
                    range.y = point.x;

                    if (i < lines.Count - 1 && range.y == lines[i + 1].x - 1)
                    {
                        range.y = lines[i + 1].y;
                        lines.RemoveAt(i + 1);
                    }

                    lines[i] = range;

                    return;
                }

                if (point.x == range.x - 1)
                {
                    lines[i] = new Vector2Int(point.x, range.y);

                    return;
                }

                if (point.x < range.x - 1)
                {
                    lines.Insert(i, new Vector2Int(point.x, point.x));

                    return;
                }
            }
        }
    }

    [Button]
    public void RemovePoint(Vector2Int point)
    {
        if (xLinesDict.TryGetValue(point.y, out var lines) == false)
        {
            return;
        }

        for (int i = 0; i < lines.Count; i++)
        {
            var range = lines[i];
            if (point.x < range.x || point.x > range.y)
            {
                continue;
            }

            if (point.x == range.x && point.x == range.y)
            {
                lines.RemoveAt(i);
                return;
            }

            if (point.x == range.x)
            {
                lines[i] = new Vector2Int(point.x + 1, range.y);
                return;
            }

            if (point.x == range.y)
            {
                lines[i] = new Vector2Int(range.x, point.x - 1);
                return;
            }

            lines.Insert(i + 1, new Vector2Int(point.x + 1, range.y));
            lines[i] = new Vector2Int(range.x, point.x - 1);
            return;
        }
    }

    [Button]
    public IEnumerable<(Vector2Int start, Vector2Int end)> GetRectangles()
    {
        foreach (var (y, ranges) in xLinesDict)
        {
            foreach (var range in ranges)
            {
                yield return (new Vector2Int(range.x, y),
                    new Vector2Int(range.y, y));
            }
        }
    }
}
