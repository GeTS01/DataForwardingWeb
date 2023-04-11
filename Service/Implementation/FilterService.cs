using DataForwardingWeb.Domain.Base;
using DataForwardingWeb.DTO.Filter;
using DataForwardingWeb.Repository.Base;
using DTO;
using DTO.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Implementation
{
    public abstract class FilterService<DATA, ENTITY> : IReadOnlyService<DATA, ENTITY> where DATA : Data<ENTITY> where ENTITY : PersistentObject
    {
        private static readonly string[] keywords = { "Min", "Max", "Start", "End", "Items" };
        public abstract IRepository<ENTITY> GetRepository();
        public abstract Data<ENTITY> read(long id);
        public abstract Page<DATA, ENTITY> read(int number, int size);

        public Page<DATA, ENTITY> readFilter(FilterModel<ENTITY> filter, int number, int size)
        {
            Type entityType = typeof(ENTITY);
            var attributes = entityType.CustomAttributes;
            var tableAttribute = attributes.FirstOrDefault(a => a.AttributeType.Equals(typeof(TableAttribute)));
            if (tableAttribute == null)
            {
                throw new Exception("This type is not annotated TableAttribute");
            }
            var tableName = tableAttribute.ConstructorArguments[0];
            var deviceFields = typeof(ENTITY).GetProperties();
            var filterFields = filter.GetType().GetProperties();

            const string AND_STR = " AND ";
            string query = $"SELECT * FROM {tableName} WHERE ";
            if(deviceFields == null || deviceFields.Length == 0) 
            {
                throw new Exception("nothing to filter");
            }
            deviceFields?.ToList().ForEach(deviceField =>
            {
                var foundFields = filterFields.Where(f => f.Name.StartsWith(deviceField.Name)).ToList();
                for (int i = 0; i < foundFields.Count; i++)
                {
                    var ff = foundFields[i];
                    string foundedFieldName = ff.Name;
                    var foundFildValue = ff.GetValue(filter);
                    if (foundFildValue == null)
                    {
                        continue;
                    }
                    string suffix = foundedFieldName.Substring(deviceField.Name.Length);
                    foreach (var keyword in keywords)
                    {
                        if (suffix.StartsWith(keyword))
                        {
                            switch (keyword)
                            {
                                case "Min":
                                    {
                                        long filterValue = (long) ff.GetValue(filter);
                                        var columnAttr = deviceField.CustomAttributes.FirstOrDefault(x => x.AttributeType.Equals(typeof(ColumnAttribute)));
                                        if (columnAttr != null && filterValue != null)
                                        {
                                            query += $"{columnAttr.ConstructorArguments[0].Value} >= {filterValue} AND ";
                                        }
                                    }
                                    break;
                                case "Max":
                                    {
                                        long filterValue = (long)ff.GetValue(filter);
                                        var columnAttr = deviceField.CustomAttributes.FirstOrDefault(x => x.AttributeType.Equals(typeof(ColumnAttribute)));
                                        if (columnAttr != null && filterValue != null)
                                        {
                                            query += $"{columnAttr.ConstructorArguments[0].Value} <= {filterValue} AND ";
                                        }
                                    }
                                    break;
                                case "Start":
                                    {
                                        DateTime filterValue = (DateTime)ff.GetValue(filter);
                                        var columnAttr = deviceField.CustomAttributes.FirstOrDefault(x => x.AttributeType.Equals(typeof(ColumnAttribute)));
                                        if (columnAttr != null && filterValue != null)
                                        {
                                            query += $"{columnAttr.ConstructorArguments[0].Value} >= '{filterValue.ToString("yyyy-MM-dd hh:mm:ss")}' AND ";
                                        }
                                    }
                                    break;
                                case "End":
                                    {
                                        DateTime filterValue = (DateTime)ff.GetValue(filter);
                                        var columnAttr = deviceField.CustomAttributes.FirstOrDefault(x => x.AttributeType.Equals(typeof(ColumnAttribute)));
                                        if (columnAttr != null && filterValue != null)
                                        {
                                            query += $"{columnAttr.ConstructorArguments[0].Value} <= '{filterValue.ToString("yyyy-MM-dd hh:mm:ss")}' AND ";
                                        }
                                    }
                                    break;
                                case "Items":
                                    {
                                        int[]? filterValues = ff.GetValue(filter) as int[];
                                        if (filterValues != null)
                                        {
                                            var resultInStr = String.Join(",", filterValues.AsEnumerable());

                                            var columnAttr = deviceField.CustomAttributes.FirstOrDefault(x => x.AttributeType.Equals(typeof(ColumnAttribute)));
                                            if (columnAttr != null)
                                            {

                                                query += $"{columnAttr.ConstructorArguments[0].Value} IN ({resultInStr}) AND ";
                                            }
                                        }
                                    }
                                    break;
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
            var res = GetRepository()
                .ExecuteSelectSql(query)
                .Skip(number * size)
                .Take(size)
                .ToList()
                .Select(x => createDataInstance(x))
                .ToList();

            var a = new Page<DATA, ENTITY>()
            {
                items = res,
                Number = number,
                Size = size,
                TotalCount = 0,
                TotalPages = 0
            };

            return a;
        }

        private DATA createDataInstance(ENTITY e)
        {
            var data = Activator.CreateInstance(typeof(DATA), e) as DATA;
            return data;
        }
    }
}
