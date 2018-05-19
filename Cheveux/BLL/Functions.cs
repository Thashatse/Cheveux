using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Functions
    {
        public string GetFullProductTypeText(char ProductType)
        {
           /*
            * Given abbreviated char that database returns for product type, 
            * this class will return the full text of that product type
            */
            if (ProductType == 'S')
            {
                return "Service";
            }
            else if (ProductType == 'T')
            {
                return "Treatment";
            }
            else if (ProductType == 'A')
            {
                return "Application Service";
            }
            else
            {
                return "error";
            }
        }
    }
}
