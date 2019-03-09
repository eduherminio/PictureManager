using Xunit;

namespace PictureManager.Api.Test.Utils
{
    /// <summary>
    /// Test group definition class.
    /// </summary>
    [CollectionDefinition(Name)]
    public class PictureManagerTestCollection : ICollectionFixture<Fixture>
    {
        public const string Name = "PictureManagerTestCollection";
    }
}
