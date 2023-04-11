using DataForwardingWeb.Domain.Base;


namespace DTO.Data
{
    public abstract class Data<T> where T : PersistentObject
    {
        protected Data(T entity) { }
    }

}
