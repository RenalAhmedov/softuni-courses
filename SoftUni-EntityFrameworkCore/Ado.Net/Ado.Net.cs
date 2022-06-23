using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace _01.Ado.Net
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            SqlConnection sqlConnection = new SqlConnection(Configuration.CONNECTION_STRING);

            await sqlConnection.OpenAsync();

            int villainId = int.Parse(Console.ReadLine());

            await using (sqlConnection)
            {
                //01.
                //await PrintVillainsWithMoreThan3Minions(sqlConnection);

                //02.
                await PrintVillainsMinionsInfoById(sqlConnection, villainId);
            };

            //Problem 01.VillainName
            static async Task PrintVillainsWithMoreThan3Minions(SqlConnection sqlConnection)
            {
                SqlCommand sqlCommand = new SqlCommand(Queries.VILLAINS_WITH_MORE_THAN_3_MINIONS, sqlConnection);
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                await using (sqlDataReader)
                {
                     while(await sqlDataReader.ReadAsync())
                     {
                        string villainName = sqlDataReader.GetString(0);
                        int minionsCount = sqlDataReader.GetInt32(1);

                        Console.WriteLine($"{villainName} - {minionsCount} ");
                     }
                }
            }

            //Problem 02.VillainNames
            static async Task PrintVillainsMinionsInfoById(SqlConnection sqlConnection, int villainId)
            {
                SqlCommand getVillainNameComand = new SqlCommand(Queries.VILLAIN_NAME_BY_ID, sqlConnection);
               

                getVillainNameComand.Parameters.AddWithValue("@Id", villainId);

                object villainNameObject = await getVillainNameComand.ExecuteScalarAsync();

                if (villainNameObject == null)
                {
                    Console.WriteLine($"No villain with ID {villainId} exists in the database");
                    return;
                }

                string villainName = (string)villainNameObject;

                SqlCommand villainMinionsInfoCmd = new SqlCommand(Queries.VILLAIN_MINIONS_INFO_BY_ID, sqlConnection);

                villainMinionsInfoCmd.Parameters.AddWithValue("@Id", villainId);

                SqlDataReader sqlDataReader = await villainMinionsInfoCmd.ExecuteReaderAsync();

                await using (sqlDataReader)
                {
                    Console.WriteLine($"Villain: {villainName}");

                    if (!sqlDataReader.HasRows)
                    {
                        Console.WriteLine("(no minions)");
                    }
                    else
                    {
                        while (await sqlDataReader.ReadAsync())
                        {
                            long rowNumber = sqlDataReader.GetInt64(0);
                            string minionName = sqlDataReader.GetString(1);
                            int minionAge = sqlDataReader.GetInt32(2);

                            Console.WriteLine($"{rowNumber} {minionName} {minionAge}");
                        }
                    }
                }

            }
        }
    }
}
