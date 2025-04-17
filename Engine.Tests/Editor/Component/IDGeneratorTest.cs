using VisualNovelEngine.Engine.Editor.Component;

namespace VisualNovelEngine.Engine.Tests.Editor.Component
{
    [TestClass]
    public class IDGeneratorTest
    {
        /// <summary>
        /// Test the creation of an IDGenerator object.
        /// </summary>
        [TestMethod]
        public void IDGeneratorCreationTest()
        {
            IDGenerator idGenerator = new(0);
            Assert.IsNotNull(idGenerator);
        }
        /// <summary>
        /// Test the ID generation functionality of the IDGenerator object.
        /// </summary>
        [TestMethod]
        public void UniqueIDTest()
        {
            IDGenerator idGenerator = new(0);
            int id1 = idGenerator.GenerateID();
            int id2 = idGenerator.GenerateID();
            Assert.AreNotEqual(id1, id2);
        }
        /// <summary>
        /// Test the ID generation functionality of the IDGenerator object with a starting index.
        /// </summary>
        [TestMethod]
        public void IDGeneratorStartingIndexTest()
        {
            int startingIndex = 5;
            IDGenerator idGenerator = new(startingIndex);
            Assert.AreEqual(startingIndex, idGenerator.ID);
        }
    }
}