using System;

namespace Prospects.Cross.Infrastructure.Interfaces
{
    public interface INetworkService
    {
        bool IsNetworkAvailable { get; }

    }
}
