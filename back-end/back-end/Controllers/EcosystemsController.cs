using SECODashBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace SECODashBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class EcosystemsController: ControllerBase
{
    [HttpGet]
    [SwaggerOperation("Get all ecosystems")]
    [SwaggerResponse(statusCode: 200, description: "successful operation")]
    public ActionResult<List<Ecosystem>> GetAll(){
        List<Ecosystem> topics = new List<Ecosystem>
        { new (
            1,
            "agriculture", 
            "Agriculture",
            new List<Project>
                {
                    new(
                    "awesome-agriculture", 
                    1, 
                    "Open source technology for agriculture, farming, and gardening", 
                    null,
                    null,
                    1100)},
    34534)
        };
        return new ObjectResult(topics);
    }
}