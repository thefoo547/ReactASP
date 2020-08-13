using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.DapperConn.Instructor
{
    public interface IInstructorRepo
    {
        Task<IEnumerable<InstructorModel>> FindAll();
        Task<InstructorModel> GetById(Guid id);
        Task<int> Create(InstructorModel model);
        Task<int> Update(InstructorModel model);
        Task<int> Delete(Guid id);
    }
}
