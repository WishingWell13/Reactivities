namespace API;

public class WeatherForecast
{
    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    //Question mark mens is optional
    //public string? Summary { get; set; }
    //Only use though if Nullable is set to true in API.csproj
    public string Summary { get; set; }
}
