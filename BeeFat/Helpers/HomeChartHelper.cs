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

    private List<string> backgroundColors = new() { ChartColor.FromRgba( 255, 99, 132, 0.2f ), ChartColor.FromRgba( 54, 162, 235, 0.2f ), ChartColor.FromRgba( 255, 206, 86, 0.2f ), ChartColor.FromRgba( 75, 192, 192, 0.2f ), ChartColor.FromRgba( 153, 102, 255, 0.2f ), ChartColor.FromRgba( 255, 159, 64, 0.2f ) };
    private List<string> borderColors = new() { ChartColor.FromRgba( 255, 99, 132, 1f ), ChartColor.FromRgba( 54, 162, 235, 1f ), ChartColor.FromRgba( 255, 206, 86, 1f ), ChartColor.FromRgba( 75, 192, 192, 1f ), ChartColor.FromRgba( 153, 102, 255, 1f ), ChartColor.FromRgba( 255, 159, 64, 1f ) };

    private bool isAlreadyInitialised;

    private HomeHelper _homeHelper;

    public HomeChartHelper(HomeHelper homeHelper)
    {
        _homeHelper = homeHelper;
    }

    public class WatcherEvent
    {
        public string Sector { get; set; }

        public int Count { get; set; }

        public DateTime Date { get; } = DateTime.Now;
    }

    public async Task OnAfterRenderAsync( bool firstRender )
    {
        await LineChart.Clear();
        await LineChart.AddDataSet( GetLineChartDataset() );
        // if ( !isAlreadyInitialised )
        // {
        //     isAlreadyInitialised = true;
        //
        //     await LineChart.Clear();
        //     await LineChart.AddDataSet( GetLineChartDataset() );
        // }
    }

    private LineChartDataset<WatcherEvent> GetLineChartDataset()
    {
        var metabolism = MetabolismCalculator.CalculateMetabolism(_homeHelper.User);
        var weekMacronutrients = _homeHelper.GetPrefixWeekMacronutrients().ToList();
        var data = weekMacronutrients.Select(m => new WatcherEvent()
            { Sector = StaticBeeFat.NumberToDay[m.DayOfWeek], Count = Math.Min(2*metabolism, m.Macronutrient.Calories) }).ToList();
        
        return new()
        {
            Label = "Статистика следования треку",
            Data = data,
            BackgroundColor = backgroundColors[0], // line chart can only have one color
            BorderColor = borderColors[0],
            Fill = true,
            PointRadius = 3,
            BorderWidth = 1,
            PointBorderColor = Enumerable.Repeat( borderColors.First(), 6 ).ToList(),
            CubicInterpolationMode = "monotone",
        };
    }
}