using DataForwardingWeb.Domain.Base;

namespace Service
{
    public interface IFullAccessService<DATA, ENTITY> where DATA : DTO.Data<ENTITY> where ENTITY : PersistentObject
    {
        /// <summary>
        /// Delete entity by id
        /// </summary>
        /// <param name="id">number of reading page.</param>
        /// <returns>Page of sequence in persistent storage.</returns>
        void delete(long id);

        /// <summary>
        /// Forced deletion by id
        /// </summary>
        /// <param name="id">number of reading page.</param>
        /// <returns>Page of sequence in persistent storage.</returns>
        void hardDelete(long id)
        {
            throw new Exception();
        }
    }
}
