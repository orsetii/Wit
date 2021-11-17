using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wit.Extensions
{
    public static class ContainsExtensions
    {
        public static bool ContainsAnyInList(this String str, List<string> list)
        {
            foreach (var item in list)
            {
                if (str == item)
                    return true;
            }
            return false;
        }
    }
}
