using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Extensions
{
    public static class OrderQueryBuilder
    {
        public static String CreateOrderQuery<T>(string orderByQueryString)
        {
            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            StringBuilder orderQueryBuilder = new StringBuilder();


            // title ascending, price descending, id ascending,
            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param)) // hic bir deger girilmemis diger parametreye gec
                {
                    continue;
                }

                var propertyFromQueryName = param.Split(' ')[0]; // Title, id gibi seyler geliyor buraya
                var objectProperty = propertyInfos.FirstOrDefault(propInfo => propInfo.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty is null) // var olmayan bir property sorgusu olmus olmali diger paramatreye gec
                {
                    continue;
                }

                string orderType = param.EndsWith(" desc") ? "descending" : "ascending";
                string myQuery = $"{objectProperty.Name} {orderType}"; // such as; title desc

                orderQueryBuilder.Append(myQuery);
            }

            string orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            return orderQuery;
        }
    }
}
