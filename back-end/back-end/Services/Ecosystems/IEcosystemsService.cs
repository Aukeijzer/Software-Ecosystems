﻿using SECODashBackend.Models;

namespace SECODashBackend.Services.Ecosystems;

public interface IEcosystemsService
{
   public Task<List<Ecosystem>?> GetAllAsync();
   public Task<int> AddAsync(Ecosystem ecosystem);
   Task<Ecosystem?> GetByIdAsync(long id);
   Task<Ecosystem?> GetByNameAsync(string name);
   
}