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

namespace restlicsharpclient.restliclient.response
{
    /// <summary>
    /// Representation of a Rest.li error response,
    /// corresponding to any request that returns an error.
    /// If the error is a Rest.li service exception, then the RestliException
    /// object will have the error data in the form of a ErrorResponse wrapped into it.
    /// </summary>
    public class ClientErrorResponse : Response
    {
        public RestliException error { get; }

        public ClientErrorResponse(Dictionary<string, List<string>> headers, int status, RestliException error)
            : base(headers, status)
        {
            this.error = error;
        }
    }
}
