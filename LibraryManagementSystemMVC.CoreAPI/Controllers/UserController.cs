using coreData.Data;
using coreModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LibraryManagementSystemMVC.CoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _sqlConnection;
        SqlCommand cmd = null;
        SqlDataAdapter da = null;
        public UserController(ApplicationDbContext applicationDbContext, IConfiguration configuration)
        {
            _applicationDbContext = applicationDbContext;
            _configuration = configuration;

            // IConfiguration aracılığıyla bağlantı dizesini al
            string connectionString = _configuration.GetConnectionString("dbcon");
            _sqlConnection = new SqlConnection(connectionString);


        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_applicationDbContext.Users == null)
            {
                return NotFound();
            }
            return await _applicationDbContext.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUsers(int id)
        {
            if (_applicationDbContext.Users == null)
            {
                return NotFound();
            }
            var user = await _applicationDbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {

            _applicationDbContext.Users.Add(user);
            await _applicationDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsers), new { id = user.UserId }, user);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }
            _applicationDbContext.Entry(user).State = EntityState.Modified;
            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return Ok();
        }
    }
}

//Diğer Kodlar

//[HttpPost]
//[Route("Registration")]
//public string Registration(User user)
//{
//    string msg = string.Empty;
//    try
//    {
//        using (SqlCommand cmd = new SqlCommand("usp_Registration", _sqlConnection))
//        {
//            cmd.CommandType = CommandType.StoredProcedure;

//            // Diğer SqlCommand ayarlarını buraya ekleyebilirsiniz.
//            // Örnek: cmd.Parameters.AddWithValue("@ParameterName", parameterValue);
//            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
//            cmd.Parameters.AddWithValue("@LastName", user.LastName);
//            cmd.Parameters.AddWithValue("@UserName", user.Username);
//            cmd.Parameters.AddWithValue("@Password", user.Password);
//            cmd.Parameters.AddWithValue("@RegistrationDate", user.RegistrationDate);
//            _sqlConnection.Open();
//            int i = cmd.ExecuteNonQuery();
//            _sqlConnection.Close();
//            if (i > 0)
//            {
//                msg = "Data inserted";
//            }
//            else
//            {
//                msg = "Error";
//            }
//        }
//    }
//    catch (Exception ex)
//    {
//        msg = ex.Message;
//    }
//    return msg;
//}

//[HttpPost]
//[Route("User/Login")]
//public string UserLogin(User user)
//{
//    string msg = string.Empty;
//    try
//    {
//        da = new SqlDataAdapter("usp_Login", _sqlConnection);
//        da.SelectCommand.CommandType = CommandType.StoredProcedure;
//        da.SelectCommand.Parameters.AddWithValue("@UserName", user.Username);
//        da.SelectCommand.Parameters.AddWithValue("@Password", user.Password);
//        DataTable dt = new DataTable();
//        da.Fill(dt);
//        if (dt.Rows.Count > 0)
//        {
//            msg = "User is valid";
//        }
//        else
//        {
//            msg = "User is Invalid";
//        }

//    }
//    catch (Exception ex)
//    {
//        msg = ex.Message;
//    }
//    return msg;
//}
