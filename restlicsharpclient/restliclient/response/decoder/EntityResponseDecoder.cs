using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using restlicsharpdata.restlidata;
using restlicsharpclient.restliclient.transport;

namespace restlicsharpclient.restliclient.response.decoder
{
    public class EntityResponseDecoder<TEntity> : RestResponseDecoder<EntityResponse<TEntity>> where TEntity : RecordTemplate
    {
        public EntityResponse<TEntity> DecodeResponse(TransportResponse transportResponse)
        {
            TEntity record = default(TEntity);
            Dictionary<string, object> dataMap = transportResponse.data;
            if (dataMap != null)
            {
                ConstructorInfo constructor = typeof(TEntity).GetConstructor(new[] { typeof(Dictionary<string, object>) });
                record = (TEntity)constructor.Invoke(new object[] { dataMap });
            }
            return new EntityResponse<TEntity>(transportResponse.responseHeaders, transportResponse.status ?? 200, record, transportResponse.fromNetwork);
        }
    }
}
