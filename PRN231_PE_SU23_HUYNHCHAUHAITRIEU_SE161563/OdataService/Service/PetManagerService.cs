using Microsoft.Extensions.Configuration;
using OdataData.Model;
using OdataService.DTO;
using OdataService.Repository;

namespace OdataService.Service
{
    public interface IPetManagerService
    {
        Task<LoginResponse> Login(string email, string password);
    }

    public class PetManagerService : IPetManagerService
    {
        private readonly IRepository<PetShopMember> _repository;
        private readonly IConfiguration _configuration;
        private readonly String _prn231;

        public PetManagerService(IRepository<PetShopMember> repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
            _prn231 = _configuration["PE_PRN231"];
        }

        public async Task<LoginResponse> Login(string email, string password)
        {
            try
            {
                var account = _repository.GetAll()
                    .Where(a => a.EmailAddress.Contains(email) && a.MemberPassword.Equals(password))
                    .FirstOrDefault();

                //login success
                if (account != null && account.MemberRole == 1)
                {
                    var token = AccessTokenManager.GenerateJwtToken(string.IsNullOrEmpty(account.EmailAddress) ? "" : account.EmailAddress, account.MemberRole, account.MemberId, _configuration);
                    return new LoginResponse()
                    {
                        jwtToken = token
                    };
                }

                return new LoginResponse();
            }
            catch 
            {
                throw;
            }
        }
    }
}
