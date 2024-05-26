using MudBlazor;
using MudBlazor.Utilities;

namespace WalletWatch.Web.Layout
{
    public class WalletWatchPallete : PaletteDark
    {
        private WalletWatchPallete()
        {
            Primary = new MudColor("#9966FF");
            Secondary = new MudColor("#F6AD31");
            Tertiary = new MudColor("#8AE491");
        }

        public static WalletWatchPallete CreatePallete => new();
    }
}
