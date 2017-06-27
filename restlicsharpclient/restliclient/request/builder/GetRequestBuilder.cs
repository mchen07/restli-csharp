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

using restlicsharpdata.restlidata;
using restlicsharpclient.restliclient.response;

namespace restlicsharpclient.restliclient.request.builder
{
    /// <summary>
    /// Class for constructing Rest.li GET Requests.
    /// </summary>
    /// <typeparam name="TKey">The key (id) type of the entity being retrieved</typeparam>
    /// <typeparam name="TEntity">The type of entity being retrieved</typeparam>
    public class GetRequestBuilder<TKey, TEntity> : SingleEntityRequestBuilder<GetRequest<TKey, TEntity>, EntityResponse<TEntity>, TKey, TEntity>
        where TKey : IEquatable<TKey> where TEntity : class, RecordTemplate
    {
        public GetRequestBuilder(string baseUrlTemplate) : base(baseUrlTemplate) { }

        public override GetRequest<TKey, TEntity> Build()
        {
            return new GetRequest<TKey, TEntity>(headers, id, queryParams, baseUrlTemplate);
        }
    }
}