using ABCSharedLibrary.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using ABCSharedLibrary.Models.Requests.Identity;

namespace Application.Features.Identity.Roles.Commands
{
    public class UpdateRolePermissionsCommand : IRequest<IResponseWrapper>
    {
        public UpdateRolePermissionsRequest UpdateRolePermissions { get; set; }
    }

    public class UpdateRolePermissionsCommandHandler(IRoleService roleService)
        : IRequestHandler<UpdateRolePermissionsCommand, IResponseWrapper>
    {
        private readonly IRoleService _roleService = roleService;
        public async Task<IResponseWrapper> Handle(UpdateRolePermissionsCommand request, CancellationToken cancellationToken)
        {
            var message = await _roleService.UpdatePermissionsAsync(request.UpdateRolePermissions);
            return await ResponseWrapper.SuccessAsync(message);
        }
    }
}
