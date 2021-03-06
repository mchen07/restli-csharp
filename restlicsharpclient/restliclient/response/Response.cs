﻿/*
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
using System.Linq;

namespace restlicsharpclient.restliclient.response
{
    /// <summary>
    /// Representation of a rest response.
    /// <para>Returned by an instance of the RestResponseDecoder interface.</para>
    /// </summary>
    public abstract class Response
    {
        public IReadOnlyDictionary<string, IReadOnlyList<string>> headers;
        public int status;

        public Response(Dictionary<string, List<string>> headers, int status)
        {
            this.headers = headers.ToDictionary(_ => _.Key, _ => (IReadOnlyList<string>)_.Value.ToList());
            this.status = status;
        }
    }
}
