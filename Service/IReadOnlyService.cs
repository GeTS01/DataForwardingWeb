using DataForwardingWeb.Domain.Base;
using DataForwardingWeb.DTO.Filter;
using DTO;
using DTO.Data;

namespace Service
{
    public interface IReadOnlyService<DATA, ENTITY> where DATA : Data<ENTITY> where ENTITY : PersistentObject
    {
        /// <summary>
        /// Get entity by number and size of page
        /// </summary>
        /// <param name="id">number of reading page.</param>
        /// <returns>Page of sequence in persistent storage.</returns>
        Data<ENTITY> read(long id);

        /// <summary>
        /// Get entity by number and size of page
        /// </summary>
        /// <param name="number">number of reading page.</param>
        /// <param name="size">size of reading page.</param>
        /// <returns>Page of sequence in persistent storage.</returns>
        Page<DATA, ENTITY> read(int number, int size);

        /// <summary>
        /// Get entity by number and size of page
        /// </summary>
        /// <param name="number">number of reading page.</param>
        /// <param name="size">size of reading page.</param>
        /// <returns>Page of sequence in persistent storage.</returns>
        Page<DATA, ENTITY> readFilter(FilterModel<ENTITY> filter, int number, int size);
    }
}