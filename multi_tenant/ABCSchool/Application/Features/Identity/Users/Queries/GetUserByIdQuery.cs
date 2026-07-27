using ABCSharedLibrary.Wrappers;
using MediatR;
using ABCSharedLibrary.Models.Responses.Identity;

namespace Application.Features.Identity.Users.Queries
{
    public class GetUserByIdQuery : IRequest<IResponseWrapper>
    {
        public string UserId { get; set; }
    }

    public class GetByIdQueryHandler : IRequestHandler<GetUserByIdQuery, IResponseWrapper>
    {
        private readonly IUserService _userService;

        public GetByIdQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IResponseWrapper> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByIdAsync(request.UserId, cancellationToken);
            return await ResponseWrapper<UserResponse>.SuccessAsync(data: user);
        }
    }
}
