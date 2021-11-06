using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using OngProject.Application.Interfaces;

namespace OngProject.Services{
    public class CurrentUserService
    {
        public string userId {get; set;}
        public string userName{get; set;}
        public string userRole {get; set;}
        
    }
}