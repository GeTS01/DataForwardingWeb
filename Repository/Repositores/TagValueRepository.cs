using DataForwardingWeb.Repository.Base;
using DataForwardingWeb.Repository.Repositores.Interfaces;
using Domain;
using Storage;

namespace DataForwardingWeb.Repository.Repositores
{
    public class TagValueRepository : Repository<TagValue>, ITagValueRepository
    {
        public TagValueRepository(AppDbContext context) : base(context)
        {
        }
    }
}