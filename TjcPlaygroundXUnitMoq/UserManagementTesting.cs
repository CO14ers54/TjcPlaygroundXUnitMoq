using TjcPlaygroundXUnitMoqUat;

namespace TjcPlaygroundXUnitMoq
{
    public class UserManagementTesting
    {
        [Fact]
        public void Add_CreateUser()
        {
            // Arrange - Prepare the Data for the test
            var userManagement = new UserManagement();


            // Act - Execute the test code
            userManagement.Add(new("Todd", "Christensen"));

            // Assert - Compare execution results with expected values
            var savedUser = Assert.Single(userManagement.AllUsers);
            Assert.NotNull(savedUser); // Assert the userManagement list is not empty;
            Assert.Equal("Todd", savedUser.firstName);
            Assert.Equal("Christensen", savedUser.lastName);

            Assert.False(savedUser.VerifiedEmail);
        }

        [Fact]
        public void Update_MobileNumber()
        {
            // Arrange
            var userManagement = new UserManagement();

            // Act
            userManagement.Add(new("Todd", "Christensen"));

            var firstUser = userManagement.AllUsers.First();
            firstUser.Phone = "13038701184";

            userManagement.UpdatePhone(firstUser);

            // Assert
            var savedUser = Assert.Single(userManagement.AllUsers);
            Assert.NotNull(savedUser);
            Assert.Equal("13038701184", savedUser.Phone);
        }
    }
}