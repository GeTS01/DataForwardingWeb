using DataForwardingWeb.Domain.Base;

namespace DTO
{
    public interface IModel<ENTITY> where ENTITY : PersistentObject
    {
        ENTITY toEntity();
        ENTITY toEntity(long id);
    }
}