﻿using SECODashBackend.Models;
using spider.Models;

namespace spider.Converter;

public class SpiderDataConverter : ISpiderDataConverter
{
    public List<Project> ToProjects(SpiderData data)
    {
        // Parser to parse the result from the search query to a C# data type.
        List<Project> projects = DataToProjects(data.search.nodes);
        return projects;
    }
    
    public List<Project> TopicSearchToProjects(TopicSearchData data)
    {
        List<Project> projects = DataToProjects(data.topic.repositories.nodes);
        return projects;
    }
    
    private List<Project> DataToProjects(Repository[] nodes)
    {
        var projects = new List<Project>();
        foreach (var repository in nodes)
        {
            String[] topics = new String[nodes.Length];
            for (int i = 0; i < nodes.Length; i++)
            {
                topics[i] = repository.repositoryTopics.nodes[i].topic.name;
            }
            projects.Add(new Project()
            {
                Name = repository.name,
                ReadMe = (repository.readme == null ? null : repository.readme.text),
                Owner = repository.owner.login,
                Description = repository.description,
                Topics = topics
            });
        }

        return projects;
    }
}