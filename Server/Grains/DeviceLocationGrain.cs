using Interfaces;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Serilog;

namespace Server.Grains
{
    public class DeviceLocationGrain : Grain, IDeviceLocationGrain
    {
        private bool _hasUpdate;
        private double _longitude;
        private double _latitude;

        public override Task OnActivateAsync()
        {
            var initialDelay = new Random().Next(1, 5);
            RegisterTimer(UpdateCurrentLocation, null, TimeSpan.FromSeconds(initialDelay), TimeSpan.FromSeconds(5));
            return base.OnActivateAsync();
        }

        public Task Submit(double longitude, double latitude)
        {
            _longitude = longitude;
            _latitude = latitude;
            _hasUpdate = true;

            return Task.CompletedTask;
        }

        private async Task UpdateCurrentLocation(object state)
        {
            if (_hasUpdate)
            {
                var deviceId = this.GetPrimaryKeyString();
                Log.Logger.Information("Updating {Device} to location {Location}",
                    this.GetPrimaryKeyString(),
                    (_longitude, _latitude)
                );

                var devicesGrain = GrainFactory.GetGrain<IDevicesGrain>(0);
                await devicesGrain.Update(deviceId, _longitude, _latitude);

                _hasUpdate = false;
            }
        }
    }
}