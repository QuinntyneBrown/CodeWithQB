using CodeWithQB.Core.Interfaces;
using CodeWithQB.Core.Models;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeWithQB.Api.Features.Videos
{
    public class UpsertVideoCommand
    {

        public class Validator: AbstractValidator<Request> {
            public Validator()
            {
                RuleFor(request => request.Video.VideoId).NotNull();
            }
        }

        public class Request : IRequest<Response> {
            public VideoDto Video { get; set; }
        }

        public class Response
        {
            public Guid VideoId { get;set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            public IAppDbContext _context { get; set; }
            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {
                var video = await _context.Videos.FindAsync(request.Video.VideoId);

                if (video == null) {
                    video = new Video();
                    _context.Videos.Add(video);
                }

                video.Name = request.Video.Name;
                video.Description = request.Video.Description;
                video.Url = request.Video.Url;
                video.Abstract = request.Video.Abstract;

                await _context.SaveChangesAsync(cancellationToken);

                return new Response() { VideoId = video.VideoId };
            }
        }
    }
}
