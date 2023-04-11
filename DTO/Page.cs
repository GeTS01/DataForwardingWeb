using DataForwardingWeb.Domain.Base;
using DTO.Data;

namespace DTO
{
    public class Page<DATA, ENTITY> where DATA : Data<ENTITY> where ENTITY : PersistentObject
    {
        public long Number { get; set; }
        public long Size { get; set; }

        // все страницы 
        public long TotalPages { get; set; }

        // все элементы
        public long TotalCount { get; set; }

        public List<DATA> items { get; set; }

        public Page()
        {

        }
        public Page(long number, long size, long totalPages, long totalCount, List<DATA> items)
        {
            Number = number;
            Size = size;
            TotalPages = totalPages;
            TotalCount = totalCount;
            this.items = items;
        }
    }
}
