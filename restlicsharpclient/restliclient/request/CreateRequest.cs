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
using System;

using restlicsharpdata.restlidata;
using restlicsharpclient.restliclient.response;
using restlicsharpclient.restliclient.response.decoder;
using restlicsharpclient.restliclient.util;

namespace restlicsharpclient.restliclient.request
{
    /// <summary>
    /// Representation of a rest.li CREATE request.
    /// <para>To be constructed using an instance of CreateRequestBuilder.</para>
    /// </summary>
    /// <typeparam name="TKey">The key (id) type of the entity being created</typeparam>
    /// <typeparam name="TEntity">The type of entity being created</typeparam>
    public class CreateRequest<TKey, TEntity> : Request<CreateResponse<TKey, TEntity>>
        where TKey : IEquatable<TKey> where TEntity : class, RecordTemplate
    {
        public CreateRequest(TEntity input, Dictionary<string, List<string>> headers, Dictionary<string, object> queryParams, string baseUrlTemplate)
            : base(ResourceMethod.CREATE, input, headers, queryParams, baseUrlTemplate)
        {
            responseDecoder = new CreateResponseDecoder<TKey, TEntity>();
        }
    }
}