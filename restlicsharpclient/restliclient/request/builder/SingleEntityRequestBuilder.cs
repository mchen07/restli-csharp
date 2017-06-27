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
    /// Asbtract class for constructing Rest.li Requests that entail transaction
    /// of a single entity.
    /// </summary>
    /// <typeparam name="TRequest">The type of Request being constructed</typeparam>
    /// <typeparam name="TResponse">The type of Response expected</typeparam>
    /// <typeparam name="TKey">The key (id) type of the entity</typeparam>
    /// <typeparam name="TEntity">The type of entity</typeparam>
    public abstract class SingleEntityRequestBuilder<TRequest, TResponse, TKey, TEntity> : RequestBuilderBase<TRequest, TResponse>
        where TRequest : Request<TResponse> where TResponse : Response where TKey : IEquatable<TKey> where TEntity : class, RecordTemplate
    {
        /*
         * ID is stored as an object so that regardless of whether TKey is
         * a value type or a reference type, the ID field can be nullable.
         * The SetID(TKey) method is used to safely set the field.
         */
        protected object id { get; private set; }
        public TEntity input { protected get; set; }

        public SingleEntityRequestBuilder(string baseUrlTemplate) : base(baseUrlTemplate)
        {
            id = null;
            input = null;
        }

        public void SetID(TKey id)
        {
            this.id = id;
        }
    }
}