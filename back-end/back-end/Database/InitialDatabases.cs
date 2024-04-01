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

using SECODashBackend.Dtos.Ecosystem;

namespace SECODashBackend.Database;

public static class InitialDatabases
{
    public static EcosystemCreationDto agriculture = new EcosystemCreationDto
    {
        EcosystemName = "agriculture",
        Description = "Software related to agriculture",
        Email = "4087568626",
        Topics = 
        [
            "Agriculture", 
            "Farming", 
            "Rural development",
            "Agro-tech",
            "Sustainable practices",
            "Crop analysis",
            "Agricultural innovation",
            "Agri-data",
            "Open-source farming",
            "Smart agriculture",
            "Cows",
            "Livestock" 
        ],
        Technologies = 
        [
            "Internet of Things (IoT)",
            "Machine Learning (ML)",
            "Data Analytics", 
            "Remote Sensing", 
            "Geographic Information Systems (GIS)",
            "Robotics",
            "Sensor Networks",
            "Cloud Computing",
            "Mobile Applications"
        ],
        Excluded = [],
    };
    
    
    public static EcosystemCreationDto artificialintelligence = new EcosystemCreationDto
    {
        EcosystemName = "artificial-intelligence", 
        Description = "Software related to artificial intelligence",
        Email = "4087568626",
        Topics = 
        [
            "Machine Learning",
            "Natural Language Processing",
            "NLP",
            "Computer Vision",
            "Speech Recognition",
            "Reinforcement Learning",
            "Deep Learning",
            "Neural Networks",
            "Explainable AI",
            "Transfer Learning",
            "AI Ethics and Fairness" 
        ],
        Technologies =
        [
            "TensorFlow",
            "PyTorch",
            "Keras",
            "Scikit-learn",
            "OpenCV",
            "Natural Language Toolkit",
            "Apache MXNet",
            "Microsoft Cognitive Toolkit",
            "AI Frameworks and Libraries",
            "AutoML"  
        ],
        Excluded = [],
        
    };
    public static EcosystemCreationDto quantum = new EcosystemCreationDto
    {
        EcosystemName = "quantum", 
        Description = "Software related to quantum mechanics",
        Email = "4087568626",
        Topics = 
        [
            "Quantum Computing",
            "Quantum Algorithms", 
            "Quantum Information Theory",
            "Quantum Cryptography",
            "Quantum Error Correction",
            "Quantum Machine Learning",
            "Quantum Simulations",
            "Quantum Entanglement",
            "Quantum Supremacy",
            "Quantum Networking"
        ],
        Technologies =
        [
            "Quantum Gates",
            "Quantum Circuits",
            "Superconducting Qubits",
            "Trapped Ions",
            "Topological Qubits",
            "Quantum Annealing",
            "Quantum Sensing",
            "Quantum Hardware",
            "Quantum Software Development Kits",
            "Quantum Cloud Platforms"
        ],
        Excluded = [],

    };

}