using System.Collections.Generic;

namespace OngProject.Application.DTOs.Identity
{
    public class Result
    {
        public string Token { get; init; }
        public bool Login { get; init; }
        public IList<string> Errors { get; init; }
    }
}