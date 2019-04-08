using System;
using Microsoft.Extensions.Configuration;

namespace myAbpBasic.MicroService.Core.Consul
{
    public class ServiceEntity
    {

        public ServiceEntity(IConfiguration configuration)
        {
            IP = configuration["Service:IP"];
            Port = Convert.ToInt32(configuration["Service:Port"]);
            ServiceName = configuration["Service:Name"];
            ConsulIP = configuration["Consul:IP"];
            ConsulPort = Convert.ToInt32(configuration["Consul:Port"]);
        }

        public string IP { get; set; }
        public int Port { get; set; }
        public string ServiceName { get; set; }
        public string ConsulIP { get; set; }
        public int ConsulPort { get; set; }
    }
}
