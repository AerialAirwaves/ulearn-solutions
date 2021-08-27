using System.Collections.Generic;
using GeometryTasks;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace GeometryPainting
{
    public static class SegmentExtensions
    {
        private class ColorWrapper
        {
            public Color Color;
        }

        private static ConditionalWeakTable<Segment, ColorWrapper> segmentsColors
            = new ConditionalWeakTable<Segment, ColorWrapper>();

        public static void SetColor(this Segment segment, Color color)
        {
            if (segmentsColors.TryGetValue(segment, out var result))
                result.Color = color;
            else
                segmentsColors.Add(segment, new ColorWrapper { Color = color});
        }

        public static Color GetColor(this Segment segment)
        {
            return (segmentsColors.TryGetValue(segment, out var result))
                ? result.Color
                : Color.Black;
        }
    }
}
