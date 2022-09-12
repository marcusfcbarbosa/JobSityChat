using JobSity.Core.Communications;
using JobSityChat.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace JobSityChat.Services
{
    public interface IAuthService
    {
        Task<UserAnswersLogin> Login(UserLogin userLogin);
        Task<UserAnswersLogin> Register(UserRegister userRegister);
    }

    public class AuthenticationService : Service, IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _settings;
        public AuthenticationService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.IdentityUrl);
        }
        public async Task<UserAnswersLogin> Login(UserLogin userLogin)
        {
            var loginContent = GetContent(userLogin);
            var response = await _httpClient.PostAsync(requestUri: $"/api/identity/authenticate", content: loginContent);
            if (!HandleErrorsResponse(response))
            {
                return new UserAnswersLogin
                {
                    ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }
            return await DeserializarObjetoResponse<UserAnswersLogin>(response);
        }

        public async Task<UserAnswersLogin> Register(UserRegister userRegister)
        {
            var registroContent = GetContent(userRegister);
            var response = await _httpClient.PostAsync(requestUri: $"/api/identity/register", content: registroContent);
            if (!HandleErrorsResponse(response))
            {
                return new UserAnswersLogin
                {
                    ResponseResult = await DeserializarObjetoResponse<ResponseResult>(response)
                };
            }
            return await DeserializarObjetoResponse<UserAnswersLogin>(response);
        }
    }
}
