using Interfaces;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Server.Grains
{
    public class DevicesGrain : Grain, IDevicesGrain
    {
        private Dictionary<string, Tuple<double, double>> _devices = new Dictionary<string, Tuple<double, double>>(); 

        public Task<Tuple<double, double>> GetLocation(string deviceId)
        {
            if (!_devices.TryGetValue(deviceId, out Tuple<double, double> location))
            {
                location = new Tuple<double, double>(Double.NaN, Double.NaN);
            }

            return Task.FromResult(location);
        }

        public Task Update(string deviceId, double longitude, double latitude)
        {
            _devices[deviceId] = new Tuple<double, double>(longitude, latitude);

            return Task.CompletedTask;
        }
    }
}