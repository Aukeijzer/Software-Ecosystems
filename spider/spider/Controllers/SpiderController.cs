using Microsoft.AspNetCore.Mvc;
using spider;

namespace spider.Controllers;

[ApiController]
[Route("[controller]")]
public class SpiderController
{
    [HttpGet(Name = "GetQueryData")]
    public SpiderData Get()
    {

        return Spider.Main();
    }
}