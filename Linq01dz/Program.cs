using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Linq01dz
{
    class Program
    {
        static List<Area> areas = new List<Area>();


        static void Main(string[] args)
        {
            string connectionString = "Data Source=AMANKELDI-PC;initial catalog=CRCMS_new;Integrated Security=True";
            string sql = "SELECT * FROM Area";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connectionString);
                DataTable areaTable = new DataTable("Area");
                adapter.Fill(areaTable);

                foreach (DataRow row in areaTable.Rows)
                {
                    Area ar = new Area();

                    ar.AreaId = (int)row["AreaId"];
                    ar.TypeArea = (int)row["TypeArea"];
                    ar.Name = row["Name"].ToString();
                    ar.ParentId = (int)row["ParentId"];
                    ar.NoSplit = row["NoSplit"].ToString();
                    ar.AssemblyArea = row["AssemblyArea"].ToString();
                    ar.FullName = row["FullName"].ToString();
                    ar.MultipleOrders = row["MultipleOrders"].ToString();
                    ar.HiddenArea = row["HiddenArea"].ToString();
                    ar.IP = row["IP"].ToString();
                    ar.PavilionId = (int)row["PavilionId"];
                    ar.TypeId = (int)row["TypeId"];
                    ar.OrderExecution = (int)row["OrderExecution"];
                    ar.Dependence = (int)row["Dependence"];
                    ar.WorkingPeople = (int)row["WorkingPeople"];
                    ar.ComponentTypeId = (int)row["ComponentTypeId"];
                    ar.GroupId = (int)row["GroupId"];
                    ar.Segment = row["Segment"].ToString();

                    areas.Add(ar);
                }
            }

            Task1();
            Console.WriteLine("\n===========================\n");
            Task2();
            Console.WriteLine("\n===========================\n");
            Task3();
            Console.WriteLine("\n===========================\n");
            Task4();
            Console.WriteLine("\n===========================\n");
            Task5();
            Console.WriteLine("\n===========================\n");
            Task6();

        }

        static void Task1()
        {
            //Использовать цепочку состоящую не менее из 3 операторов.
            //Выгрузить данные где TypeArea = 1, 
            //произвести сортировку данных от большего к меньшим и вывести на экран следующие данные – Name, FullName, IP

            var data = areas.Where(w => w.TypeArea == 1).OrderByDescending(o => o.AreaId)
                   .Select(s => new { s.Name, s.FullName, s.IP }).ToList();

            Console.WriteLine("Task 1:\n");
            foreach (var v in data)
            {
                Console.WriteLine("\tName: {0}\tFullName: {1}\tIP: {2}", v.Name, v.FullName, v.IP);
            }


        }

        static void Task2()
        {
            //Используя синтаксис облегчающий восприятия выгрузить следующие данные: для ParentId = 0, 
            //отобразить на экран следующие данные – Name, FullName, IP.
            //При этом необходимо использовать отложенную выгрузку данных.

            var data = from a in areas
                       where a.ParentId == 0
                       select new
                       {
                           a.Name,
                           a.FullName,
                           a.IP
                       };

            Console.WriteLine("Task 2:\n");
            foreach (var v in data)
            {
                Console.WriteLine("\tName: {0}\tFullName: {1}\tIP: {2}", v.Name, v.FullName, v.IP);
            }

        }

        static void Task3()
        {
            //Создать массив целых чисел «Pavilion» от 1 до 6.
            //Произвести выгрузку данных из таблицы создав подзапрос. 
            //В подзапросе необходимо выбрать из массива «Pavilion» только 2, 4, 6.
            //Затем вывести на экран следующие данные – PavilionId, Name, FullName, IP

            int[] pavilion = new[] { 1, 2, 3, 4, 5, 6 };

            var data = areas.Where(w => pavilion.Where(ww => ww == 2 || ww == 4 || ww == 6)
                .Contains(w.PavilionId)).Select(s => new
                {
                    s.Name,
                    s.FullName,
                    s.IP,
                    s.PavilionId
                }).ToList();

            Console.WriteLine("Task 3:\n");
            foreach (var v in data)
            {
                Console.WriteLine("\tName: {0}\tFullName: {1}\tIP: {2}\tPavilionId: {3}", v.Name, v.FullName, v.IP, v.PavilionId);
            }
        }

        static void Task4()
        {
            //Используя условие пункта «с», написать запрос на синтаксисе облегчающее восприятие.
            int[] pavilion = new[] { 1, 2, 3, 4, 5, 6 };
            var data = from a in areas
                       where pavilion.Where(w => w == 2 || w == 4 || w == 6).Contains(a.PavilionId)
                       select new
                       {
                           a.Name,
                           a.FullName,
                           a.IP,
                           a.PavilionId
                       };

            Console.WriteLine("Task 4:\n");
            foreach (var v in data)
            {
                Console.WriteLine("\tName: {0}\tFullName: {1}\tIP: {2}\tPavilionId: {3}", v.Name, v.FullName, v.IP, v.PavilionId);
            }
        }

        static void Task5()
        {
            //Создать запрос с использованием ключевого слова «let», 
            //и выгрузить данные только где столбец «WorkingPeople» > 1.
            var data = from a in areas
                       let wp = a.WorkingPeople
                       where wp > 1
                       select a;

            Console.WriteLine("Task 5");
            foreach (var v in data)
            {
                Console.WriteLine("\tWorking: {0}", v.WorkingPeople);
            }
        }

        static void Task6()
        {
            //Создать запрос с использованием ключевого слова «into»,  
            //где в первом запросе должны вывестись следующие данные: 
            //ParentId, FullName, ParentId, Dependence, далее во втором запросе отобразить только те зоны у которых Dependence > 0.

            var data = from a in areas
                       select new
                       {
                           a.ParentId,
                           a.FullName,
                           a.PavilionId,
                           a.Dependence
                       }
                into i
                       where i.Dependence > 0
                       select i;

            Console.WriteLine("Task 6:\n");
            foreach (var v in data)
            {
                Console.WriteLine("\tParentId: {0}\tFullName: {1}\tPavilionId: {2}\tDependence: {3}", v.ParentId, v.FullName, v.PavilionId, v.Dependence);
            }


        }
    }
}
