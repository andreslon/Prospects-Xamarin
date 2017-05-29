using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prospects.Cross.Infrastructure.Resources
{
    public static class LocalizedStrings
    {
        public static string Get(string key)
        {
            return AppResources.ResourceManager.GetString(key);
        }
    }
}
