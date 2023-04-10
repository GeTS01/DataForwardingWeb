using DataForwardingWeb.Domain.Base;
using DataForwardingWeb.DTO.Data;

namespace Service
{
    public interface IAccountService<ENTITY> where ENTITY : PersistentObject
    {
        TokenData<ENTITY> authenticate(string username, string password);
    }
}