using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace GeneratorDatabase
{
    internal class Managers
    {
        private static string conString = "User Id = s81969;Password=s81969;Data Source =(DESCRIPTION =  (ADDRESS = (PROTOCOL = tcp)(HOST = 217.173.198.135)(PORT = 1521)) (CONNECT_DATA = (SERVICE_NAME = tpdb)));";

        private static List<string> firstNames = new List<string>() { "Alphonso", "Bartolemo", "Sanders", "Laurena", "Marrissa", "Jarad", "Pepe", "Lanie", "Ingamar", "Bastian", "Mariette", "Cyril", "Brandais", "Sophia", "Leslie", "Sarena", "Clementius", "Roxy", "Phaedra", "Concettina", "Sigismundo", "Rosie", "Gilberte", "Nicolle", "Jany", "Ranice", "Verene", "Morgan", "Clair", "Maximilianus", "Allen", "Warren", "Vergil", "Hamilton", "Sharon", "Meryl", "Jennilee", "Davina", "Correna", "Estella", "Loralie", "Holly", "Sharona", "Giorgia", "Uta", "Lily", "Worthy", "Orelie", "Virgie", "Teri", "Doralyn", "Kerrin", "Mehetabel", "Sue", "Anderson", "Ebonee", "Luci", "Brittney", "Courtenay", "Calhoun", "Muriel", "Katie", "Percival", "Ladonna", "Daune", "Sacha", "Jojo", "Rae", "Carena", "Darya", "Carole", "Jami", "Caroline", "Clerc", "Renard", "Bobette", "Alicia", "Myrle", "Everett", "Elianora", "Larine", "Khalil", "Christoper", "Raimundo", "Alvan", "Gerry", "Genevieve", "Mathian", "Serge", "Calhoun", "Kirbee", "Arvy", "Ulysses", "Kristoffer", "Noami", "Mellicent", "Sergio", "Bevon", "Cal", "Cristionna" };
        private static List<string> lastNames = new List<string>() { "Pacey", "Gentile", "Ransom", "Brundale", "Salkild", "Treppas", "Bambery", "Hew", "Meritt", "Hinken", "Littrell", "Pykett", "Hehl", "Witherop", "Timby", "Lucien", "Neads", "Bortoletti", "Beswetherick", "Hackin", "Tully", "Bernardo", "Gorhardt", "Hexam", "Cleiment", "Sainteau", "Tolworth", "Welds", "Hynson", "Bikker", "Clarkson", "Dutnall", "Willshaw", "Woodfin", "Haughey", "Grigore", "Abbati", "Horwell", "Pablo", "Bidnall", "Surgenor", "Wilford", "Vlasov", "Goldstraw", "Jouannin", "Whiteoak", "O'Hannay", "Gowar", "Ayrton", "Yeiles", "Diemer", "Ryson", "Maudlin", "D'Avaux", "Cadigan", "Stirman", "Bridden", "Bocking", "Eddowes", "Josefs", "Redmayne", "Egell", "Christensen", "Slimming", "Binton", "Sulman", "Schober", "Iacopetti", "Dunston", "Addionisio", "Peckham", "Gulston", "Holwell", "Ellinor", "Durman", "Fakes", "Lough", "Downey", "Tison", "Ferrierio", "Terrelly", "Lashbrook", "Augie", "Britian", "Edison", "Tight", "Tofano", "Denerley", "Schoffel", "de Guerre", "Kitchingman", "Ben-Aharon", "Rushman", "Kaplan", "Gaukrodge", "Guihen", "Braidman", "Walley", "Blenkiron", "Wetter", "Longstreeth", "Adess", "Wegman", "Panther", "Richt", "Dabinett", "Mathely", "Bridywater", "Gwillyam", "Cowx", "Andrivot", "Cookes", "Hamberstone", "Raisher", "Yapp", "Tzar", "Bassom", "Scryne", "Alekseev", "Pagan", "Guess", "Maunton", "Burchmore", "Skures", "Bearcock", "Fosten", "Fieldsend", "Amoore", "Rentoll", "Sang", "Iggo", "Owbrick", "Parkyn", "Ducarel", "Cherm", "Breede", "Rembrandt", "Mazillius", "Grunguer", "Kampshell", "Drewett", "L'Archer", "Tink", "Rousby", "Dudenie", "Drieu", "Godfrey", "Sutor", "De Avenell", "Medway", "Augar", "Vidler", "Tzar", "Roser", "Schaben", "Cleve", "Ruskin", "Hanmer", "Gatenby", "Wolfers", "Gannan", "Larner", "Daubney", "Gillet", "Antoni", "Dunton", "Sarch", "Skydall", "Upward", "Martelet", "Lethardy", "McAdam", "Bentinck", "Brinson", "Tettley", "Doidge", "Shemilt", "Dunn", "Domanski", "Thorns", "Schapiro", "Pinniger", "Kestell", "Heinonen", "MacArthur", "Saulter", "Furst", "Meredyth", "Erat", "Crusham", "Ivey", "Kealy", "Warstall", "Lazonby", "Dodgshon", "Abeles", "Balas", "Kenner", "Gowenlock", "Brend" };
        private static List<string> positions = new List<string>() { "Manager", "Senior Manager", "Director", "Assistant Director", "CEO", "CTO" };
    
        public static void SaveUsingOracleBulkCopy(DataTable dt)
        {
            try
            {
                using (var connection = new OracleConnection(conString))
                {
                    connection.Open();
                    string[] names = new string[dt.Rows.Count];
                    string[] addresses = new string[dt.Rows.Count];
                    string[] positions = new string[dt.Rows.Count];

                    for (int j = 0; j < 1000; j++)
                    {
                        
                        names[j] = Convert.ToString(dt.Rows[j]["MANAGER_NAME"]);
                        addresses[j] = Convert.ToString(dt.Rows[j]["EMAIL_ADDRESS"]);
                        positions[j] = Convert.ToString(dt.Rows[j]["POSITION"]);
                    }

                    OracleParameter fn = new OracleParameter();
                    fn.OracleDbType = OracleDbType.Varchar2;
                    fn.Value = names;

                    OracleParameter ea = new OracleParameter();
                    ea.OracleDbType = OracleDbType.Varchar2;
                    ea.Value = addresses;

                    OracleParameter pos = new OracleParameter();
                    pos.OracleDbType = OracleDbType.Varchar2;
                    pos.Value = positions;

                    // create command and set properties
                    OracleCommand cmd = connection.CreateCommand();
                    cmd.CommandText = "INSERT INTO MANAGERS (MANAGER_NAME, EMAIL_ADDRESS, POSITION) VALUES (:1, :2, :3)";
                    cmd.ArrayBindCount = names.Length;
                    cmd.Parameters.Add(fn);
                    cmd.Parameters.Add(ea);
                    cmd.Parameters.Add(pos);
                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetDataTableOfManagers()
        {
            DataTable dt = new DataTable();
            DataRow dr;
            string fullName;
            string emailAddress;
            string firstName;
            string lastName;
            string position;

            dt.Columns.Add("MANAGER_NAME", System.Type.GetType("System.String"));
            dt.Columns.Add("EMAIL_ADDRESS", System.Type.GetType("System.String"));
            dt.Columns.Add("POSITION", System.Type.GetType("System.String"));

            var rand = new Random();


            for (int i = 0; i < 1000; i++)
            {

                firstName = firstNames[rand.Next(firstNames.Count)];
                lastName = lastNames[rand.Next(lastNames.Count)];
                position = positions[rand.Next(positions.Count)];
                fullName = firstName + " " + lastName;
                emailAddress = firstName + "." + lastName + "@evilcorp.com";
                
                dr = dt.NewRow();
                dr["MANAGER_NAME"] = fullName;
                dr["EMAIL_ADDRESS"] = emailAddress;
                dr["POSITION"] = position;
                dt.Rows.Add(dr);
                
            }

            return dt;

        }
    }
}
