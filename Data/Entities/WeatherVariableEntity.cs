public class WeatherVariableEntity : BaseEntity
{
    public decimal Value { get; set; }
    public string Unit { get; set; }
    public string LongName { get; set; }
    public DateTime Timestamp { get; set; }
    public int WeatherStationId { get; set; }
}