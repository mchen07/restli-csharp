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

using System;
using System.Collections.Generic;
using System.Linq;

using restlicsharpdata.restlidata;
using restlicsharpclient.restliclient.transport;
using restlicsharpclient.restliclient.util;
using com.linkedin.restli.common;

namespace restlicsharpclient.restliclient.response.decoder
{
    public class CollectionResponseDecoder<TEntity, TMeta> : RestResponseDecoder<CollectionResponse<TEntity, TMeta>>
        where TEntity : RecordTemplate
        where TMeta : RecordTemplate
    {
        public CollectionResponse<TEntity, TMeta> DecodeResponse(TransportResponse transportResponse)
        {
            List<TEntity> elements;
            CollectionMetadata paging = null;
            TMeta metadata = null;

            object output;
            
            // Decode elements
            if (transportResponse.data.TryGetValue("elements", out output))
            {
                try
                {
                    elements = (output as List<object>)
                        .Select(element => DataUtil.BuildRecord<TEntity>((Dictionary<string, object>)element))
                        .ToList();
                }
                catch
                {
                    throw new InvalidCastException("Error decoding elements from TransportResponse.");
                }
            }
            else
            {
                throw new KeyNotFoundException("Could not find elements in TransportResponse.");
            }

            // Decode paging (optional)
            if (transportResponse.data.TryGetValue("paging", out output))
            {
                try
                {
                    paging = DataUtil.BuildRecord<CollectionMetadata>((Dictionary<string, object>)output);
                }
                catch
                {
                    throw new InvalidCastException("Error decoding paging data from TransportResponse.");
                }
            }

            // Decode metadata (optional)
            if (transportResponse.data.TryGetValue("metadata", out output))
            {
                try
                {
                    metadata = DataUtil.BuildRecord<TMeta>((Dictionary<string, object>)output);
                }
                catch
                {
                    throw new InvalidCastException("Error decoding metadata from TransportResponse.");
                }
            }

            return new CollectionResponse<TEntity, TMeta>(transportResponse.responseHeaders, transportResponse.status ?? 200, elements, paging, metadata);
        }
    }
}
