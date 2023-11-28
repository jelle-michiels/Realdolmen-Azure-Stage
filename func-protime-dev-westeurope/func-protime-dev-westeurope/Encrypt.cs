using System.Net;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace func_protime_dev_westeurope
{
    public class Encrypt
    {
        private readonly ILogger _logger;
        private const string mysecurityKey = "ProtimeEncryptionKey";

        public Encrypt(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Encrypt>();
        }

        [Function("Encrypt")]
        public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var dataFromBody = await req.ReadAsStringAsync();

            var encryptedData = EncryptData("Welcome to Azure Functions!");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString(encryptedData);

            return response;
        }

        public static string EncryptData(string TextToEncrypt)
        {
            byte[] MyEncryptedArray = UTF8Encoding.UTF8.GetBytes(TextToEncrypt);

            MD5CryptoServiceProvider MyMD5CryptoService = new MD5CryptoServiceProvider();
            byte[] MysecurityKeyArray = MyMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(mysecurityKey));
            MyMD5CryptoService.Clear();

            var MyTripleDESCryptoService = new TripleDESCryptoServiceProvider();
            MyTripleDESCryptoService.Key = MysecurityKeyArray;
            MyTripleDESCryptoService.Mode = CipherMode.ECB;
            MyTripleDESCryptoService.Padding = PaddingMode.PKCS7;

            var MyCrytpoTransform = MyTripleDESCryptoService.CreateEncryptor();
            byte[] MyresultArray = MyCrytpoTransform.TransformFinalBlock(MyEncryptedArray, 0, MyEncryptedArray.Length);
            MyTripleDESCryptoService.Clear();

            return Convert.ToBase64String(MyresultArray, 0, MyresultArray.Length);
        }
    }
}
