using JobSity.Core.Communications;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace JobSityChat.Controllers
{
    public class MainController : Controller
    {
        protected bool ResponseHasErrors(ResponseResult resposta)
        {
            if (resposta != null && resposta.Errors.Mensagens.Any())
            {
                resposta.Errors.Mensagens.ForEach(x => ModelState.AddModelError(key: string.Empty, errorMessage: x));
                return true;
            }
            return false;
        }
    }
}
