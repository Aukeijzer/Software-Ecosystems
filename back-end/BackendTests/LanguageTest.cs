﻿using NUnit.Framework;
using System;
using SECODashBackend.Classifier;
using SECODashBackend.Dto;
using SECODashBackend.Enums;
using SECODashBackend.Models;

namespace BackendTests;
[TestFixture]
public class LanguageTest
{
    [Test]
    public void GetLanguagesPerEcosystem_ReturnsCorrectList()
    {
        // Arrange
        var ecosystems = new List<Ecosystem>
        {
            new Ecosystem
            {
                Id = "1",
                Name = "Ecosystem1",
                Projects = new List<Project>
                {
                    new Project
                    {
                        Id = "1",
                        Name = "Project1",
                        Languages = new List<ProjectProgrammingLanguage>
                        {
                            new ProjectProgrammingLanguage
                            {
                                Id = "1",
                                Language = ProgrammingLanguage.CSharp,
                                Percentage = 70,
                            },
                            new ProjectProgrammingLanguage
                            {
                                Id = "2",
                                Language = ProgrammingLanguage.Java,
                                Percentage = 30,
                            }
                        },
                        Owner = "Owner1",
                    },
                    new Project
                    {
                        Id = "2",
                        Name = "Project2",
                        Languages = new List<ProjectProgrammingLanguage>
                        {
                            new ProjectProgrammingLanguage
                            {
                                Id = "3",
                                Language = ProgrammingLanguage.ABAP,
                                Percentage = 10
                            },
                            new ProjectProgrammingLanguage
                            {
                                Id = "4",
                                Language = ProgrammingLanguage.Python,
                                Percentage = 30
                            },
                            new ProjectProgrammingLanguage
                            {
                                Id = "5",
                                Language = ProgrammingLanguage.CSharp,
                                Percentage = 60
                            }
                        },
                        Owner = "owner2",
                    },
                    new Project
                    {
                        Id = "3",
                        Name = "Project3",
                        Languages = new List<ProjectProgrammingLanguage>
                        {
                            new ProjectProgrammingLanguage
                            {
                                Id = "6",
                                Language = ProgrammingLanguage.ABAP,
                                Percentage = 40
                            },
                            new ProjectProgrammingLanguage
                            {
                                Id = "7",
                                Language = ProgrammingLanguage.R,
                                Percentage = 20
                            },
                            new ProjectProgrammingLanguage
                            {
                                Id = "8",
                                Language = ProgrammingLanguage.Dockerfile,
                                Percentage = 40
                            }
                        },
                        Owner = "owner3",
                    }
                    
                }
            },
        };
        
        // Act
        var result = LanguageClassifier.GetLanguagesPerEcosystem(ecosystems);
        
        // Assert that the list contains the correct amount of languages
        Assert.That(result.Count, Is.EqualTo(5));
        // Assert that the list contains the correct percentages
        Assert.That(result[0].Percentage, Is.EqualTo(70 + 60));
        // Assert that the whole list is ordered by percentage
        Assert.That(result[0].Percentage, Is.GreaterThan(result[1].Percentage));    
        // Assert that the list contains no duplicates
        Assert.That(result[0].Language, Is.Not.EqualTo(result[1].Language));
    }

    
}