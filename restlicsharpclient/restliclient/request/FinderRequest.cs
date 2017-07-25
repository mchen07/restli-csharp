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
using restlicsharpclient.restliclient.response;
using restlicsharpclient.restliclient.response.decoder;
using restlicsharpclient.restliclient.util;

namespace restlicsharpclient.restliclient.request
{
    public class FinderRequest<TEntity, TMeta> : Request<CollectionResponse<TEntity, TMeta>>
        where TEntity : RecordTemplate
        where TMeta : RecordTemplate
    {
        private string methodName;

        public FinderRequest(Dictionary<string, List<string>> headers, Dictionary<string, object> queryParams, string methodName, string baseUrlTemplate, Dictionary<string, object> pathKeys)
            : base(ResourceMethod.FINDER, null, headers, queryParams, baseUrlTemplate, pathKeys)
        {
            this.methodName = methodName;
            responseDecoder = new CollectionResponseDecoder<TEntity, TMeta>();
        }
    }
}