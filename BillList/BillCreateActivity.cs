using Android.App;
using Android.OS;
using Android.Widget;
using DAL;
using SQLite;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BillList
{
    [Activity(Label = "AddBillActivity", LaunchMode = Android.Content.PM.LaunchMode.SingleTask, ParentActivity = typeof(MainActivity))]
    public class BillCreateActivity : Activity
    {
        private string dbPath;
        public int day = 0;
        public double amount = 0;
        public int val = 0;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddBill);

            dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "bills.db");

            var statusLbl = FindViewById<TextView>(Resource.Id.statusLbl);
            var addBtn = FindViewById<Button>(Resource.Id.addBillBtn);
           var _seekBar = FindViewById<SeekBar>(Resource.Id.dayOfMonthSeekBar);
           var _textView = FindViewById<TextView>(Resource.Id.dayOfMonthTextView);
           var numpick = FindViewById<NumberPicker>(Resource.Id.numberPicker1);
            numpick.MinValue = 0;
            numpick.MaxValue = 999;

            val = numpick.Value;
            numpick.ValueChanged += (sender, args) =>
            {
                val = numpick.Value;
                
            };
            



            _seekBar.ProgressChanged += (object sender, SeekBar.ProgressChangedEventArgs e) => {
                day = e.Progress;
                if (e.FromUser)
                {
                    _textView.Text = string.Format("This bill will be due on day {0}", e.Progress);
                    
                }
            };

            if (savedInstanceState == null)
            {
                statusLbl.Text = "";
                
            }

            else
                statusLbl.Text = savedInstanceState.GetString("status");
               

            addBtn.Click += AddBillButtonClick;
        }

        

        private void AddBillButtonClick(object sender, EventArgs e)
        {
            var statusLbl = FindViewById<TextView>(Resource.Id.statusLbl);
            var billName = FindViewById<EditText>(Resource.Id.billNameTextBox);
            var amount = FindViewById<EditText>(Resource.Id.costInput);
            
            
          







            var b = new Bill
            {

                Name = billName.Text.Trim(),
                Amount = val,
                DayDue = day

              
                };

                using (var db = new SQLiteConnection(dbPath))
                {
                    var billCheck = db.Table<Bill>().ToList().Where(d => Regex.IsMatch(d.Name, b.Name, RegexOptions.IgnoreCase)).FirstOrDefault();

                    if (billCheck == null)
                    {
                        db.Insert(b);
                        statusLbl.Text = "Successfully Added New Bill";
                    }
                    else
                    {
                     
                        db.Update(billCheck);
                        statusLbl.Text = "Successfully Updated Bill";
                    }
                }
            }


        protected override void OnSaveInstanceState(Bundle outState)
        {
            var statusLbl = FindViewById<TextView>(Resource.Id.statusLbl);
            var monthslide = FindViewById<SeekBar>(Resource.Id.dayOfMonthSeekBar);
            var valuespin = FindViewById<NumberPicker>(Resource.Id.numberPicker1);
            outState.PutString("status", statusLbl.Text);
            outState.PutInt("day", monthslide.Progress);
            outState.PutInt("value", valuespin.Value);
            base.OnSaveInstanceState(outState);
        }
    }

        
    }
