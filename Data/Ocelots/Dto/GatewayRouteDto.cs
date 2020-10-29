using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CodePlus.Blazor.Data.Ocelots.Dto
{
    public class GatewayRouteDto
    {
        [DisplayName("下游HTTP方法")]
        public string DownstreamHttpMethod { get; set; }
        [DisplayName("上游HTTP方法")]
        public string UpstreamHttpMethod { get; set; }
        [DisplayName("上游地址")]
        public string UpstreamPathTemplate { get; set; }
        [DisplayName("下游地址")]
        public string DownstreamPathTemplate { get; set; }
        [DisplayName("下游协议")]
        public string DownstreamScheme { get; set; }
        [DisplayName("下游IP端口")]
        public string DownstreamHostAndPorts { get; set; }
    }
}
