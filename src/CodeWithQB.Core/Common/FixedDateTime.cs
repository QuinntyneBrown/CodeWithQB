using CodeWithQB.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System;

namespace CodeWithQB.Core.Common
{
    public class FixedDateTime: IDateTime
    {
        private readonly IConfiguration _configuration;
        public FixedDateTime(IConfiguration configuration)
            => _configuration = configuration;

        public DateTime UtcNow => DateTime.Parse(_configuration["UtcNow"]);
    }
}
