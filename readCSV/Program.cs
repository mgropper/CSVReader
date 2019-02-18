using System.IO;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System;
using System.Runtime.InteropServices;
using System.Reflection;

namespace readCSV
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //read contents of the CSV File
            string csvData = File.ReadAllText("train_small.csv");
            //remove header from CSV data string
            csvData = csvData.Substring(csvData.IndexOf('\n') + 1);

            //loop over rows
            foreach (string row in csvData.Split('\n'))
            {
                //check if row is not empty
                if (!string.IsNullOrEmpty(row))
                {
                    //split row into cells
                    string[] datum = row.Split(',');

                    //add cells to temp struct
                    data d = new data();
                    d.N = int.Parse(datum[0]);
                    d.Unique_views = int.Parse(datum[1]);
                    d.Tag = datum[2];
                    d.Content = datum[3];

                    //send temp struct to database (using method written in SqliteDatabaseAccess.cs)
                    SqliteDataAccess.Save(d);
                }
            }

            //print the database
            List<data> db = SqliteDataAccess.Load();
            Console.WriteLine("n   unique_views   tag        content");
            foreach (data d in db)
            {
                Console.WriteLine(d.N + " " + d.Unique_views + " " + d.Tag + " " + d.Content);
            }
        }
    }
}
