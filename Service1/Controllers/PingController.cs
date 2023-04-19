using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry;

namespace Service1.Controllers;

[ApiController]
[Route("[controller]")]
public class PingController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        using var source = new ActivitySource("ExampleTracer");

        // A span
        using var activity = source.StartActivity("Call to Service B");

        Baggage.Current.SetBaggage("ExampleItem", "The information");

        // 'Ping' Service B
        using var client = new HttpClient();
        _ = await client.GetAsync("http://host.docker.internal:6000/ping");

        // Another span
        using var activityTwo = source.StartActivity("Arbitrary 10ms delay");
        await Task.Delay(10);

        return Ok();
    }
}
    