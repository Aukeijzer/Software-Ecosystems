using spider.Dtos;
using spider.Models;
using spider.Models;
using spider.Models.Rest;

namespace spider.Converter;

public interface IRestDataConverter
{
    public List<ContributorDto> ToContributors(Contributors contributors);
    
}