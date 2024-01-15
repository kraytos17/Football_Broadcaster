using Gc_Broadcasting_Api.Models;

namespace Gc_Broadcasting_Api.Interfaces;

public interface IAdminRepo {
    Task<bool> CreateAdmin(Admin? admin);
    Task<bool> Login(string username, string? password);
}
