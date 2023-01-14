using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using PhonBook.Core.Domain.Contacts;
using PhonBook.infra.Data.sqlserver.Common;

namespace PhonBook.infra.Data.sqlserver.Contacts
{
    public class ContactRepository : IContactRepository
    {
        private readonly DapperContext _context;

        public ContactRepository(DapperContext dapperContext)
        {
            _context = dapperContext;
        }

        public async Task Add(Contact contact)
        {
            var query = "INSERT INTO Contacts (Fullname, POST, Tell_phone,Phone_number1,Phone_number2,Email,Group_Id) " +
                "VALUES " + "(@Fullname, @POST, @Tell_phone,@Phone_number1,@Phone_number2,@Email,@Group_Id)";

            var parameters = new DynamicParameters();
            parameters.Add("Fullname", contact.Fullname, DbType.String);
            parameters.Add("POST", contact.POST, DbType.String);
            parameters.Add("Tell_phone", contact.Tell_phone, DbType.String);
            parameters.Add("Phone_number1", contact.Phone_number1, DbType.String);
            parameters.Add("Phone_number2", contact.Phone_number2, DbType.String);
            parameters.Add("Email", contact.Email, DbType.String);
            parameters.Add("Group_Id", contact.Group_Id, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }


        public async Task<Contact> Create(Contact contact)
        {
            var query = "INSERT INTO Contacts (Fullname, POST, Tell_phone,Phone_number1,Phone_number2,Email,Group_Id) " +
               "VALUES " + "(@Fullname, @POST, @Tell_phone,@Phone_number1,@Phone_number2,@Email,@Group_Id) SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("Fullname", contact.Fullname, DbType.String);
            parameters.Add("POST", contact.POST, DbType.String);
            parameters.Add("Tell_phone", contact.Tell_phone, DbType.String);
            parameters.Add("Phone_number1", contact.Phone_number1, DbType.String);
            parameters.Add("Phone_number2", contact.Phone_number2, DbType.String);
            parameters.Add("Email", contact.Email, DbType.String);
            parameters.Add("Group_Id", contact.Group_Id, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);
                var result = new Contact
                {
                    Id = id,
                    Fullname = contact.Fullname,
                    POST = contact.POST,
                    Phone_number1 = contact.Phone_number1,
                    Phone_number2 = contact.Phone_number2,
                    Email = contact.Email,
                    Tell_phone = contact.Tell_phone,
                    Group_Id = contact.Group_Id,
                };
                return result;
            }

        }



        public async void Delete(int Id)
        {
            var query = "DELETE FROM Contacts WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { Id });
            }
        }

        public Contact FindById(int Id)
        {
            var query = "SELECT * FROM Contacts WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var res = connection.QuerySingleOrDefault<Contact>(query, new { Id });
                return res;
            }
        }

        public async Task<Contact> FindByIdAsync(int Id)
        {
            var query = "SELECT * FROM Contacts WHERE Id = @Id";
            using (var connection = _context.CreateConnection())
            {
                var res = await connection.QuerySingleOrDefaultAsync<Contact>(query, new { Id });
                return res;
            }
        }


        public Contact find_Contact_by_name(string Fullname)
        {
            var query = "SELECT * FROM Contacts WHERE Fullname=@Fullname";
            using (var connection = _context.CreateConnection())
            {
                var contact =  connection.QuerySingleOrDefault<Contact>(query, new { Fullname });
                return contact;
            }
        }
        public async Task< List<Contact> >Serch_contact(string Fullname)
        {
            var query = "SELECT * FROM Contacts WHERE Fullname like  @Fullname";
            using (var connection = _context.CreateConnection())
            {
                var contact =await connection.QueryAsync<Contact>(query, new { Fullname });
                return contact.ToList();
            }
        }

        public async Task<Contact> find_Contact_by_name_Async(string Fullname)
        {
            Console.WriteLine($"fullname: {Fullname}");
             var query = "SELECT * FROM Contacts WHERE Fullname=@Fullname";
            using (var connection = _context.CreateConnection())
            {
                var contact = await connection.QuerySingleOrDefaultAsync<Contact>(query, new { Fullname });
                Console.WriteLine($"contact: {contact.Fullname}");

                return contact;
            }
        }

        public List<Contact> GetAll()
        {
            var query = "SELECT * FROM Contacts";
            using (var connection = _context.CreateConnection())
            {
                var res = connection.Query<Contact>(query);
                return res.ToList();
            }
        }

        public async Task<List<Contact>> GetAllAsync()
        {
            var query = "SELECT * FROM Contacts";
            //var query = "SELECT Id, Name AS CompanyName, Address, Country FROM Companies";

            using (var connection = _context.CreateConnection())
            {
                var res = await connection.QueryAsync<Contact>(query);
                return res.ToList();
            }
        }

        public async void Update(int Id, Contact contact)
        {
            var query = "UPDATE Contacts SET Fullname = @Fullname, POST=@POST, Tell_phone=@Tell_phone, Phone_number1=@Phone_number1, Phone_number2=@Phone_number2, Email=@Email,Group_Id=@Group_Id   WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", Id, DbType.Int32);
            parameters.Add("Fullname", contact.Fullname, DbType.String);
            parameters.Add("POST", contact.POST, DbType.String);
            parameters.Add("Tell_phone", contact.Tell_phone, DbType.String);
            parameters.Add("Phone_number1", contact.Phone_number1, DbType.String);
            parameters.Add("Phone_number2", contact.Phone_number2, DbType.String);
            parameters.Add("Email", contact.Email, DbType.String);
            parameters.Add("Group_Id", contact.Group_Id, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
