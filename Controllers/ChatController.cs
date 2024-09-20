using Google.Cloud.Dialogflow.V2;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CMV.Controllers
{
    [Route("api/chat")]
    public class ChatController : Controller
    {
        private readonly SessionsClient _sessionsClient;
        private readonly string _projectId = "your-project-id";

        public ChatController(SessionsClient sessionsClient)
        {
            _sessionsClient = sessionsClient;
        }

        [HttpPost]
        public async Task<string> Post([FromBody] dynamic body)
        {
            var sessionId = HttpContext.Session.Id;
            var sessionName = SessionName.FromProjectSession(_projectId, sessionId);
            var queryInput = new QueryInput
            {
                Text = new TextInput
                {
                    Text = body.message.ToString(),
                    LanguageCode = "en"
                }
            };
            var response = await _sessionsClient.DetectIntentAsync(sessionName, queryInput);
            var result = response.QueryResult.FulfillmentText;
            return result;
        }
    }
}
