using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using OnlineStore.Models;

namespace OnlineStore.Controllers
{
    public class ReportHomeController : Controller
    {
        public ActionResult Index(string selectedState = "California")
        {
         
            SalesReportModel model = new SalesReportModel();

            string connectionString = ConfigurationManager.ConnectionStrings["AdventureWorks"].ConnectionString;
            string connectionString2 = ConfigurationManager.ConnectionStrings["OnlineStoreDatabase"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString2))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandText = "";
            }





            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT DISTINCT StateProvince FROM Address INNER JOIN SalesOrderHeader ON Address.AddressID = SalesOrderHeader.BillToAddressID";
               
                List<string> states = new List<string>();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        states.Add(reader.GetString(0));
                    }
                }

                model.TopSalesByQuantity = new TopSaleByQuantity[0];
                SqlCommand Quantitycommand = connection.CreateCommand();
                Quantitycommand.CommandText = command.CommandText = @"select top 5 product.ProductID, product.name, SUM(OrderQty) from salesorderdetail JOIN SalesOrderHeader
                                        ON SalesOrderDetail.SalesOrderID = SalesOrderHeader.SalesOrderID
                                        JOIN[Address] ON SalesOrderHeader.BillToAddressID = Address.AddressID
                                        join product on product.productid = salesorderdetail.productid
                                         WHERE Address.StateProvince = '" + selectedState + "' group by product.ProductID, product.name order by sum(OrderQty) desc";

                List<TopSaleByQuantity> Quantity = new List<TopSaleByQuantity>();
                using (SqlDataReader QuantityReader = Quantitycommand.ExecuteReader())
                {
                    while (QuantityReader.Read())
                    {
                        Quantity.Add(new TopSaleByQuantity { ProductID = QuantityReader.GetInt32(0), ProductName = QuantityReader.GetString(1), Quantity = QuantityReader.GetInt32(2)});

                    }
                    model.TopSalesByQuantity = Quantity.ToArray();

                }

                model.TopSalesByDollar = new TopSaleByDollar[0];
                SqlCommand Totalcommand = connection.CreateCommand();
                Totalcommand.CommandText = command.CommandText = @"select top 5 product.ProductID, product.name, SUM(LineTotal) from salesorderdetail JOIN SalesOrderHeader
                                        ON SalesOrderDetail.SalesOrderID = SalesOrderHeader.SalesOrderID
                                        JOIN[Address] ON SalesOrderHeader.BillToAddressID = Address.AddressID
                                        join product on product.productid = salesorderdetail.productid
                                         WHERE Address.StateProvince = '" + selectedState + "' group by product.ProductID, product.name order by sum(LineTotal) desc";

                List<TopSaleByDollar> total = new List<TopSaleByDollar>();
                using (SqlDataReader totalReader = Totalcommand.ExecuteReader())
                {
                    while (totalReader.Read())
                    {
                        total.Add(new TopSaleByDollar {ProductID = totalReader.GetInt32(0), ProductName = totalReader.GetString(1), Total = totalReader.GetDecimal(2)});
                       
                    }

                    model.TopSalesByDollar = total.ToArray();
                }

                model.States = states.ToArray();
                //model.TopSalesByDollar = new TopSaleByDollar[0];
               // model.TopSalesByQuantity = new TopSaleByQuantity[0];
                
                connection.Close();
            }
            model.SelectedState = selectedState;
            return View(model);
        }
    }

    
}
