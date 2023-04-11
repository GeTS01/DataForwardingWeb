using DataForwardingWeb.Domain.Base;
using DTO;
using DTO.Data;

namespace DataForwardingWeb.DTO.Data
{
    public class TokenData<ENTITY> : Data<ENTITY> where ENTITY : PersistentObject
    {
        private readonly string token;
        private readonly DateTime expiredAt;
        private readonly Data<ENTITY> date;
        public TokenData(ENTITY entity, string token, DateTime expiredAt, Data<ENTITY> date) : base(entity)
        {
            this.token = token;
            this.expiredAt = expiredAt;
            this.date = date;
        }
        public string getToken()
        {
            return this.token;
        }
        public Data<ENTITY> getDate()
        {
            return this.date;
        }
    }
}