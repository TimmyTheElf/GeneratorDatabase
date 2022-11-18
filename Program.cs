using System.Data;
using System.Data.OleDb;
using System.Net;
using System.Xml.Linq;
using Oracle.ManagedDataAccess.Client;


namespace GeneratorDatabase
{
    internal class Program
    {

        static void Main(string[] args)
        {

            DataTable managers = new DataTable();
            managers = Managers.GetDataTableOfManagers();

            Managers.SaveUsingOracleBulkCopy(managers);

            DataTable projects = new DataTable();
            projects = Projects.GetDataTableOfProjects();

            Projects.SaveUsingOracleBulkCopy(projects);



        }
    }
}