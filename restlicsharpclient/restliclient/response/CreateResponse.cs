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

using restlicsharpdata.restlidata;

namespace restlicsharpclient.restliclient.response
{
    /// <summary>
    /// Representation of a Rest.li CREATE response.
    /// <para>Returned by an instance of the CreateResponseDecoder class.</para>
    /// </summary>
    /// <typeparam name="TEntity">The type of Entity represented by this Response</typeparam>
    public class CreateResponse<TKey, TEntity> : Response
        where TEntity : class, RecordTemplate
    {
        public TKey key;
        public TEntity entity;

        public CreateResponse(Dictionary<string, List<string>> headers, int status, TKey key, TEntity entity)
            : base(headers, status)
        {
            this.key = key;
            this.entity = entity;
        }
    }
}
