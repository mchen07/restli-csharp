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

using restlicsharpdata.restlidata;
using restlicsharpclient.restliclient.response;
using restlicsharpclient.restliclient.util;

namespace restlicsharpclient.restliclient.request.builder
{
    public abstract class PagingRequestBuilder<TRequest, TEntity, TMeta> : RequestBuilderBase<TRequest, CollectionResponse<TEntity, TMeta>>
        where TRequest : Request<CollectionResponse<TEntity, TMeta>>
        where TEntity : class, RecordTemplate
        where TMeta : class, RecordTemplate
    {
        public PagingRequestBuilder(string baseUrlTemplate) : base(baseUrlTemplate) { }

        public void PaginateStart(int start)
        {
            base.SetParam(RestConstants.kStartParam, start);
        }

        public void PaginateCount(int count)
        {
            base.SetParam(RestConstants.kCountParam, count);
        }
        public void Paginate(int start, int count)
        {
            PaginateStart(start);
            PaginateCount(count);
        }
    }
}