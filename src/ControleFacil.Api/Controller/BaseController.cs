using System.Security.Claims;
using ControleFacil.Api.Contract;
using Microsoft.AspNetCore.Mvc;

namespace ControleFacil.Api.Controller
{
    public abstract class BaseController : ControllerBase
    {
        protected long ObterIdUsuarioLogado()
        {
            var id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            long.TryParse(id, out long idUsuario);
            return idUsuario;
        }

        protected ModelErrorContract RetornarModelBadRequest(Exception ex)
        {
            return new ModelErrorContract { Title = "Bad Request",
                                            StatusCode = 400,
                                            DateTime = DateTime.Now,
                                            Message = ex.Message
            };
        }

        protected ModelErrorContract RetornarModelNotFound(Exception ex)
        {
            return new ModelErrorContract { Title = "Not Found",
                                            StatusCode = 404,
                                            DateTime = DateTime.Now,
                                            Message = ex.Message
            };
        }

        protected ModelErrorContract RetornarModelUnauthorized(Exception ex)
        {
            return new ModelErrorContract { Title = "Unauthorized",
                                            StatusCode = 401,
                                            DateTime = DateTime.Now,
                                            Message = ex.Message
            };
        }
        protected ModelErrorContract RetornarModelInternalErrorServer(Exception ex)
        {
            return new ModelErrorContract { Title = "Internal Server Error",
                                            StatusCode = 500,
                                            DateTime = DateTime.Now,
                                            Message = "Ocorreu um erro inesperado no nosso sistema, " +
                                             "nossos engenheiros já foram notificados, " +
                                             "por favor, tente mais tarde."
            };
        }
    }
}