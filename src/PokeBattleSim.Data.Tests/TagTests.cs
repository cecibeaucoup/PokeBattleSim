using Xunit;
using PokeBattleSim.Data.Entities;

namespace PokeBattleSim.Data.Tests
{
    public class TagTests
    {
        [Fact]
        public void Tag_Properties()
        {
            var t = new Tag("test", "desc");
            Assert.Equal("test", t.Name);
            Assert.Equal("desc", t.Description);
        }

        [Fact]
        public void Tag_PropertiesCanBeModified()
        {
            var t = new Tag("original", "original desc");
        
            t.Name = "modified";
            t.Description = "modified desc";
        
            Assert.Equal("modified", t.Name);
            Assert.Equal("modified desc", t.Description);
        }

        [Fact]
        public void Tag_EmptyName()
        {
            var t = new Tag("", "empty name");
            Assert.Empty(t.Name);
        }

        [Fact]
        public void Tag_EmptyDescription()
        {
            var t = new Tag("name", "");
            Assert.Empty(t.Description);
        }

        [Fact]
        public void Tag_MultipleTags()
        {
            var tags = new[]
            {
                new Tag("Tag1", "Description 1"),
                new Tag("Tag2", "Description 2"),
                new Tag("Tag3", "Description 3")
            };

            Assert.Equal(3, tags.Length);
            Assert.Equal("Tag1", tags[0].Name);
            Assert.Equal("Tag3", tags[2].Name);
        }

        [Fact]
        public void Tag_LongDescription()
        {
            var longDesc = "This is a very long description that contains lots of information about the tag";
            var t = new Tag("LongTag", longDesc);
        
            Assert.Equal(longDesc, t.Description);
        }
    }
}
