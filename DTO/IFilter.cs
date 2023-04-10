using DataForwardingWeb.Domain.Base;


namespace DTO
{
    public interface IFilter<ENTITY> where ENTITY : PersistentObject
    {
    }
}
