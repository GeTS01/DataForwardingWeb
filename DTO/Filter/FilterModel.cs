using DataForwardingWeb.Domain.Base;
using DTO;

namespace DataForwardingWeb.DTO.Filter
{
    public abstract class FilterModel<ENTITY> : IModel<ENTITY>, IFilter<ENTITY> where ENTITY : PersistentObject
    {
        public ENTITY toEntity()
        {
            throw new NotImplementedException();
        }

        public ENTITY toEntity(long id)
        {
            throw new NotImplementedException();
        }
    }
}