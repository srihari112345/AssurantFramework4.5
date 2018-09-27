using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace Employee.App
{
    class Program
    {

        static void Main(string[] args)
        {
            try
            {
                string baseUrl = "https://localhost:60589/api/employee";
                int option;
                bool tryAgain = true;
                string yesOrNo;
                List<EmployeeModel> employees = new List<EmployeeModel>();
                do
                {
                    Console.WriteLine("1) Fetch All/ {n} employees that are actively not engaged on an Project ");
                    Console.WriteLine("2) Fetch All /{n} employees that are actively engaged on 1 project ");
                    Console.WriteLine("3) Fetch All /{n} employees that are actively engaged in multiple projects");
                    Console.Write("Enter the Option: ");
                    while (!int.TryParse(Console.ReadLine(), out option))
                    {
                        Console.Write("Enter a Valid Interger: ");
                    }
                    switch (option)
                    {
                        case 1:
                            GetEmployeeEngagementAsync(baseUrl + "/zero/project", "Zero").GetAwaiter().GetResult();
                            break;
                        case 2:
                            GetEmployeeEngagementAsync(baseUrl + "/single/project", "Single").GetAwaiter().GetResult();
                            break;
                        case 3:
                            GetEmployeeEngagementAsync(baseUrl + "/multiple/projects", "Multiple").GetAwaiter().GetResult();
                            break;
                        default:
                            Console.WriteLine("Invalid Input");
                            break;
                    }
                    Console.WriteLine("Do you want to continue? (y/n)? ");
                    do
                    {
                        Console.Write("Enter Option:");
                        yesOrNo = Console.ReadLine().ToString().ToLower();
                    } while (yesOrNo != "y" && yesOrNo != "n");
                    tryAgain = (yesOrNo == "y") ? true : false;
                } while (tryAgain);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.ReadKey();
            }
        }

        public static async Task GetEmployeeEngagementAsync(string url, string type)
        {
            List<EmployeeModel> employees = new List<EmployeeModel>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "Anything");
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        employees = await response.Content.ReadAsAsync<List<EmployeeModel>>();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine("Try Again..");
                Console.ReadKey();
            }
            finally
            {
                if (employees.Any())
                {
                    WrtieToFile(employees, type);
                }
            }
        }

        public static void WrtieToFile(List<EmployeeModel> employeeList, string type)
        {
            try
            {
                Console.WriteLine("Writing to file..");
                var count = 1;
                var splitList = employeeList.Select((x, i) => new { Index = i, Value = x })
                                            .GroupBy(x => x.Index / 100)
                                            .Select(x => x.Select(v => v.Value).ToList())
                                            .ToList();

                int startToWrite, totalTimeToWrite = 0;
                splitList.ForEach(x =>
                {
                    var stringBuilder = new StringBuilder();
                    foreach (var item in x)
                    {
                        stringBuilder.Append("EmployeeId: " + item.EmployeeId.ToString().Trim()).Append("\t");
                        stringBuilder.Append("Name: " + item.Name.Trim()).Append("\t");
                        stringBuilder.Append("BaseLocation: " + item.BaseLocation.Trim());
                        stringBuilder.Append(Environment.NewLine);
                    }
                    startToWrite = DateTime.Now.Millisecond;
                    File.WriteAllText("D:\\Project\\AssurantAssignment\\Report\\" + type + "Project\\Report" + count + ".txt", stringBuilder.ToString());
                    totalTimeToWrite += DateTime.Now.Millisecond - startToWrite;
                    count++;
                });
                Console.WriteLine("Reports Generated");
                Console.WriteLine("Press any key..");
                Console.WriteLine(" Time taken for writing : {0} millseconds", totalTimeToWrite);
                Console.ReadKey();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                Console.WriteLine("Try Again..");
                Console.ReadKey();
            }
        }
    }
}
