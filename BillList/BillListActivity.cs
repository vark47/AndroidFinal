using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using DAL;
using SQLite;

namespace BillList
{
    [Activity(Label = "BillListActivity", LaunchMode = Android.Content.PM.LaunchMode.SingleTask, ParentActivity = typeof(MainActivity))]
    public class BillListActivity : ListActivity
    {
        private string dbPath;
        Bill[] items;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

             dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "bills.db");
 
            SetBillList();
        }

        private void SetBillList()
        {
           

            var monthName = Intent.GetStringExtra("Month");
            

     

            using (var db = new SQLiteConnection(dbPath))
            {
                items = db.Table<Bill>().ToArray();
            }
            

           
            ListAdapter = new BillAdapter(this, items, monthName);
        }

 

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            Intent = intent;
            SetBillList();
        }
    }
}