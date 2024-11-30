using Murmur;
using System.Security.Cryptography;

namespace Kube.Lib;

public class WeatherService
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    public IEnumerable<WeatherForecast> GetWeatherForecast()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)],
            Hash = Hash()
        }).ToArray();
    }

    private string Hash()
    {
        var data = Guid.NewGuid().ToByteArray();
        HashAlgorithm murmur128 = MurmurHash.Create128(managed: false); // returns a 128-bit algorithm using "unsafe" code with default seed
        var hash = murmur128.ComputeHash(data);
        return BitConverter.ToString(hash).Replace("-", "");
    }
}