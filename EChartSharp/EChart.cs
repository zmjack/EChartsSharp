using EChartSharp.Option;
using EChartSharp.Series;
using NStandard.Data;

namespace EChartSharp;

public class EChart
{
    public Title? Title { get; set; }
    public Tooltip? Tooltip { get; set; }
    public Legend? Legend { get; set; }

    public Axis? XAxis { get; set; }
    public Axis? YAxis { get; set; }
    public ISeries[]? Series { get; set; }

    public EChart SetAxis(string[] classes, Orient orient = Orient.Horizontal)
    {
        if (orient == Orient.Horizontal)
        {
            XAxis = new()
            {
                Type = AxisType.Category,
                Data = classes,
            };
            YAxis = new()
            {
                Type = AxisType.Value,
            };
        }
        else if (orient == Orient.Vertical)
        {
            XAxis = new()
            {
                Type = AxisType.Value,
            };
            YAxis = new()
            {
                Type = AxisType.Category,
                Data = classes,
            };
        }
        else throw new NotImplementedException();

        return this;
    }

    private Axis? GetClassAxis()
    {
        if (XAxis?.Type?.Value == AxisType.Category) return XAxis;
        if (YAxis?.Type?.Value == AxisType.Category) return YAxis;
        return null;
    }

    private void CheckAxis(DataFrame<double> frame)
    {
        var categoryAxis = GetClassAxis();
        if (categoryAxis is null)
        {
            SetAxis(frame.Index);
            categoryAxis = GetClassAxis()!;
        }

        if (!Enumerable.SequenceEqual(categoryAxis.Data ?? [], frame.Index)) throw new InvalidOperationException("The values of index must be same.");
    }

    public EChart Line(DataFrame<double> frame, bool stacked = false)
    {
        CheckAxis(frame);

        var stack = stacked ? Guid.NewGuid().ToString() : null;
        Series =
        [
            ..
            Series ?? [],

            ..
            from values in frame.ColumnValues()
            select new SeriesLine
            {
                Stack = stack,
                Data = new(values)
            }
        ];
        return this;
    }

    public EChart Bar(DataFrame<double> frame, bool stacked = false)
    {
        CheckAxis(frame);

        var stack = stacked ? Guid.NewGuid().ToString() : null;
        Series =
        [
            ..
            Series ?? [],

            ..
            from values in frame.ColumnValues()
            select new SeriesBar
            {
                Stack = stack,
                Data = new(values)
            }
        ];
        return this;
    }

    public EChart Pie(DataFrame<double> frame)
    {
        CheckAxis(frame);

        Series =
        [
            ..
            Series ?? [],

            new SeriesPie
            {
                Data = new(
                [
                    ..
                    from zip in frame.Index.Zip(frame.ColumnValues(0))
                    select new DataElement
                    {
                        Name = zip.First.ToString(),
                        Value = zip.Second
                    }
                ])
            }
        ];
        return this;
    }

}
