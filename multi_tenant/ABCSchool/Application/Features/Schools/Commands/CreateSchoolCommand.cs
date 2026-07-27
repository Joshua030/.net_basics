using Application.Pipelines;
using ABCSharedLibrary.Wrappers;
using Domain.Entities;
using Mapster;
using MediatR;
using ABCSharedLibrary.Models.Requests.Schools;

namespace Application.Features.Schools.Commands
{
    public class CreateSchoolCommand : IRequest<IResponseWrapper>, IValidateMe
    {
        public CreateSchoolRequest CreateSchool { get; set; }
    }

    public class CreateSchoolCommandHandler : IRequestHandler<CreateSchoolCommand, IResponseWrapper>
    {
        private readonly ISchoolService _schoolService;

        public CreateSchoolCommandHandler(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        public async Task<IResponseWrapper> Handle(CreateSchoolCommand request, CancellationToken cancellationToken)
        {
            var newSchool = request.CreateSchool.Adapt<School>();
            var schoolId = await _schoolService.CreateAsync(newSchool);

            return await ResponseWrapper<int>.SuccessAsync(data: schoolId, "school created successfully");
        }
    }

}
