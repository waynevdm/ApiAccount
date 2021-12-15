using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBusiness.Model
{
    public class BusinessResponse
    {
        public IList<string> Errors { get; set; } = new List<string>();

        public bool HasErrors()
        {
            return Errors.Count > 0;
        }

        public object Data;
    }
}
