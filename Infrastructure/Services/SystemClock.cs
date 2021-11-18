using Application.Common.Interfaces;
using System;

namespace Infrastructure.Services
{
    public class SystemClock : ISystemClock
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
