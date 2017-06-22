using System.Collections.Generic;

using restlicsharpdata.restlidata;

namespace restlicsharpclient.restliclient.request.builder
{
    public abstract class SingleEntityRequestBuilder<TRequest, TKey, TEntity> : RequestBuilderBase<TRequest>
        where TRequest : Request where TEntity : RecordTemplate
    {
        public TKey id;
        public TEntity input;

        public SingleEntityRequestBuilder(string baseUrlTemplate) : base(baseUrlTemplate)
        {
            this.id = default(TKey);
            this.input = default(TEntity);
        }
    }
}