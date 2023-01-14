using Dapper;
using PhonBook.Core.Domain.Contacts;
using PhonBook.Core.Domain.Grops;
using PhonBook.infra.Data.sqlserver.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PhonBook.infra.Data.sqlserver.Grops
{
    public class GroupRepository : IGroupRepository
    {
        private readonly DapperContext _context;

        public GroupRepository(DapperContext dapperContext)
        {
            _context = dapperContext;
        }
        public async Task Add(Grop group)
        {
            var query = "INSERT INTO Grops (Name, Name_Company, Organization,Tell_phone,Address,Code_posti,Fax,Contact_id) " +
                "VALUES " + "(@Name, @Name_Company, @Organization,@Tell_phone,@Address,@Code_posti,@Fax,@Contact_id)";

            var parameters = new DynamicParameters();

            parameters.Add("Name", group.Name, DbType.String);
            parameters.Add("Name_Company", group.Name_Company, DbType.String);
            parameters.Add("Organization", group.Organization, DbType.String);
            parameters.Add("Tell_phone", group.Tell_phone, DbType.String);
            parameters.Add("Address", group.Address, DbType.String);
            parameters.Add("Code_posti", group.Code_posti, DbType.String);
            parameters.Add("Fax", group.Fax, DbType.Int32);
            parameters.Add("Contact_id", group.Contact_id, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }

        }

        public async Task<Grop> Create(Grop group)
        {
            var query = "INSERT INTO Grops(Name, Name_Company, Organization, Tell_phone, Address, Code_posti, Fax)  VALUES (@Name, @Name_Company, @Organiztion,@Tell_phone,@Address,@Code_posti,@Fax ,@Contact_id)" +
                "SELECT CAST(SCOPE_IDENTITY() as int)";
            var parameters = new DynamicParameters();
            parameters.Add("Name", group.Name, DbType.String);
            parameters.Add("Name_Company", group.Name_Company, DbType.String);
            parameters.Add("Organization", group.Organization, DbType.String);
            parameters.Add("Tell_phone", group.Tell_phone, DbType.String);
            parameters.Add("Address", group.Address, DbType.String);
            parameters.Add("Code_posti", group.Code_posti, DbType.String);
            parameters.Add("Fax", group.Fax, DbType.Int32);
            parameters.Add("Contact_id", group.Contact_id, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var createdGroup = new Grop
                {
                    Id = id,
                    Name = group.Name,
                    Address = group.Organization,
                    Code_posti = group.Code_posti,
                    Name_Company = group.Name_Company,
                    Tell_phone = group.Tell_phone,
                    Fax = group.Fax,
                    Organization = group.Organization,
                    Contact_id = group.Contact_id,
                };
                return createdGroup;
            }
        }

        public  void Delete(int Id)
        {
            var query = "DELETE FROM Grops WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                connection.Execute(query, new { Id });
            }
        }
        public async Task<int> DeleteAsync(int Id)
        {
            var query = "DELETE FROM Grops WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var res=  await connection.ExecuteAsync(query, new { Id });
                return res;
            }
        }

        public async Task<Grop> FindByIdAsync(int id)
        {
            var query = "SELECT * FROM Grops WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var grop = await connection.QuerySingleOrDefaultAsync<Grop>(query, new { id });
                return grop;
            }
        }

        public async Task<List<Grop>> GetAllAsync()
        {
            var query = "SELECT * FROM Grops";

            using (var connection = _context.CreateConnection())
            {
                var grop = await connection.QueryAsync<Grop>(query);
                return grop.ToList();
            }
        }

        public async Task< int >Update(int Id, Grop group)
        {
            var query = "UPDATE Grops SET Name = @Name, Name_Company=@Name_Company, Organization=@Organization, Tell_phone=@Tell_phone, Address=@Address, Code_posti=@Code_posti, Fax=@Fax, Contact_id=@Contact_id   WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", Id, DbType.Int32);
            parameters.Add("Name", group.Name, DbType.String);
            parameters.Add("Name_Company", group.Name_Company, DbType.String);
            parameters.Add("Organization", group.Organization, DbType.String);
            parameters.Add("Tell_phone", group.Tell_phone, DbType.String);
            parameters.Add("Address", group.Address, DbType.String);
            parameters.Add("Code_posti", group.Code_posti, DbType.String);
            parameters.Add("Fax", group.Fax, DbType.Int32);
            parameters.Add("Contact_id", group.Contact_id, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
               var res= await connection.ExecuteAsync(query, parameters);
                return res;
            }

        }


        public async Task<Grop> find_Group_by_name(string Name)
        {
            var query = "SELECT * FROM Grops WHERE Name=@Name";
            using (var connection = _context.CreateConnection())
            {
                var grop = await connection.QuerySingleOrDefaultAsync<Grop>(query, new { Name });
                return grop;
            }
        }

        public List<Contact> GetContacts(int idgrop)
        {
            // var query = "Select * from Groups WHERE Contact_id=@Contact_id";
            // //var query = "SELECT * FROM Contacts WHERE Id = @Id";
            // using (var connection = _context.CreateConnection())
            // {
            //    var res = connection.QuerySingleOrDefault<Contact>(query, new { idgrop });
            //    return res;
            // }
            throw new NotImplementedException();
        }

        public List<Grop> GetAll()
        {
            var query = "SELECT * FROM Grops";

            using (var connection = _context.CreateConnection())
            {
                var grops =  connection.Query<Grop>(query);
                return grops.ToList();
            }
        }

        public Grop FindById(int Id)
        {
            var query = "SELECT * FROM Grops WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var grops =  connection.QuerySingleOrDefault<Grop>(query, new { Id });
                return grops;
            }
        }

        public async Task<Grop> FindByNameAsync(string Name)
        {
            var query = "SELECT * FROM Grops WHERE Name=@Name";
            using (var connection = _context.CreateConnection())
            {
                var grop = await connection.QuerySingleOrDefaultAsync<Grop>(query, new { Name });
                return grop;
            }
        }

        public Grop FindByName(string Name)
        {
            var query = "SELECT * FROM Grops WHERE Name=@Name";
            using (var connection = _context.CreateConnection())
            {
                var grop =  connection.QuerySingleOrDefault<Grop>(query, new { Name });
                return grop;
            }
        }

        public async Task<List<Grop>> Serch_grop(string Name)
        {
            var query = "SELECT * FROM Grops WHERE Fullname like  @Name";
            using (var connection = _context.CreateConnection())
            {
                var grops = await connection.QueryAsync<Grop>(query, new { Name });
                return grops.ToList();
            }
        }
    }
}