using DataForwardingWeb.DTO.Data;
using DataForwardingWeb.DTO.Filter;
using DataForwardingWeb.Repository.Repositores.Interfaces;
using Domain;
using DTO;
using Service;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataForwardingWeb.Service.Implementation
{
    public class RequestService : IReadOnlyService<RequestData, Request>
    {
        private readonly IRequestRepository _requestRepository;

        public RequestService(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        // Метод для нахождения записи в базе
        public Data<Request> read(long id)
        {
            var tag = _requestRepository
                .GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return new RequestData(tag);
        }

        //Медтод для нахождения записи в базе в определенном интервале
        public Page<RequestData, Request> read(int number, int size)
        {
            long totalCount = _requestRepository.GetAll().Count();
            var items = _requestRepository.GetAll()
                .Skip(number * size)
                .Take(size)
                .Select(x => new RequestData(x)).ToList()
                ?? new List<RequestData>();
            return new Page<RequestData, Request>(
                number,
                size,
                totalCount / size,
                totalCount,
                items
                );
        }

        string[] keywords = { "Min", "Max", "Start", "End", "Items" };
        public Page<RequestData, Request> readFilter(FilterModel<Request> filter, int number, int size)
        {
            var request = _requestRepository.GetAll();

            var requstFilds = typeof(Request).GetProperties();

            RequestFilterModel? requestFilterModel = filter as RequestFilterModel;
            var filterFilter = requestFilterModel?.GetType().GetProperties();

            const string AND_STR = " AND ";
            string query = "SELECT * FROM request WHERE ";

            requstFilds?.ToList().ForEach(requestField =>
            {
                var foundFields = filterFilter.Where(f => f.Name.StartsWith(requestField.Name)).ToList();
                if (foundFields.Count > 0)
                {
                    for (int i = 0; i < foundFields.Count; i++)
                    {
                        var ff = foundFields[i];
                        string foundedFieldName = ff.Name;
                        if (foundedFieldName == null)
                            return;
                        string suffix = foundedFieldName.Substring(requestField.Name.Length);
                        foreach (var keyword in keywords)
                        {
                            if (suffix.StartsWith(keyword))
                            {
                                switch (keyword)
                                {
                                    case "Min":
                                        {
                                            long filterValue = (long)ff.GetValue(requestFilterModel);
                                            var columnAttr = requestField.CustomAttributes.FirstOrDefault(x => x.AttributeType.Equals(typeof(ColumnAttribute)));
                                            if (columnAttr != null && filterValue != null)
                                            {
                                                query += $"{columnAttr.ConstructorArguments[0].Value} >= {filterValue} AND ";
                                            }
                                        }
                                        break;
                                    case "Max":
                                        {
                                            long filterValue = (long)ff.GetValue(requestFilterModel);
                                            var columnAttr = requestField.CustomAttributes.FirstOrDefault(x => x.AttributeType.Equals(typeof(ColumnAttribute)));
                                            if (columnAttr != null && filterValue != null)
                                            {
                                                query += $"{columnAttr.ConstructorArguments[0].Value} <= {filterValue} AND ";
                                            }
                                        }
                                        break;
                                    case "Start":
                                        {
                                            DateTime filterValue = (DateTime)ff.GetValue(requestFilterModel);
                                            var columnAttr = requestField.CustomAttributes.FirstOrDefault(x => x.AttributeType.Equals(typeof(ColumnAttribute)));
                                            if (columnAttr != null && filterValue != null)
                                            {
                                                query += $"{columnAttr.ConstructorArguments[0].Value} >= '{filterValue.ToString("yyyy-MM-dd hh:mm:ss")}' AND ";
                                            }
                                        }
                                        break;
                                    case "End":
                                        {
                                            DateTime filterValue = (DateTime)ff.GetValue(requestFilterModel);
                                            var columnAttr = requestField.CustomAttributes.FirstOrDefault(x => x.AttributeType.Equals(typeof(ColumnAttribute)));
                                            if (columnAttr != null && filterValue != null)
                                            {
                                                query += $"{columnAttr.ConstructorArguments[0].Value} <= '{filterValue.ToString("yyyy-MM-dd hh:mm:ss")}' AND ";
                                            }
                                        }
                                        break;
                                    case "Items":
                                        {
                                            var filterValues = (int[])ff.GetValue(requestFilterModel);
                                            var resultInStr = String.Join(",", filterValues.AsEnumerable());

                                            var columnAttr = requestField.CustomAttributes.FirstOrDefault(x => x.AttributeType.Equals(typeof(ColumnAttribute)));
                                            if (columnAttr != null)
                                            {

                                                query += $"{columnAttr.ConstructorArguments[0].Value} IN ({resultInStr}) AND ";
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
            });
            if (query.EndsWith(AND_STR))
            {
                query = query.Substring(0, query.Length - AND_STR.Length);
            }
            Console.WriteLine(query);
            var res = _requestRepository.ExecuteSelectSql(query)
                .Select(x => new RequestData(x)).ToList()
                ?? new List<RequestData>();

            return new Page<RequestData, Request>()
            {
                Size = res.Count,
                items = res,
                Number = res.Count,
                TotalCount = res.Count,
                TotalPages = res.Count
            };
        }

        //Метод для получения всех устройств созданных в заданном диапазоне времени
        public List<RequestData> GetByInterval(DateTime? startDate, DateTime? endDate)
        {
            var result = _requestRepository
                .GetAll()
                .Select(x => new RequestData(x))
                .ToList();


            if (startDate > endDate)
                throw new Exception("TimeError");

            if (startDate == null && endDate != null)
            {
                startDate = endDate;
                return GetRequestList(startDate, endDate, result);
            }
            if (endDate == null && startDate != null)
            {
                endDate = startDate;
                return GetRequestList(startDate, endDate, result);
            }
            if (startDate == null && endDate == null)
            {
                return result;
            }
            if (startDate != null && endDate != null)
            {
                return GetRequestList(startDate, endDate, result);
            }

            return null;
        }

        //Вспомогательный функция для  метода GetByInterval
        public static List<RequestData> GetRequestList(DateTime? startDate, DateTime? endDate, List<RequestData> requests)
        {
            return requests
                .Where(x => x.CreatedAt >= startDate && x.CreatedAt <= endDate)
                .ToList();
        }
    }
}
