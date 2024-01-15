using Gc_Broadcasting_Api.Interfaces;
using Gc_Broadcasting_Api.Models;

namespace Gc_Broadcasting_Api.Endpoints;
    public static class AdminEndpoints {
        public static void MapAdminEndpoints(this IEndpointRouteBuilder app) {
            app.MapPost("api/admin", CreateAdmin);
            app.MapPost("api/login", Login);
        }

        public static async Task<IResult> CreateAdmin(Admin? admin, IAdminRepo adminRepo) {
            if (admin is null) { return TypedResults.BadRequest("Admin request body is null/empty.");}
            var res = await adminRepo.CreateAdmin(admin);
            if(!res) {
                return TypedResults.NotFound();
            }
            return TypedResults.Created();
        }

        public static async Task<IResult> Login(string? username, string? password, IAdminRepo adminRepo) {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(username) ||string.IsNullOrWhiteSpace(password)) {
                return TypedResults.BadRequest("Username/Password is either empty or only whitespaces.");
            }
            var res = await adminRepo.Login(username, password);
            if(!res) {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok();
        }
    }