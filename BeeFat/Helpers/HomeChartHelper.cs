using BeeFat.Components.Account.Domain.Helpers;
using BeeFat.Data;
using Blazorise.Charts;

namespace BeeFat.Helpers;

public class HomeChartHelper
{
    public LineChart<WatcherEvent> LineChart;

    public LineChartOptions LineChartOptions = new()
    {
        Parsing = new ChartParsing
        {
            XAxisKey = "sector",
            YAxisKey = "count",
        }
    };

    private List<string> _backgroundColors = new() { ChartColor.FromRgba( 255, 99, 132, 0.2f ), ChartColor.FromRgba( 54, 162, 235, 0.2f ), ChartColor.FromRgba( 255, 206, 86, 0.2f ), ChartColor.FromRgba( 75, 192, 192, 0.2f ), ChartColor.FromRgba( 153, 102, 255, 0.2f ), ChartColor.FromRgba( 255, 159, 64, 0.2f ) };
    private List<string> _borderColors = new() { ChartColor.FromRgba( 255, 99, 132, 1f ), ChartColor.FromRgba( 54, 162, 235, 1f ), ChartColor.FromRgba( 255, 206, 86, 1f ), ChartColor.FromRgba( 75, 192, 192, 1f ), ChartColor.FromRgba( 153, 102, 255, 1f ), ChartColor.FromRgba( 255, 159, 64, 1f ) };

    private bool _isAlreadyInitialised;

    private HomeHelper _homeHelper;

    public HomeChartHelper(HomeHelper homeHelper)
    {
        LineChart = new LineChart<WatcherEvent>();
        _homeHelper = homeHelper;
    }

    public class WatcherEvent
    {
        public string Sector { get; set; }

        public int Count { get; set; }

        public DateTime Date { get; } = DateTime.Now;
    }

    public async Task OnAfterRenderAsync(ApplicationUser user, bool firstRender)
    {
        if (firstRender)
        {
            var dataset = GetLineChartDataset(user);
            await LineChart.Clear();
            await LineChart.AddDataSet(dataset);
        }
    }


    private LineChartDataset<WatcherEvent> GetLineChartDataset(ApplicationUser user)
    {
        var weekMacronutrients = _homeHelper.GetPrefixWeekMacronutrients(user).ToList();
        var data = weekMacronutrients.Select(m => new WatcherEvent()
            { Sector = StaticBeeFat.NumberToDay[m.DayOfWeek], Count = m.Macronutrient.Calories }).ToList();
        
        return new()
        {
            Label = "Количество съеденных калорий",
            Data = data,
            BackgroundColor = _backgroundColors[0], // line chart can only have one color
            BorderColor = _borderColors[0],
            Fill = true,
            PointRadius = 3,
            BorderWidth = 1,
            PointBorderColor = Enumerable.Repeat( _borderColors.First(), 6 ).ToList(),
            CubicInterpolationMode = "monotone",
        };
    }
}