﻿namespace spider.Models.Graphql;

public class SpiderData
{
    public SearchResult Search { get; init; }
    
    public RateLimit? RateLimit { get; init; }
}