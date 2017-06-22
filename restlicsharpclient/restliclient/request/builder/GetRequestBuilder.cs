using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using restlicsharpdata.restlidata;

namespace restlicsharpclient.restliclient.request.builder
{
    public class GetRequestBuilder<TKey, TEntity> : SingleEntityRequestBuilder<GetRequest<TKey, TEntity>, TKey, TEntity> where TEntity : RecordTemplate
    {
        public GetRequestBuilder(string baseUrlTemplate) : base(baseUrlTemplate) { }

        public override GetRequest<TKey, TEntity> Build()
        {
            return new GetRequest<TKey, TEntity>(headers, id, queryParams, baseUrlTemplate);
        }
    }
}