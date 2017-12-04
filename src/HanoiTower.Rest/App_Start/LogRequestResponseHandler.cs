using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using log4net;

namespace HanoiTower.Rest
{
    public class LogRequestResponseHandler : DelegatingHandler
    {
        private static readonly ILog Logger = LogManager.GetLogger("HanoiLogger");
        
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var requestBody = await request.Content.ReadAsStringAsync();
                Logger.Info(string.IsNullOrEmpty(requestBody) ? "No request body" : requestBody);
            
            var result = await base.SendAsync(request, cancellationToken);

            if (result.Content != null)
            {
                var responseBody = await result.Content.ReadAsStringAsync();
                Logger.Debug($"response: {responseBody}");
            }

            return result;
        }
    }
}