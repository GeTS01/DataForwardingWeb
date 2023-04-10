using DataForwardingWeb.Domain.Enum;
using DataForwardingWeb.DTO.Data;
using DataForwardingWeb.DTO.Filter;
using DataForwardingWeb.Repository.Repositores.Interfaces;
using Domain;
using DTO;
using Microsoft.EntityFrameworkCore;
using Service;
using Service.Implementation;

namespace DataForwardingWeb.Service.Implementation
{
    public class TagValuesService : IReadOnlyService<TagValuesData, TagValue>
    {
        private readonly ITagValueRepository _tagValueRepository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly ITagRepository _tagRepository;
      

        public TagValuesService(
            ITagValueRepository tagValueRepository,
            IDeviceRepository deviceRepository,
            ITagRepository tagRepository)
        {
            _tagValueRepository = tagValueRepository;
            _deviceRepository = deviceRepository;
            _tagRepository = tagRepository;
        }



        // Метод для нахождения записи в базе
        public Data<TagValue> read(long id)
        {
            var tagValue = _tagValueRepository
                .GetAll()
                .Include(x => x.Tag)
                .Where(x => x.Id == id)
                .FirstOrDefault()
                ?? throw new Exception($"TagValue with {id} not found");
            return new TagValuesData(tagValue);
        }

        

        //Метод для создания 
        public Data<TagValue> Create(IModel<TagValue> model)
        {
            TagValue tagValue = model.toEntity();
            _tagValueRepository.Create(tagValue);
            var g = _tagValueRepository.SaveChangesAsync();
            return new TagValuesData(tagValue);
        }

        //Медтод для нахождения записи в базе в определенном интервале
        public Page<TagValuesData, TagValue> read(int number, int size)
        {
            long totalCount = _tagValueRepository.GetAll().Count();
            return new Page<TagValuesData, TagValue>(
                number,
                size,
                totalCount / size,
                totalCount,
                _tagValueRepository.GetAll()
                .Skip(number * size)
                .Take(size)
                .Select(x => new TagValuesData(x))
                .ToList()
                ?? new List<TagValuesData>()
                );
        }

        //Метод для получения всех устройств созданных в заданном диапазоне времени
        public List<TagData> GetByNameAndDeviceIdAndType(string? tagName, long? deviceId, MeasureType? type)
        {
            var result = _tagRepository
                .GetAll();
            if (tagName != null)
                result = result.Where(x => x.Name.StartsWith(tagName));
            if (deviceId != null)
                result = result.Where(x => x.DeviceId == deviceId);

            if (type != null)
                result = result.Where(x => x.MeasureType == type);

            return result.Select(x => new TagData(x)).ToList(); ;

        }

        public List<TagValue> GetTagValueOnTag(long tagId)
        {
            var tag = _tagRepository
                .GetAll()
                .Where(x => x.Id == tagId)
                .FirstOrDefault()
            ?? throw new Exception($"TagValue with {tagId} not found");

            var tagValue = _tagValueRepository
                .GetAll()
                .Where(x => x.TagId == tag.Id)
                .ToList();

            return tagValue;

        }

        // Метод получения всех тегов
        public List<Tag> GetAllTag()
        {
            return _tagRepository.GetAll().ToList();
        }

        public Page<TagValuesData, TagValue> readFilter(FilterModel<TagValue> filter, int number, int size)
        {
            throw new NotImplementedException();
        }


    }
}
