/*
   Copyright (c) 2017 LinkedIn Corp.

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System.Collections.Generic;

using restlicsharpdata.restlidata;
using restlicsharpclient.restliclient.transport;
using restlicsharpclient.restliclient.util;

namespace restlicsharpclient.restliclient.response.decoder
{
    /// <summary>
    /// Class for decoding an entity TransportResponse into an EntityResponse.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to be retrieved</typeparam>
    public class EntityResponseDecoder<TEntity> : RestResponseDecoder<EntityResponse<TEntity>>
        where TEntity : class, RecordTemplate
    {
        public EntityResponse<TEntity> DecodeResponse(TransportResponse transportResponse)
        {
            TEntity record = null;
            Dictionary<string, object> dataMap = transportResponse.data;
            if (dataMap != null)
            {
                record = DataUtil.BuildRecord<TEntity>(dataMap);
            }
            return new EntityResponse<TEntity>(transportResponse.responseHeaders, transportResponse.status ?? 200, record);
        }
    }
}
