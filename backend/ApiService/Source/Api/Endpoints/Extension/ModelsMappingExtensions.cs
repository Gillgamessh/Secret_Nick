using AutoMapper;
using Epam.ItMarathon.ApiService.Domain.Entities.User;

namespace Epam.ItMarathon.ApiService.Api.Endpoints.Extension;

internal static class ModelsMappingExtensions
{
    internal static void SetUserMappingOptions(this IMappingOperationOptions mappingOptions, List<User> users,
        string userAuthCode)
    {
        var authUser = users.First(user => user.AuthCode == userAuthCode);
        mappingOptions.Items["OwnerCode"] = userAuthCode;
        mappingOptions.Items["AuthAsAdmin"] = authUser.IsAdmin;
        mappingOptions.Items["GiftRecipientUserId"] = authUser.GiftRecipientUserId;
    }
}