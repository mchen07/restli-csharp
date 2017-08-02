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

using restlicsharpclient.restliclient.response;

namespace restlicsharpclient.restliclient
{
    /// <summary>
    /// Callback used by the Rest Client in handling the received Response.
    /// </summary>
    /// <typeparam name="TResponse">The expected Response type</typeparam>
    public class RestliCallback<TResponse>
        where TResponse : Response
    {
        public delegate void SuccessHandler(TResponse response);
        public delegate void ErrorHandler(ClientErrorResponse response);

        public SuccessHandler successHandler;
        public ErrorHandler errorHandler;

        public RestliCallback(SuccessHandler successHandler, ErrorHandler errorHandler)
        {
            this.successHandler = successHandler;
            this.errorHandler = errorHandler;
        }

        public void OnSuccess(TResponse response)
        {
            successHandler(response);
        }

        public void OnError(ClientErrorResponse response)
        {
            errorHandler(response);
        }
    }
}
