using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;
using Epam.ItMarathon.ApiService.Domain.Builders;
using Epam.ItMarathon.ApiService.Domain.Shared.ValidationErrors;
using FluentAssertions;

namespace Epam.ItMarathon.ApiService.Domain.Tests.AggregateTests
{
    /// <summary>
    /// Unit tests for the <see cref="Room"/> aggregate.
    /// </summary>
    public class RoomTests
    {
        /// <summary>
        /// Tests that drawing a room returns BadRequestError when there are not enough users.
        /// </summary>
        [Fact]
        public void Draw_ShouldReturnFailure_WhenNotEnoughUsers()
        {
            // Arrange
            var room = new RoomBuilder()
                .WithName("Test Room")
                .WithDescription("Test Room")
                .WithMinUsersLimit(2)
                .WithGiftExchangeDate(DateTime.UtcNow.AddDays(1))
                .AddUser(userBuilder => userBuilder
                    .WithFirstName("Jone")
                    .WithLastName("Doe")
                    .WithDeliveryInfo("Some info...")
                    .WithPhone("+380000000000")
                    .WithId(1)
                    .WithWantSurprise(true)
                    .WithInterests("Some interests...")
                    .WithWishes([]))
                .Build();

            // Act
            var result = room.Value.Draw();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<BadRequestError>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("room.MinUsersLimit"));
        }

        /// <summary>
        /// Tests that drawing a room returns BadRequestError when the room is already closed.
        /// </summary>
        [Fact]
        public void Draw_ShouldReturnFailure_WhenRoomIsAlreadyClosed()
        {
            // Arrange
            var room = new RoomBuilder()
                .WithName("Test Room")
                .WithDescription("Test Room")
                .WithMinUsersLimit(0)
                .WithGiftExchangeDate(DateTime.UtcNow.AddDays(1))
                .WithShouldBeClosedOn(DateTime.UtcNow)
                .Build();

            // Act
            var result = room.Value.Draw();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<BadRequestError>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("room.ClosedOn"));
        }

        /// <summary>
        /// Tests that drawing a room successfully assigns gift recipients to users.
        /// </summary>
        /// <param name="usersToGenerate">The number of users to generate for the test.</param>
        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(100)]
        [InlineData(1000)]
        [InlineData(5000)]
        public void Draw_ShouldAssignGiftRecipients_WhenSuccessful(ulong usersToGenerate)
        {
            // Arrange
            var roomBuilder = new RoomBuilder()
                .WithName("Test Room")
                .WithDescription("Test Room")
                .WithMinUsersLimit(3)
                .WithMaxUsersLimit((uint)usersToGenerate)
                .WithGiftExchangeDate(DateTime.UtcNow.AddDays(1));

            for (ulong id = 1; id <= usersToGenerate; id++)
            {
                roomBuilder.AddUser(userBuilder => userBuilder
                    .WithFirstName("Jone")
                    .WithLastName("Doe")
                    .WithDeliveryInfo("Some info...")
                    .WithPhone("+380000000000")
                    .WithId(id)
                    .WithWantSurprise(true)
                    .WithInterests("Some interests...")
                    .WithWishes([]));
            }

            var room = roomBuilder.Build();

            // Act
            var result = room.Value.Draw();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.ClosedOn.Should().NotBeNull();
            result.Value.ClosedOn.Should().BeOnOrBefore(DateTime.UtcNow);
            result.Value.Users.Should().OnlyHaveUniqueItems(u => u.GiftRecipientUserId);
            result.Value.Users.Should()
                .NotContain(u => u.GiftRecipientUserId == u.Id); // Ensure no user is assigned to themselves
        }
    }
}