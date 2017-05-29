using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prospects.Cross.Infrastructure.Interfaces
{
    public interface IToastService
    {
        void ShowToastLong(string message);
        void ShowToastShort(string message); 
    }
}
