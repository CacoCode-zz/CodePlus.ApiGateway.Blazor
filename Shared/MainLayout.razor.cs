using BootstrapBlazor.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodePlus.Blazor.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class MainLayout
    {
        /// <summary>
        ///获得/设置 是否收缩侧边栏
        /// </summary>
        public bool IsCollapsed { get; set; }

        private Task OnCollapsed(bool collapsed)
        {
            IsCollapsed = collapsed;
            return Task.CompletedTask;
        }

        private IEnumerable<MenuItem> GetIconSideMenuItems()
        {
            var ret = new List<MenuItem>
            {
                new MenuItem() { Text = "主页", Icon = "fa fa-tachometer" , Url = "/"},
                new MenuItem() { Text = "网关设置", Icon = "fa fa-sitemap", Url = "/gatewayroute" },
            };
            return ret;
        }
    }
}
