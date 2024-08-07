﻿using EChartSharp.Types;

namespace EChartSharp.Option;

public class Legend : IAlignment
{
    public OrientValue Orient { get; set; }
    public int? Left { get; set; }
    public int? Top { get; set; }
    public int? Right { get; set; }
    public int? Bottom { get; set; }
}
