using ISpan.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//Insert("May", "may8888", "abc123", new DateTime(2000 ,1 , 1), 165);
			//Delete(3);
			//Select(4);
			Update(4, "changed");
		}
		static void Insert (string name,string account,string password,DateTime dateOfBirthd,int Height)
		{
			string sqlInsert = "INSERT INTO Users(name,account,password,dateOfBirthd,Height)VALUES(@name,@account,@password,@dateOfBirthd,@Height)"; //sql語法

			var dbHelper = new SqlDbHelper("default");
			try
			{
				var parameter = new SqlParameterBuilder()
					.AddNVarchar("@name", 50, name)
					.AddNVarchar("@account", 50, account)
					.AddNVarchar("@password", 50, password)
					.AddDateTime("@dateOfBirthd", dateOfBirthd)
					.AddInt("@Height", Height)
					.Build();

				dbHelper.ExecuteNonQuery(sqlInsert, parameter);
				Console.WriteLine("紀錄已新增");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"錯誤，原因:{ex.Message}");
			}
		}

		static void Update(int id,string changeName)
		{
			string sqlDelete = "update users set name=@changeName  where Id=@Id";//sql語法

			var dbHelper = new SqlDbHelper("default");
			try
			{
				var parameter = new SqlParameterBuilder()
					.AddInt("@Id", id)
					.AddNVarchar("@changeName", 50, changeName)
					.Build();

				dbHelper.ExecuteNonQuery(sqlDelete, parameter);
				Console.WriteLine("紀錄已更新");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"錯誤，原因:{ex.Message}");
			}
		}
		static void Delete(int id)
		{
			string sqlDelete = "delete from Users where Id=@Id";//sql語法

			var dbHelper = new SqlDbHelper("default");
			try
			{
				var parameter = new SqlParameterBuilder()
					.AddInt("@Id", id)
					.Build();

				dbHelper.ExecuteNonQuery(sqlDelete, parameter);
				Console.WriteLine("紀錄已刪除");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"錯誤，原因:{ex.Message}");
			}
		}

		static void Select(int id)
		{
			var dbHelper = new SqlDbHelper("default");
			string sql = "select id,name from users where id=@id order by id desc";

			try
			{
				var parameters = new SqlParameterBuilder().AddInt("id", id).Build();
				DataTable users = dbHelper.Select(sql, parameters);

				foreach (DataRow row in users.Rows)
				{
					int idd = row.Field<int>("id");
					string name = row.Field<string>("name");
					Console.WriteLine($"id={id},title={name}");
				}

			}

			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
