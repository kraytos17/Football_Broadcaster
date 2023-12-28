using Gc_Broadcasting_Api.Models;

namespace Gc_Broadcasting_Api.Interfaces;

public interface IAdminRepo {
    Task<Admin> CreateAdmin(Admin admin);
    Task<bool> Login(string username, string password);
}
