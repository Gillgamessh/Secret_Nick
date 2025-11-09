using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.Gift;
using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.User;

namespace Epam.ItMarathon.ApiService.Infrastructure.Database.Models.Room.Extensions
{
    internal static class RoomExtensions
    {
        public static RoomEf SyncRoom(this RoomEf trackedRoom, RoomEf updatedRoom)
        {
            var updatedTime = DateTime.UtcNow;

            trackedRoom.ClosedOn = updatedRoom.ClosedOn;
            trackedRoom.AdminId = updatedRoom.AdminId;
            trackedRoom.ModifiedOn = updatedTime;
            trackedRoom.MinUsersLimit = updatedRoom.MinUsersLimit;
            trackedRoom.MaxUsersLimit = updatedRoom.MaxUsersLimit;
            trackedRoom.MaxWishesLimit = updatedRoom.MaxWishesLimit;
            trackedRoom.Name = updatedRoom.Name;
            trackedRoom.Description = updatedRoom.Description;
            trackedRoom.InvitationNote = updatedRoom.InvitationNote;
            trackedRoom.GiftExchangeDate = updatedRoom.GiftExchangeDate;
            trackedRoom.GiftMaximumBudget = updatedRoom.GiftMaximumBudget;
            trackedRoom.Users = SyncUsersInRoom(trackedRoom.Users, updatedRoom.Users, updatedTime);

            return trackedRoom;
        }

        private static ICollection<UserEf> SyncUsersInRoom(ICollection<UserEf> trackedUsers,
            ICollection<UserEf> updatedUsers, DateTime updatedTime)
        {
            var trackedDict = trackedUsers.ToDictionary(user => user.Id);
            var updatedDict = updatedUsers.ToDictionary(user => user.Id);

            var toRemove = trackedDict.Keys.Except(updatedDict.Keys).ToList();
            foreach (var id in toRemove)
            {
                trackedUsers.Remove(trackedDict[id]);
            }

            var newUsers = updatedUsers.Where(u => u.Id == 0).ToList();
            foreach (var user in newUsers)
            {
                trackedUsers.Add(user);
            }

            foreach (var updatedUser in updatedUsers.Where(user => user.Id != 0))
            {
                if (!trackedDict.TryGetValue(updatedUser.Id, out var trackedUser))
                {
                    continue;
                }

                trackedUser.ModifiedOn = updatedTime;
                trackedUser.FirstName = updatedUser.FirstName;
                trackedUser.LastName = updatedUser.LastName;
                trackedUser.Phone = updatedUser.Phone;
                trackedUser.DeliveryInfo = updatedUser.DeliveryInfo;
                trackedUser.GiftRecipientUserId = updatedUser.GiftRecipientUserId;
                trackedUser.Email = updatedUser.Email;
                trackedUser.WantSurprise = updatedUser.WantSurprise;
                trackedUser.Interests = updatedUser.Interests;
                trackedUser.Wishes = SyncUserWishes(trackedUser.Wishes, updatedUser.Wishes, updatedTime);
            }

            return trackedUsers;
        }

        private static ICollection<GiftEf> SyncUserWishes(ICollection<GiftEf> trackedWishes,
            ICollection<GiftEf> updatedWishes, DateTime updatedTime)
        {
            var comparer = StringComparer.OrdinalIgnoreCase; // or Ordinal if case-sensitive
            // Delete entities which not present in updated
            var toRemove = trackedWishes
                .Where(trackedWish => !updatedWishes.Any(updatedWish =>
                    comparer.Equals(updatedWish.Name ?? "", trackedWish.Name ?? "") &&
                    comparer.Equals(updatedWish.InfoLink ?? "", trackedWish.InfoLink ?? "")))
                .ToList();

            foreach (var remove in toRemove)
            {
                trackedWishes.Remove(remove);
            }

            // Update existing one
            foreach (var updatedWish in updatedWishes)
            {
                var existing = trackedWishes.FirstOrDefault(tracked =>
                    comparer.Equals(tracked.Name ?? "", updatedWish.Name ?? "") &&
                    comparer.Equals(tracked.InfoLink ?? "", updatedWish.InfoLink ?? ""));

                if (existing != null)
                {
                    existing.ModifiedOn = updatedTime;
                }
                else
                {
                    trackedWishes.Add(updatedWish);
                }
            }

            return trackedWishes;
        }
    }
}