using Entities.Models;
using Services.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DataShaper<T> : IDataShaper<T> where T : class
    {
        public PropertyInfo[] Properties { get; set; }

        public DataShaper()
        {
            Properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }


        public IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fieldsString)
        {
            var requiredProperties = GetRequiredProperties(fieldsString);
            var datas = FetchDatas(entities, requiredProperties);
            return datas;
        }
        public ExpandoObject ShapeData(T entity, string fieldsString)
        {
            var requiredProperties = GetRequiredProperties(fieldsString);
            var data = FetchDataForEntity(entity, requiredProperties);
            return data;
        }

        private IEnumerable<PropertyInfo> GetRequiredProperties(string fieldsString)
        {
            var requiredFields =  new List<PropertyInfo>();

            if (!string.IsNullOrWhiteSpace(fieldsString))
            {
                var fields = fieldsString.Split(',', StringSplitOptions.RemoveEmptyEntries);

                foreach (var field in fields)
                {
                    Console.WriteLine("Field received : " + field);
                    var property = Properties.FirstOrDefault(propertyInfo => propertyInfo.Name.Equals(field.Trim(), StringComparison.InvariantCultureIgnoreCase));
                    
                    if(property is null)
                    {
                        continue;
                    }

                    requiredFields.Add(property);
                }
            }
            // Eğer fieldsString boş veya null ise, T tipinin tüm public instance property'lerini döndür.
            else
            {
                requiredFields = Properties.ToList();
            }

            return requiredFields;
        }

        private ExpandoObject FetchDataForEntity(T entity, IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedObject = new ExpandoObject();

            foreach (var property in requiredProperties)
            {
                var objectPropertyValue = property.GetValue(entity);
                shapedObject.TryAdd(property.Name, objectPropertyValue);
            }
            return shapedObject;
        }
        private IEnumerable<ExpandoObject> FetchDatas(IEnumerable<T> entities, IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedDatas = new List<ExpandoObject>();
            foreach (var entity in entities)
            {
                var data = FetchDataForEntity(entity, requiredProperties);
                shapedDatas.Add(data);
            }

            return shapedDatas;
        }
    }
}
