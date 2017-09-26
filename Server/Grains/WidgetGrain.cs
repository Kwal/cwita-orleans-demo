using Interfaces;
using Interfaces.Models;
using Orleans;
using Serilog;
using System.Threading.Tasks;

namespace Server.Grains
{
    public class WidgetGrain : Grain, IWidgetGrain
    {
        private Widget _widget;

        public override async Task OnActivateAsync()
        {
            // simulate slowly loading some state from storage
            Log.Logger.Information("Loading widget {Id}", this.GetPrimaryKeyLong());
            await Task.Delay(2000);
            
            _widget = new Widget
            {
                Id = (int)this.GetPrimaryKeyLong(),
                Sku = "123ABC"
            };

            await base.OnActivateAsync();
        }

        public Task<Widget> Get()
        {
            return Task.FromResult(_widget);
        }

        public async Task Set(Widget widget)
        {
            // simulate slowly updating state in storage
            Log.Logger.Information("Updating widget {Id}", this.GetPrimaryKeyLong());
            await Task.Delay(2000);

            _widget = widget;
        }
    }
}