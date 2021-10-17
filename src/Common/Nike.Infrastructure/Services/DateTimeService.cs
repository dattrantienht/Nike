using System;
using Nike.Application.Common.Interfaces;

namespace Nike.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
