﻿using SECODashBackend.Dtos;

namespace SECODashBackend.Services.DataProcessor;
public interface IDataProcessorService
{
    public Task<IEnumerable<TopicResponseDto>> GetTopics(IEnumerable<TopicRequestDto> readmeDtos);
}