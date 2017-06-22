using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restlicsharpclient.restliclient.response
{
    public class EntityResponse<TEntity> : Response
    {
        public TEntity element;

        public EntityResponse(Dictionary<string, List<string>> headers, int status, TEntity data, bool fromNetwork = true)
            : base(headers, status, fromNetwork)
        {
            this.element = data;
        }
    }
}
