using Orleans;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDevicesGrain : IGrainWithIntegerKey
    {
        Task Update(string deviceId, double longitude, double latitude);

        Task<Tuple<double, double>> GetLocation(string deviceId);
    }
}