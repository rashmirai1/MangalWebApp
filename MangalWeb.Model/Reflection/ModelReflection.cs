using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangalWeb.Model
{
    public class ModelReflection
    {
        public static void MapObjects(object source, object destination)
        {
            Type sourceType = source.GetType();
            Type destinationType = destination.GetType();

            var sourceProperties = sourceType.GetProperties();
            var destinationProperties = destinationType.GetProperties();

            var commonproperties = from sp in sourceProperties
                                   join dp in destinationProperties on new { sp.Name } equals
                                       new { dp.Name }
                                   select new { sp, dp };

            foreach (var match in commonproperties)
            {
                try
                {
                    match.dp.SetValue(destination, match.sp.GetValue(source, null), null);
                }
                catch (Exception ex)
                {

                }

            }
        }
    }
}
