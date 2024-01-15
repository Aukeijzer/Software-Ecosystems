# GetByTopic(String, Int32, String) Method


GetByTopic receives a HttpGet request and returns a list of repositories



## Definition
**Namespace:** <a href="9f16cf3d-ca1a-18ca-2f8b-43d73ecc7c0a">spider.Controllers</a>  
**Assembly:** spider (in spider.exe) Version: 1.0.0+c7d0df4be2214e146cfb2d406db1cb7a57dc276b

**C#**
``` C#
public Task<ActionResult<List<ProjectDto>>> GetByTopic(
	string topic,
	int amount,
	string? startCursor
)
```



#### Parameters
<dl><dt>  String</dt><dd>topic to search for</dd><dt>  Int32</dt><dd>Amount of repositories to return</dd><dt>  String</dt><dd>The cursor to start the search from. If cursor is null start from the start</dd></dl>

#### Return Value
Task(ActionResult(List(<a href="7153ffa9-75d9-d756-b8b0-dace1841bf5b">ProjectDto</a>)))  
A list of repositories in the form of List&lt;ProjectDto&gt;

## See Also


#### Reference
<a href="393932f4-1d4b-ab60-40af-a0a9e1f1ac78">SpiderController Class</a>  
<a href="8dbf61aa-3e17-e7fe-dcfa-dcba9fa99fa0">GetByTopic Overload</a>  
<a href="9f16cf3d-ca1a-18ca-2f8b-43d73ecc7c0a">spider.Controllers Namespace</a>  
