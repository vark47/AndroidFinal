using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using DAL;
using SQLite;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BillList
{
    [Activity(Label = "Bill List", MainLauncher = true, Icon = "@drawable/icon", LaunchMode = Android.Content.PM.LaunchMode.SingleTask)]
    public class MainActivity : Activity
    {
        private string dbPath;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);





          






            Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner);

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var adapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.planets_array, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spinner.Adapter = adapter;










            var statusLbl = FindViewById<TextView>(Resource.Id.statusTxtView);
            var listBtn = FindViewById<Button>(Resource.Id.viewListBtn);
            var addBtn = FindViewById<Button>(Resource.Id.addBillBtn);

            dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "bills.db");
            
           if (!File.Exists(dbPath))//leave this if statement for release and testing, remove when seeding db
            {
                statusLbl.Text = "Please wait while the Database is created...";
                
                listBtn.Enabled = false;
                
                Task.Run(() => ExtractDatabase());
            }

        

            listBtn.Click += ListButtonClick;
            addBtn.Click += AddButtonClick;
        }
        public void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string month = spinner.GetItemAtPosition(e.Position).ToString();
            string toast = string.Format("The month is {0}", spinner.GetItemAtPosition(e.Position));
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }

        private void ListButtonClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(BillListActivity));

           

             var spinner = FindViewById<Spinner>(Resource.Id.spinner);
             var month = (string)spinner.SelectedItem;

               intent.PutExtra("Month", month);

            StartActivity(intent);
        }

        private void AddButtonClick(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(BillCreateActivity));
            StartActivity(intent);
        }
        

        private void ExtractDatabase()
        {
            using (var instream = Assets.Open("bills.db"))
            {
                using (var outstream = File.Create(dbPath))
                {
                    instream.CopyTo(outstream);
                }
            }

            var statusLbl = FindViewById<TextView>(Resource.Id.statusTxtView);
            var listBtn = FindViewById<Button>(Resource.Id.viewListBtn);
            var addBtn = FindViewById<Button>(Resource.Id.addBillBtn);

            listBtn.Enabled = true;
            addBtn.Enabled = true;
            statusLbl.Text = "Please Select a Month";
        }
    }
}

