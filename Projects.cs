using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorDatabase
{
    internal class Projects
    {
        private static string conString = "User Id = s81969;Password=s81969;Data Source =(DESCRIPTION =  (ADDRESS = (PROTOCOL = tcp)(HOST = 217.173.198.135)(PORT = 1521)) (CONNECT_DATA = (SERVICE_NAME = tpdb)));";


        private static List<string> projectNames = new List<string>() { "Point Men", "Thumbsucker", "Full Body Massage", "Riot on Sunset Strip", "Tokyo!", "Running on Empty", "Like Sunday, Like Rain", "Unknown Woman, The (La Sconosciuta)", "Hamlet (Gamlet)", "Gun the Man Down", "Un Piede in paradiso", "Cold Turkey", "Set-Up, The", "Highway Racer", "27 Dresses", "Great Caruso, The", "Abendland", "What Maisie Knew", "Jaws 2", "Amazing Spider-Man, The", "Kill, Baby, Kill (Operazione paura)", "December Boys", "Come Dance with Me!", "Kiss the Girls", "Wild Hearts Can't Be Broken", "Witches' Hammer (Kladivo na carodejnice) ", "It Happened One Night", "Son of the Pink Panther", "Hangmen Also Die", "Lord Jim" };
        private static List<string> countries = new List<string>() { "China","Brazil","China","Panama","Sweden","Russia","Cuba","Brazil","Serbia","Indonesia","Colombia","Finland","Portugal","Japan","China","Nicaragua","Indonesia","Japan","Yemen","Mexico","China","Albania","Finland","Brazil","China","Indonesia","United States","Afghanistan","Cyprus","Palestinian Territory"};
        private static List<string> serviceLines = new List<string>() { "Tax", "Accounting", "Cybersecurity", "Audit", "IT" };

        private static DataTable GetManagersID()
        {
            try
            {
                using (var connection = new OracleConnection(conString))
                {
                    connection.Open();
                    OracleCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "SELECT MANAGER_ID FROM MANAGERS";
                    cmd.CommandType = System.Data.CommandType.Text;
                    var dr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    connection.Close();
                    return dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static DataTable GetDataTableOfProjects()
        {
            DataTable dt = new DataTable();
            DataRow dr;
            string projectName;
            string country;
            string serviceLine;
            int managerID;

            DataTable managerIDS= new DataTable();
            managerIDS = GetManagersID();

            dt.Columns.Add("PROJECT_NAME", System.Type.GetType("System.String"));
            dt.Columns.Add("MANAGER_ID", System.Type.GetType("System.Int16"));
            dt.Columns.Add("PROJECT_COUNTRY", System.Type.GetType("System.String"));
            dt.Columns.Add("PROJECT_SERVICE_LINE", System.Type.GetType("System.String"));

            var rand = new Random();


            for (int i = 0; i < 1000; i++)
            {

                projectName = projectNames[rand.Next(projectNames.Count)];
                country = countries[rand.Next(countries.Count)];
                serviceLine = serviceLines[rand.Next(serviceLines.Count)];
                managerID = Convert.ToInt16(managerIDS.Rows[rand.Next(managerIDS.Rows.Count)]["MANAGER_ID"]);
                dr = dt.NewRow();
                dr["PROJECT_NAME"] = projectName;
                dr["MANAGER_ID"] = managerID;
                dr["PROJECT_COUNTRY"] = country;
                dr["PROJECT_SERVICE_LINE"] = serviceLine;
                dt.Rows.Add(dr);

            }

            return dt;

        }

        public static void SaveUsingOracleBulkCopy(DataTable dt)
        {
            try
            {
                using (var connection = new OracleConnection(conString))
                {
                    connection.Open();
                    string[] names = new string[dt.Rows.Count];
                    int[] managers = new int[dt.Rows.Count];
                    string[] countries = new string[dt.Rows.Count];
                    string[] serviceLines = new string[dt.Rows.Count];

                    for (int j = 0; j < 1000; j++)
                    {
                        names[j] = Convert.ToString(dt.Rows[j]["PROJECT_NAME"]);
                        managers[j] = Convert.ToInt16(dt.Rows[j]["MANAGER_ID"]);
                        countries[j] = Convert.ToString(dt.Rows[j]["PROJECT_COUNTRY"]);
                        serviceLines[j] = Convert.ToString(dt.Rows[j]["PROJECT_SERVICE_LINE"]);
                    }

                    OracleParameter fn = new OracleParameter();
                    fn.OracleDbType = OracleDbType.Varchar2;
                    fn.Value = names;

                    OracleParameter mid = new OracleParameter();
                    mid.OracleDbType = OracleDbType.Int16;
                    mid.Value = managers;

                    OracleParameter con = new OracleParameter();
                    con.OracleDbType = OracleDbType.Varchar2;
                    con.Value = countries;

                    OracleParameter sl = new OracleParameter();
                    sl.OracleDbType = OracleDbType.Varchar2;
                    sl.Value = serviceLines;

                    // create command and set properties
                    OracleCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "INSERT INTO PROJECTS (PROJECT_NAME, MANAGER_ID, PROJECT_COUNTRY, PROJECT_SERVICE_LINE) VALUES (:1, :2, :3, :4)";
                    cmd.ArrayBindCount = names.Length;
                    cmd.Parameters.Add(fn);
                    cmd.Parameters.Add(mid);
                    cmd.Parameters.Add(con);
                    cmd.Parameters.Add(sl);
                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}



