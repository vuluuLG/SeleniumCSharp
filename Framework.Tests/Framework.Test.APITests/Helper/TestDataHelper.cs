using Framework.Test.Common.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Test.APITest.Helper
{
    public static class TestDataHelper
    {
        public static string GetTestData(string tesName)
        {
            string fileName = string.Empty;
            string fileContent = string.Empty;
            string baseDir = Directory.GetCurrentDirectory() + "\\TestData\\";

            try
            {
                fileName = Directory.GetFiles(baseDir, tesName + ".json", SearchOption.AllDirectories).First();
                fileContent = FileHelper.ReadAllTextFromFile(fileName);
            }
            catch (Exception) { }

            return fileContent;
        }

        public static T GetTestData<T>(string testName)
        {
            string content = GetTestData(testName);
            return JsonHelper.Default.Deserialize<T>(content);
        }
    }
}
