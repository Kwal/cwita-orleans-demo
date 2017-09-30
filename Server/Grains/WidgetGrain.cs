using Interfaces;
using Interfaces.Models;
using Orleans;
using Serilog;
using System.Threading.Tasks;

namespace Server.Grains
{
    public class WidgetGrain : Grain<Widget>, IWidgetGrain
    {
        public override async Task OnActivateAsync()
        {
            // this could be a call to external storage/system,
            // so let's add some artificial delay
            await Task.Delay(2000);
            
            await base.OnActivateAsync();
        }

        public Task<Widget> Get()
        {
            return Task.FromResult(State);
        }

        public async Task Set(Widget widget)
        {
            State = widget;

            // this could be a call to external storage/system,
            // so let's add some artificial delay
            await Task.Delay(2000);
            
            await WriteStateAsync();
        }
    }
}