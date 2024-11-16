using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;

public class MyController : Controller
{
    private readonly string _connectionString;

    public MyController(IConfiguration configuration)
    {
        // 從 appsettings.json 中讀取資料庫連接字串
        _connectionString = configuration.GetConnectionString("MariaDbConnection");
    }

    // 這個動作方法將從資料庫讀取資料並顯示在前端
    [HttpGet("/getdata")]
    public IActionResult GetData()
    {
        List<Users> users = new List<Users>();

        using (var connection = new MySqlConnection(_connectionString))
        {
            try
            {
                connection.Open();

                // 查詢資料表 M_USER
                string query = "SELECT * FROM M_USER";  // 全檢索所有欄位

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // 創建 User 物件，並將查詢結果對應到各個欄位
                            var user = new Users
                            {
                                SERI_NO = reader.GetInt32("SERI_NO"),         // 對應 `id` 欄位
                                USER_ID = reader.GetString("USER_ID"),     // 對應 `name` 欄位
                                USER_PASS = reader.GetString("USER_PASS")    // 對應 `email` 欄位
                            };

                            // 將 User 物件加入到列表
                            users.Add(user);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 處理錯誤
                return StatusCode(500, "Database error: " + ex.Message);
            }
        }

        // 返回資料給前端，通常返回 JSON 格式
        return Json(users);
    }
}
