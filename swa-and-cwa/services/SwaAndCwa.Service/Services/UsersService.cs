using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using SwaAndCwa.Service.Repsitories;

namespace SwaAndCwa.Services;

public class UsersService : UsersSrv.UsersSrvBase
{

    private readonly IUserRepository _rep;
    public UsersService(
        IUserRepository rep
    ) {
        _rep = rep;
    }

    public override async Task<Users> GetUsers(Empty request, ServerCallContext context)
    {
        var res = await _rep.GetDatasAsync();
        var resUsers = new Users();
        var adjustdata = res.Select(x => new User { Id = x.Id, Name = x.Name });
        resUsers.Item.AddRange(adjustdata.ToArray());
        return resUsers;
    }

    public override async Task<User> GetUser(IdRequest req, ServerCallContext context)
    {
        var res = await _rep.GetDataAsync(req.Id);
        return new User {
            Id = res.Id,
            Name = res.Name
        };
    }
}