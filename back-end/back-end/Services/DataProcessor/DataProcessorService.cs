// Copyright (C) <2024>  <ODINDash>
// 
// This file is part of SECODash.
// 
// SECODash is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// SECODash is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with SECODash.  If not, see <https://www.gnu.org/licenses/>.

   using RestSharp;
using SECODashBackend.Dtos.Topic;
using SECODashBackend.Dtos.Project;

namespace SECODashBackend.Services.DataProcessor;

/// <summary>
/// This service is responsible for sending the readme data to the data processor and returning the resulting topics.
/// </summary>
public class DataProcessorService : IDataProcessorService
{
   private readonly RestClient _client;
   
   public DataProcessorService(string connectionString)
   {
      var options = new RestClientOptions(connectionString);
      options.MaxTimeout = 100000000;
      _client = new RestClient(options);
   }
   
   /// <summary>
   /// Sends the readme data to the data processor and modifies projectDTOS by adding the resulting topics.
   /// </summary>
   /// <param name="projectDtos">List of project data.</param>
   /// <exception cref="HttpRequestException">Thrown if the request cannot be deserialized into a List of ProjectTopicsDtos.</exception>
   public async Task<List<ProjectDto>> GetTopics(List<ProjectDto> projectDtos)
   {
      var readmeDtos = ConvertToTopicDto(projectDtos);
      
      // Only extract topics if more than 10 readmes are found
      if (readmeDtos.Count > 10)
      {
         var request = new RestRequest("extract-topics", Method.Post).AddJsonBody(readmeDtos);
         var responseDtos = await _client.PostAsync<List<TopicResponseDto>>(request) ??
                            throw new HttpRequestException();
         var newDtos = AddTopicsToProjects(responseDtos, projectDtos);
         return newDtos;
      }
      return projectDtos;
   }
   
   /// <summary>
   /// Adds topics from topics extracted from data processor to the projects
   /// </summary>
   /// <param name="topicDtos"> List of extracted topics per project.</param>
   /// <param name="projectDtos">List of project data.</param>
   private List<ProjectDto> AddTopicsToProjects(List<TopicResponseDto> topicDtos, List<ProjectDto> projectDtos)
   {
      // Create a dictionary using ProjectId as the key
      Dictionary<string, TopicResponseDto> topicDictionary = topicDtos.ToDictionary(t => t.ProjectId);

      foreach (var dto in projectDtos)
      {
         // Check if the projectID exists in the dictionary
         if (topicDictionary.TryGetValue(dto.Id, out var topicDto))
         {
            dto.AdditionalTopics = topicDto.Topics;
         }
      }

      return projectDtos;
   }

   
   /// <summary>
   /// Converts a list projectDto to a list of topicRequestDtos
   /// </summary>
   /// <param name="projectDtos">List of project data.</param>
   /// <returns> List of TopicRquestDtos </returns>
   private List<TopicRequestDto> ConvertToTopicDto(List<ProjectDto> projectDtos)
   {
      // Filter out projects with empty readme
      var readmeDtos = projectDtos
         .Where(dto => !string.IsNullOrEmpty(dto.ReadMe))
         .Select(dto => new TopicRequestDto
         {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            Readme = dto.ReadMe
         })
         .ToList();

      return readmeDtos;
   }

}
