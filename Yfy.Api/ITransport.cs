namespace Yfy.Api
{
    using System;
    using System.IO;

    internal interface ITransport
    {
        TResponse SendRpcRequest<TRequest, TResponse>(
            TRequest request,
            Uri uri,
            Func<TRequest, string> decoder = null,
            Func<string, TResponse> encoder = null);

        TResponse SendUploadRequest<TRequest, TResponse>(
            TRequest request,
            Uri uri,
            Stream body,
            Func<TRequest, string> encoder = null,
            Func<string, TResponse> decoder = null);

        void SendDownloadRequest(
            string request,
            Uri uri);
    }
}
