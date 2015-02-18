// <copyright file="TestBase.cs" company="Lyglr.com">Copyright (c) 2014 All Rights Reserved</copyright>

namespace Lyglr.Ordrin.Tests
{
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

    /// <summary>
    /// Base class for testing.
    /// </summary>
    public class TestBase
    {
        static TestBase()
        {
            // Set me!
            DeveloperKey = null;
        }

        /// <summary>
        /// Gets the developer key required for calling the service.
        /// </summary>
        protected static string DeveloperKey { get; private set; }

        /// <summary>
        /// Verify that the developer key is set.
        /// </summary>
        /// <param name="context">Passed test context.</param>
        [TestInitialize]
        public static void TestInit(TestContext context)
        {
            if (string.IsNullOrWhiteSpace(DeveloperKey))
            {
                Assert.Inconclusive("The developer key needs to be populated in TestBase.");
            }
        }
    }
}
