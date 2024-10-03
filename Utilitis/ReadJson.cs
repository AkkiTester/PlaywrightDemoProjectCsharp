using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PlaywrightDemoProject.Utilitis.ReadJson
{
    public class ReadJson
    {
        private IConfiguration configuration;

        // Constructor to load the configuration from the JSON file
        public ReadJson()
        {
            string CurrentPath = Directory.GetCurrentDirectory();

            // Move back three levels
            string thirdLevelUp = Directory.GetParent(Directory.GetParent(Directory.GetParent(CurrentPath).FullName).FullName).FullName;

            // 1. Define the path to the user data directory (this is where the profile will be stored)
            string testDataPath = Path.Combine(thirdLevelUp, "TestData");

            configuration = new ConfigurationBuilder()
                .SetBasePath(testDataPath)  // Set the base path
                .AddJsonFile("BaseData.json", optional: false, reloadOnChange: true)  // Add JSON file
                .Build();
        }

        // Function to get the value from JSON based on the key
        public string GetValueFromJson(string key)
        {
            // Retrieve the value using the key
            string value = configuration[key];
            return value;
        }

    }
}
