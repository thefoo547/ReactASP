using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
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

        public async Task<int> Create(InstructorModel model)
        {
            var sp = "sp_Instructor_Create";
            int res = 0;
            try
            {
                var conn = factoryConnection.GetConnection();
                res = await conn.ExecuteAsync(sp,
                    new
                    {
                        InstructorId = Guid.NewGuid(),
                        model.Name,
                        model.LastName,
                        model.Grade
                    },
                    commandType: CommandType.StoredProcedure);

            }
            catch
            {
                throw new Exception("No se pudo ingresar");
            }
            finally
            {
                factoryConnection.CloseConnection();
            }
            return res;
        }

        public async Task<int> Delete(Guid id)
        {
            var sp = "sp_Instructor_delete";
            int res = 0;
            try
            {
                var conn = factoryConnection.GetConnection();
                res = await conn.ExecuteAsync(sp, new { InstructorId = id }, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw new Exception("No se pudo eliminar la data");
            }
            finally
            {
                factoryConnection.CloseConnection();
            }
            return res;
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

        public async Task<InstructorModel> GetById(Guid id)
        {
            InstructorModel instructor;
            var sp = "sp_Instructor_Find";

            try
            {
                var conn = factoryConnection.GetConnection();
                instructor = await conn.QueryFirstAsync<InstructorModel>(sp, new { InstructorId = id }, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw new Exception("Error en la base de datos");
            }
            finally
            {
                factoryConnection.CloseConnection();
            }
            return instructor;
        }

        public async Task<int> Update(InstructorModel model)
        {
            var sp = "sp_Instructor_Update";
            int res = 0;
            try
            {
                var conn = factoryConnection.GetConnection();
                res = await conn.ExecuteAsync(sp, new
                {
                    model.InstructorId,
                    model.Name,
                    model.LastName,
                    model.Grade
                }, commandType: CommandType.StoredProcedure);
            }
            catch
            {
                throw new Exception("No se pudo editar la data");
            }
            finally
            {
                factoryConnection.CloseConnection();
            }
            return res;
        }
    }
}
