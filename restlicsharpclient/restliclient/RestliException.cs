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

using com.linkedin.restli.common;

namespace restlicsharpclient.restliclient
{
    /// <summary>
    /// Exception class used as a wrapper for any errors caught during rest operations,
    /// as well as to store information created in the case of a Rest.li server
    /// service exception. This class is also used as a flag in certain classes
    /// to indicate that an error has occured.
    /// </summary>
    public class RestliException : Exception
    {
        public ErrorResponse details { get; internal set; }

        public RestliException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
