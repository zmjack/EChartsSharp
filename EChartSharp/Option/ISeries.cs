using EChartSharp.Series;
using NStandard.Drawing;
using System.Text.Json.Serialization;

namespace EChartSharp.Option;

[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]
[JsonDerivedType(typeof(SeriesBar), nameof(SeriesBar))]
[JsonDerivedType(typeof(SeriesLine), nameof(SeriesLine))]
[JsonDerivedType(typeof(SeriesPie), nameof(SeriesPie))]
public interface ISeries
{
    string Type { get; }
    string? Name { get; set; }
    SeriesDataValue? Data { get; set; }
    RgbaColor? Color { get; set; }
    bool? Smooth { get; set; }
    string? Stack { get; set; }
}
