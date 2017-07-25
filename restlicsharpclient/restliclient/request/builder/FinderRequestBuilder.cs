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
using restlicsharpclient.restliclient.util;

namespace restlicsharpclient.restliclient.request.builder
{
    public class FinderRequestBuilder<TEntity, TMeta> : PagingRequestBuilder<FinderRequest<TEntity, TMeta>, TEntity, TMeta>
        where TEntity : RecordTemplate
        where TMeta : RecordTemplate
    {
        private string name;

        public FinderRequestBuilder(string baseUrlTemplate) : base(baseUrlTemplate) { }

        public void Name(string name)
        {
            this.name = name;
            SetParam(RestConstants.kQueryTypeParam, name);
        }

        public override FinderRequest<TEntity, TMeta> Build()
        {
            return new FinderRequest<TEntity, TMeta>(headers, queryParams, name, baseUrlTemplate, pathKeys);
        }
    }
}