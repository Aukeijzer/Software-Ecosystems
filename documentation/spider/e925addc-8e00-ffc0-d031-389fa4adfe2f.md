# GetByNames Method


GetContributersByName receives a HttpPost request with a list of ProjectRequestDtos and then returns a list of repositories for each ProjectRequestDto



## Definition
**Namespace:** <a href="9f16cf3d-ca1a-18ca-2f8b-43d73ecc7c0a">spider.Controllers</a>  
**Assembly:** spider (in spider.exe) Version: 1.0.0+c7d0df4be2214e146cfb2d406db1cb7a57dc276b

**C#**
``` C#
public Task<ActionResult<List<ProjectDto>>> GetByNames(
	List<ProjectRequestDto> repos
)
```



#### Parameters
<dl><dt>  List(<a href="12393ff2-f4e8-f895-f359-5363e9206efc">ProjectRequestDto</a>)</dt><dd>List of ProjectRequestDtos with at least the repo and ownerNames</dd></dl>

#### Return Value
Task(ActionResult(List(<a href="7153ffa9-75d9-d756-b8b0-dace1841bf5b">ProjectDto</a>)))  
A list of repositories in the form of List&lt;ProjectDto&gt;

## See Also


#### Reference
<a href="393932f4-1d4b-ab60-40af-a0a9e1f1ac78">SpiderController Class</a>  
<a href="9f16cf3d-ca1a-18ca-2f8b-43d73ecc7c0a">spider.Controllers Namespace</a>  
