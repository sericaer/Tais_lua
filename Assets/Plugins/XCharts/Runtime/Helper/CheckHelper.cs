/******************************************/
/*                                        */
/*     Copyright (c) 2018 monitor1394     */
/*     https://github.com/monitor1394     */
/*                                        */
/******************************************/
using System.Text;
using UnityEngine;

namespace XCharts
{
    internal static class CheckHelper
    {
        private static bool IsColorAlphaZero(Color color)
        {
            return color != Color.clear && color.a == 0;
        }
        public static string CheckChart(BaseChart chart)
        {
            var sb = ChartHelper.sb;
            sb.Length = 0;
            //sb.AppendFormat("version:{0}_{1}\n", XChartsMgr.version, XChartsMgr.date);
            CheckSize(chart, sb);
            CheckTheme(chart, sb);
            CheckTitle(chart, sb);
            CheckLegend(chart, sb);
            CheckGrid(chart, sb);
            CheckSerie(chart, sb);
            return sb.ToString();
        }

        private static void CheckSize(BaseChart chart, StringBuilder sb)
        {
            if (chart.chartWidth == 0 || chart.chartHeight == 0)
            {
                sb.Append("warning:chart width or height is 0\n");
            }
        }

        private static void CheckTheme(BaseChart chart, StringBuilder sb)
        {
            var themeInfo = chart.themeInfo;
            themeInfo.CheckWarning(sb);
        }

        private static void CheckTitle(BaseChart chart, StringBuilder sb)
        {
            var title = chart.title;
            if (!title.show) return;
            if (string.IsNullOrEmpty(title.text)) sb.Append("warning:title->text is null\n");
            if (title.textStyle.color != Color.clear && title.textStyle.color.a == 0)
                sb.Append("warning:title->textStyle->color alpha is 0\n");
            if (title.subTextStyle.color != Color.clear && title.subTextStyle.color.a == 0)
                sb.Append("warning:title->subTextStyle->color alpha is 0\n");
        }

        private static void CheckLegend(BaseChart chart, StringBuilder sb)
        {
            var legend = chart.legend;
            if (!legend.show) return;
            if (legend.textStyle.color != Color.clear && legend.textStyle.color.a == 0)
                sb.Append("warning:legend->textStyle->color alpha is 0\n");
            var serieNameList = chart.series.GetLegalSerieNameList();
            Debug.LogError("namelist:" + serieNameList.Count);
            if (serieNameList.Count == 0) sb.Append("warning:legend need serie.name or serieData.name not empty\n");
            foreach (var category in legend.data)
            {
                if (!serieNameList.Contains(category))
                    sb.AppendFormat("warning:legend [{0}] is invalid, must be one of serie.name or serieData.name\n", category);
            }
        }

        private static void CheckGrid(BaseChart chart, StringBuilder sb)
        {
            if (chart is CoordinateChart)
            {
                var grid = (chart as CoordinateChart).grid;
                if (grid.left >= chart.chartWidth)
                    sb.Append("warning:grid->left > chartWidth\n");
                if (grid.right >= chart.chartWidth)
                    sb.Append("warning:grid->right > chartWidth\n");
                if (grid.top >= chart.chartHeight)
                    sb.Append("warning:grid->top > chartHeight\n");
                if (grid.bottom >= chart.chartHeight)
                    sb.Append("warning:grid->bottom > chartHeight\n");
                if (grid.left + grid.right >= chart.chartWidth)
                    sb.Append("warning:grid.left + grid.right > chartWidth\n");
                if (grid.top + grid.bottom >= chart.chartHeight)
                    sb.Append("warning:grid.top + grid.bottom > chartHeight\n");
            }
        }

        private static void CheckSerie(BaseChart chart, StringBuilder sb)
        {
            foreach (var serie in chart.series.list)
            {
                if (IsColorAlphaZero(serie.itemStyle.color))
                    sb.AppendFormat("warning:serie {0} itemStyle->color alpha is 0\n", serie.index);
                if (serie.itemStyle.opacity == 0)
                    sb.AppendFormat("warning:serie {0} itemStyle->opacity is 0\n", serie.index);
                if (serie.itemStyle.borderWidth != 0 && IsColorAlphaZero(serie.itemStyle.borderColor))
                    sb.AppendFormat("warning:serie {0} itemStyle->borderColor alpha is 0\n", serie.index);
                switch (serie.type)
                {
                    case SerieType.Line:
                        if (serie.lineStyle.width == 0)
                            sb.AppendFormat("warning:serie {0} lineStyle->width is 0\n", serie.index);
                        if (serie.lineStyle.opacity == 0)
                            sb.AppendFormat("warning:serie {0} lineStyle->opacity is 0\n", serie.index);
                        if (serie.lineStyle.color != Color.clear && serie.lineStyle.color.a == 0)
                            sb.AppendFormat("warning:serie {0} lineStyle->color alpha is 0\n", serie.index);
                        break;
                    case SerieType.Bar:
                        if (serie.barWidth == 0)
                            sb.AppendFormat("warning:serie {0} barWidth is 0\n", serie.index);
                        break;
                    case SerieType.Pie:
                        if (serie.radius.Length >= 2 && serie.radius[1] == 0)
                            sb.AppendFormat("warning:serie {0} radius[1] is 0\n", serie.index);
                        break;
                    case SerieType.Scatter:
                    case SerieType.EffectScatter:
                        if (serie.symbol.type == SerieSymbolType.None)
                            sb.AppendFormat("warning:symbol type is None");
                        break;

                }
            }
        }
    }
}