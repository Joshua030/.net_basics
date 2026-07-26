using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Schools
{
    public interface ISchoolService
    {
        Task<int> CreateAsync(School school);
        Task<int> UpdateAsync(School school);
        Task<int> DeleteAsync(School school);
        Task<School> GetByIdlAsync(int schoolId);
        Task<List<School>> GetAllAsync();

        Task<School> GetByNameAsync(string name);
    }
}
