# GetRepoCount Method


This method gets the amount of repositories that match the keyword with the included filters



## Definition
**Namespace:** <a href="c6df77e0-28de-d4ed-9b46-1241a40828db">spider.Services</a>  
**Assembly:** spider (in spider.exe) Version: 1.0.0+c7d0df4be2214e146cfb2d406db1cb7a57dc276b

**C#**
``` C#
public Task<int?> GetRepoCount(
	string keyword,
	int starCountLower,
	int starCountUpper,
	int tries = 3
)
```



#### Parameters
<dl><dt>  String</dt><dd>The keyword to search for</dd><dt>  Int32</dt><dd>lower bound for the amount of stars</dd><dt>  Int32</dt><dd>upper bound for the amount of stars</dd><dt>  Int32  (Optional)</dt><dd>amount of retries before failing</dd></dl>

#### Return Value
Task(Nullable(Int32))  
the amount of repositories in the form of an int

#### Implements
<a href="90d285c1-f6e7-3adf-f1cd-b28d4a2cc619">IGitHubGraphqlService.GetRepoCount(String, Int32, Int32, Int32)</a>  


## Exceptions
<table>
<tr>
<td>BadHttpRequestException</td>
<td>If it fails after tries amount of retries throw</td></tr>
</table>

## See Also


#### Reference
<a href="dfcd0dda-1a22-945e-c8e0-186fc06cea47">GitHubGraphqlService Class</a>  
<a href="c6df77e0-28de-d4ed-9b46-1241a40828db">spider.Services Namespace</a>  
