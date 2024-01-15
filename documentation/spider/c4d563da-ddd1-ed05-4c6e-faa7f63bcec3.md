# GetByKeywordSplit Method


GetByKeywordSplit splits the search space into smaller chunks and then calls GetByKeyword on each chunk.



## Definition
**Namespace:** <a href="c6df77e0-28de-d4ed-9b46-1241a40828db">spider.Services</a>  
**Assembly:** spider (in spider.exe) Version: 1.0.0+c7d0df4be2214e146cfb2d406db1cb7a57dc276b

**C#**
``` C#
public Task<List<ProjectDto>> GetByKeywordSplit(
	string name,
	int amount,
	string? startCursor
)
```



#### Parameters
<dl><dt>  String</dt><dd>keyword to search by</dd><dt>  Int32</dt><dd>Amount of repositories to return</dd><dt>  String</dt><dd>The cursor to start searching from. If startCursor is null it starts searching from the start</dd></dl>

#### Return Value
Task(List(<a href="7153ffa9-75d9-d756-b8b0-dace1841bf5b">ProjectDto</a>))  
A list of repositories including contributors in the form of List&lt;ProjectDto&gt;

#### Implements
<a href="f4065558-f243-1e97-b3a9-e64febc5a099">ISpiderProjectService.GetByKeywordSplit(String, Int32, String)</a>  


## See Also


#### Reference
<a href="002041a8-208c-6226-6dbb-8cf036f78722">SpiderProjectService Class</a>  
<a href="c6df77e0-28de-d4ed-9b46-1241a40828db">spider.Services Namespace</a>  
