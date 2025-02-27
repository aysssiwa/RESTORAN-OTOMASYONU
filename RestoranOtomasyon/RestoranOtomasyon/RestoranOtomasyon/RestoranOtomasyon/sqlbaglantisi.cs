using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
namespace RestoranOtomasyon
{
    internal class sqlbaglantisi
    {
        public OleDbConnection baglanti()
        {
            OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\User\Desktop\RestoranOtomasyon\RestoranOtomasyon\RestoranOtomasyon\RestoranOtomasyonVeritabanı\RestoranOtomasyon.accdb");
            conn.Open();
            return conn;
        } 
    }
}
