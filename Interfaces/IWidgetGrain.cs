using Interfaces.Models;
using Orleans;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IWidgetGrain : IGrainWithIntegerKey
    {
        Task<Widget> Get();

        Task Set(Widget widget);
    }
}