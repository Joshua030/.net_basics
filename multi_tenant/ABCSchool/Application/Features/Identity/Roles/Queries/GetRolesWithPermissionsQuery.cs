using Application.Wrappers;
using MediatR;

namespace Application.Features.Identity.Roles.Queries
{
    public class GetRolesWithPermissionsQuery : IRequest<IResponseWrapper>
    {
        public string RoleId { get; set; }
    }

    public class GetRoleWithPermissionsQueryHandler(IRoleService roleService)
        : IRequestHandler<GetRolesWithPermissionsQuery, IResponseWrapper>
    {
        private readonly IRoleService _roleService = roleService;
        public async Task<IResponseWrapper> Handle(GetRolesWithPermissionsQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleService.GetRoleWithPermissionsAsync(request.RoleId, cancellationToken);
            return await ResponseWrapper<RoleResponse>.SuccessAsync(data: role);
        }
    }
}
