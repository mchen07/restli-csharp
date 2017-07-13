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
using com.linkedin.restli.common;

namespace restlicsharpclient.restliclient.response
{
    /// <summary>
    /// Representation of a Rest.li collection response,
    /// corresponding to any request that returns a collection of entities.
    /// </summary>
    /// <typeparam name="TEntity">Type of entity in the retrieved collection</typeparam>
    /// <typeparam name="TMeta">Type of record used to store the response metadata</typeparam>
    public class CollectionResponse<TEntity, TMeta> : Response
        where TEntity : class, RecordTemplate
        where TMeta : class, RecordTemplate
    {
        public IReadOnlyList<TEntity> elements;
        public CollectionMetadata paging;
        public TMeta metadata;

        public CollectionResponse(Dictionary<string, List<string>> headers, int status, List<TEntity> data, CollectionMetadata paging, TMeta metadata)
            : base(headers, status)
        {
            this.elements = data;
            this.paging = paging;
            this.metadata = metadata;
        }
    }
}
