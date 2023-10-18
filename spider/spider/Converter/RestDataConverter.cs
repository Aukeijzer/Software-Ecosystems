using spider.Dtos;
using spider.Models.Rest;

namespace spider.Converter;

public class RestDataConverter : IRestDataConverter
{
    public List<ContributorDto> ToContributors(Contributors contributors)
    {
        List<ContributorDto> result = new List<ContributorDto>();
        foreach (var contributor in contributors.Items)
        {
            result.Add(GithubToContributorDto(contributor));
        }

        return result;
    }

    public ContributorDto GithubToContributorDto(Contributor contributor)
    {
        var contributorDto = new ContributorDto
        {
            Contributions = contributor.Contributions,
            Id = contributor.Id,
            Login = contributor.Login,
            NodeId = contributor.NodeId
        };
        return contributorDto;
    }
}