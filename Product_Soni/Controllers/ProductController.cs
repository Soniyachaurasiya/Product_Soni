using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;



namespace Product_Soni.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        SqlConnection con;
        SqlDataAdapter da;
        IConfiguration c;
        public ProductController(IConfiguration configuration)
        {
            c = configuration;
            con = new SqlConnection();

            da = new SqlDataAdapter();

            string str = c.GetConnectionString("Constr");
            con.ConnectionString = str;

        }

        [HttpGet]
     public IEnumerable<Product>GetProductDetails(Product obj)

        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select*from  Product";
            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            int ctr = 0;
            List<Product> list = new List<Product>();
            if (dr.Read()) 
            {
                obj = new Product();
                obj.ProductID = (int)dr[0];
                obj.Name= (string)dr[1];    
                obj.Description = (string)dr[2];
                obj.Price= (float)dr[3];
                obj.Category= (string)dr[4];    
                list.Add(obj);
              }
            return list;
        }
        [HttpGet]
        public string Getdata(int ProductID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select*from  Product where ProductID = @p1";
            cmd.Parameters.Add(new SqlParameter("p1",System.Data.SqlDbType.Int));
            
            cmd.Parameters["p1"].Value = ProductID;
                
            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            Product obj = null;   
            List<Product> list = new List<Product>();
            if (dr.Read())
            {
                obj = new Product();
                obj.ProductID = (int)dr[0];
                obj.Name = (string)dr[1];
                obj.Description = (string)dr[2];
                obj.Price = (float)dr[3];
                obj.Category = (string)dr[4];
                list.Add(obj);
            }
            if (obj == null) {
                return "Not found";
                    }
            else {
                return list.ToString();
            }
        }

        [HttpPost]
        public string InsertData(int a, string b, string c, float d, string e) 
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "insert into Product value @p1,@p2,@p3,@p4,@p5";
            cmd.Parameters.Add(new SqlParameter("p1", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("p2", SqlDbType.VarChar, 250));
            cmd.Parameters.Add(new SqlParameter("p4", System.Data.SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("p5", System.Data.SqlDbType.VarChar, 15));
            cmd.Parameters["p1"].Value = a;
            cmd.Parameters["p2"].Value = b;
            cmd.Parameters["p3"].Value = c;
            cmd.Parameters["p4"].Value = d;
            cmd.Parameters["p5"].Value = e;
            int ans = cmd.ExecuteNonQuery();    
            return ans.ToString();
        }
        [HttpPut]
        public string UpdateData(int ProductID)
        {
            SqlCommand cmd = new SqlCommand();  
            cmd.Connection = con;
            cmd.CommandText = "update from product whee productId = @p1";
            cmd.Parameters.Add(new SqlParameter("p1", System.Data.SqlDbType.Int));
                cmd.Parameters["p1"].Value = ProductID;
            int ans = cmd.ExecuteNonQuery();
            return ans.ToString();
        }
        [HttpDelete]
        public string UpdateData(int ProductID)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Delete from product whee productId = @p1";
            cmd.Parameters.Add(new SqlParameter("p1", System.Data.SqlDbType.Int));
            cmd.Parameters["p1"].Value = ProductID;
            int ans = cmd.ExecuteNonQuery();
            return ans.ToString();
        }
    } 
}
