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

using System.Runtime.Serialization;
using System.Runtime.Serialization.DataContracts;

namespace SECODashBackend.Dtos.Ecosystem;
/// <summary>
/// This class represents a data transfer object of all data needed to create a new ecosystem.
/// </summary>
public class EcosystemCreationDto
{
    [DataMember(Name = "ecosystemName")]
    public string EcosystemName { get; set; }
    
    [DataMember(Name = "description")]
    public string Description { get; set; }
    
    [DataMember(Name = "email")]
    public string Email { get; set; }
    
    [DataMember(Name = "topics")]
    public List<string> Topics { get; set; }
    
    [DataMember(Name = "technologies")]
    public List<string> Technologies { get; set; }
    
    [DataMember(Name = "excluded")]
    public List<string> Excluded { get; set; }
 }