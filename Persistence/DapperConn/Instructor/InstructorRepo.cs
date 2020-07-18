using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DapperConn.Instructor
{
    public class InstructorRepo : IInstructorRepo
    {
        private readonly IFactoryConnection factoryConnection;

        public InstructorRepo(IFactoryConnection factoryConnection)
        {
            this.factoryConnection = factoryConnection;
        }

        public Task<int> Create(InstructorModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<InstructorModel>> FindAll()
        {
            IEnumerable<InstructorModel> instructors;
            var sp = "sp_Instructors_FindAll";

            try
            {
                var conn = factoryConnection.GetConnection();
                instructors = await conn.QueryAsync<InstructorModel>(sp, null, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw new Exception("Error en la base de datos");
            }
            finally
            {
                factoryConnection.CloseConnection();
            }
            return instructors;
        }

        public Task<IEnumerable<InstructorModel>> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(InstructorModel model)
        {
            throw new NotImplementedException();
        }
    }
}
