using System.IO;
using NUnit.Framework;

namespace UnitTests
{
    [SetUpFixture]
    public class TestFixture
    {
        /// <summary>
        /// Path to the Web Root.
        /// </summary>
        public static string DataWebRootPath = "./wwwroot";

        /// <summary>
        /// Path to the data folder for the content.
        /// </summary>
        public static string DataContentRootPath = "./data/";

        /// <summary>
        /// Runs this code once when the test harness starts up.
        /// </summary>
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            // Run this code once when the test harness starts up.

            // This will copy over the latest version of the database files

            var DataWebPath = "../../../../src/bin/Debug/net6.0/wwwroot/data";
            var DataUTDirectory = "wwwroot";
            var DataUTPath = DataUTDirectory + "/data";

            // Delete the Destination folder
            if (Directory.Exists(DataUTDirectory))
            {
                Directory.Delete(DataUTDirectory, true);
            }

            // Make the directory
            Directory.CreateDirectory(DataUTPath);

            // Copy over all data files
            var filePaths = Directory.GetFiles(DataWebPath);
            foreach (var filename in filePaths)
            {
                string OriginalFilePathName = filename.ToString();
                var newFilePathName = OriginalFilePathName.Replace(DataWebPath, DataUTPath);

                File.Copy(OriginalFilePathName, newFilePathName);
            }
        }

        /// <summary>
        /// Runs after all the tests have been executed.
        /// </summary>
        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
        }
    }
}
