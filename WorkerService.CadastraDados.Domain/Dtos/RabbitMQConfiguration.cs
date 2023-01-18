using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerService.CadastraDados.Domain.Dtos
{
    public class RabbitMQConfiguration
    {
        public string HOST_MQ { get; set; }
        public string PORT_MQ { get; set; }
        public string USER_NAME_MQ { get; set; }
        public string PWD_MQ { get; set; }
        public string CLIENT_PROVIDER_MQ { get; set; }
        public string EXCHANGE_NAME_MQ { get; set; }
        public string ROUTE_MQ { get; set; }
        public string CADASTRAR_DADOS { get; set; }
    }
}
