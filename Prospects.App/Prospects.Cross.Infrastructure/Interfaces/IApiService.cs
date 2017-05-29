using Prospects.Cross.Infrastructure.Models.Reponses;
using Prospects.Cross.Infrastructure.Models.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prospects.Cross.Infrastructure.Interfaces
{
    public interface IApiService
    {
        Task<BaseResponse<AuthResponse>> Login(string email, string password);
        Task<BaseResponse<List<ProspectResponse>>> GetProspects(string token);
    }
}
