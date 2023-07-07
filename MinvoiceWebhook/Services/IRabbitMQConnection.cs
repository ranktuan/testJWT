using MinvoiceWebhook.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Drawing;
using System.Text;

namespace MinvoiceWebhook.Services
{
    public interface IRabbitMQConnection
    {
        IConnection CreateConnection();
        void PushMessage(MessageMOD message);

        
    }
    public interface ITokenService
    {
        string GenerateToken(string username, string password);
    }


}
