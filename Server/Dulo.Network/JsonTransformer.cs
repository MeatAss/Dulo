using Dulo.Network.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dulo.Network
{
    public static class JsonTransformer
    {
        public static T DeserializeObject<T>(string message) where T : class
        {
            T model = null;

            try
            {
                model = JsonConvert.DeserializeObject<T>(message);
            }
            catch (Exception)
            {

                return null;
            }

            return model;
        }

        public static string SerializeObject<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
