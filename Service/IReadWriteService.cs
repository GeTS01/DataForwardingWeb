using DataForwardingWeb.Domain.Base;

namespace Service
{
    public interface IReadWriteService<DATA, ENTITY> : IReadOnlyService<DATA, ENTITY>
        where DATA : DTO.Data<ENTITY> where ENTITY : PersistentObject
    {


        /**
    * Creating entity
    * @param model object with type {@link MODEL}
    * @return Data object with type {@link DATA} that represents {@link ENTITY} data.
    */
        Task<DTO.Data<ENTITY>> create(DTO.IModel<ENTITY> model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id deleted entity.</param>
        /// <returns>Page of sequence in persistent storage.</returns>
        void remove(long id);

        /**
       * Updating entity
       * @param model
       * @return Data
       */
        DTO.Data<ENTITY> updateInId(long id, DTO.IModel<ENTITY> model);
    }
}