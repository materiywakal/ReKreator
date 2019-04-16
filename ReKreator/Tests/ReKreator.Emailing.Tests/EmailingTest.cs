using NUnit.Framework;
using ReKreator.Domain;

namespace ReKreator.Emailing.Tests
{
    [TestFixture]
    public class EmailingTest
    {
        private ISender sender;

        [SetUp]
        public void SetUp()
        {
            sender = new Sender("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",
                "godelrekreator+1@gmail.com");
        }

        [TearDown]
        public void TearDown()
        {
            sender = null;
        }

        [Test]
        public void MessageToUser_WhenAllCool_ThenNotNull()
        {
            User user = new User {Email = "godelrekreator+2@gmail.com", FirstName = "name", LastName = "Lastname"};

            Assert.NotNull(sender.MessageToUserAsync(user, string.Empty, "unit test."));
        }

        [Test]
        public void MessageToUser_WhenParamsNullOrEmpty_ThenNotNull()
        {
            Assert.NotNull(sender.MessageToUserAsync((User) null, null, null));
        }

        [Test]
        public void MessageToUsers_WhenAllCool_ThenNotNull()
        {
            User[] users =
            {
                new User {Email = "godelrekreator+2@gmail.com"},
                new User {Email = "godelrekreator+3@gmail.com"}
            };

            Assert.NotNull(sender.MessageToUserAsync(users, string.Empty, "unit test."));
        }

        [Test]
        public void MessageToUsers_WhenParamsNullOrEmpty_ThenNotNull()
        {

            Assert.NotNull(sender.MessageToUserAsync((User[]) null, null, null));
        }
    }
}