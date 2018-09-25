using CodeWithQB.Core.Common;
using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.API.Features.DigitalAssets
{
    public class UploadDigitalAssetCommand
    {
        public class Request : IRequest<Response> { }

        public class Response
        {
            public List<Guid> DigitalAssetIds { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IEventStore _eventStore { get; set; }
            public IHttpContextAccessor _httpContextAccessor { get; set; }
            public Handler(IEventStore eventStore, IHttpContextAccessor httpContextAccessor) {
                _eventStore = eventStore;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var httpContext = _httpContextAccessor.HttpContext;
                var defaultFormOptions = new FormOptions();
                var digitalAssets = new List<DigitalAsset>();

                if (!MultipartRequestHelper.IsMultipartContentType(httpContext.Request.ContentType))
                    throw new Exception($"Expected a multipart request, but got {httpContext.Request.ContentType}");

                var mediaTypeHeaderValue = MediaTypeHeaderValue.Parse(httpContext.Request.ContentType);

                var boundary = MultipartRequestHelper.GetBoundary(
                    mediaTypeHeaderValue,
                    defaultFormOptions.MultipartBoundaryLengthLimit);

                var reader = new MultipartReader(boundary, httpContext.Request.Body);

                var section = await reader.ReadNextSectionAsync();

                while (section != null)
                {                    
                    var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out ContentDispositionHeaderValue contentDisposition);

                    if (hasContentDispositionHeader)
                    {
                        if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                        {
                            using (var targetStream = new MemoryStream())
                            {
                                await section.Body.CopyToAsync(targetStream);
                                var name = $"{contentDisposition.FileName}".Trim(new char[] { '"' }).Replace("&", "and");
                                var bytes = StreamHelper.ReadToEnd(targetStream);
                                var contentType = section.ContentType;

                                _eventStore.Save(new DigitalAsset(name, bytes, contentType));
                            }
                        }
                    }
                    
                    section = await reader.ReadNextSectionAsync();
                }
                
                return new Response()
                {
                    DigitalAssetIds = digitalAssets.Select(x => x.DigitalAssetId).ToList()
                };
            }
        }
    }
}
