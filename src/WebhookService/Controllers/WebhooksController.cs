
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using RabbitMqService.Constants;
using RabbitMqService.Producers;

namespace SaBooBo.WebhookService.Controllers
{
    [ApiController]
    [Route("api/webhooks")]
    public class AuthZaloController : ControllerBase
    {
        private readonly IRabbitMqProducer _rabbitMqProducer;

        public AuthZaloController(IRabbitMqProducer rabbitMqProducer)
        {
            _rabbitMqProducer = rabbitMqProducer;
        }


        [HttpGet("auth/zalo/callback/{merchantId:guid}")]
        public IActionResult Get(
            Guid merchantId,
            [FromQuery] string? code,
            [FromQuery] string? oa_id
        )
        {
            Console.WriteLine($"[x] {DateTime.Now} Received request");
            Console.WriteLine("[x] code: " + code);
            Console.WriteLine("[x] oa_id: " + oa_id);
            Console.WriteLine("[x] merchantId: " + merchantId);

            // Console write all parameters from http request
            foreach (var key in Request.Query.Keys)
            {
                Console.WriteLine($"[x] Query Name {key} = {Request.Query[key]}");
            }

            Console.WriteLine($"[x] {DateTime.Now} End request.");

            // Publish the message to RabbitMQ 
            _rabbitMqProducer.PublishAsync(
                exchange: string.Empty,
                routingKey: RouteKeys.ZaloOAuthCallback,
                new
                {
                    merchantId,
                    oauthCode = code,
                    oaId = oa_id,
                    sendAt = DateTime.Now
                }
            );

            return Ok(new
            {
                code,
                oa_id,
                DateTime.Now
            });
        }

        [HttpGet("auth/{code}")]
        public IActionResult Post(string code)
        {
            // Hash the code_verifier with SHA-256
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.ASCII.GetBytes(code));

                // Base64 encode the hashed bytes
                string encoded = Convert.ToBase64String(hashedBytes);

                return Ok(new
                {
                    code,
                    encoded,
                    DateTime.Now
                });
            }
        }

    }
}