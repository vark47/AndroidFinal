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
using DAL;

namespace BillList
{
    public class BillAdapter : BaseAdapter<Bill>
    {
        private Bill[] BillArray { get; set; }
        private Activity Context { get; set; }
        public int daysInMonth = 0;
        private string month { get; set; }
        public BillAdapter(Activity context, Bill[] items, string monthName)
        {
            BillArray = items;
            Context = context;
            month = monthName;
        }

        public override Bill this[int position]
        {
            get
            {
                return BillArray[position];
            }
        }

        public override int Count
        {
            get
            {
                return BillArray.Length;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            if (view == null)
                view = Context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);

            var b = BillArray[position];
            string postfix = "";

         //uses day due to figure out what postfix to use  

            switch (b.DayDue)
            {
                case 1:
                    postfix = "st";
                    break;
                case 2:
                    postfix = "nd";
                    break;
                case 3:
                    postfix = "rd";
                    break;
                case 21:
                    postfix = "st";
                    break;
                case 22:
                    postfix = "nd";
                    break;
                case 23:
                    postfix = "rd";
                    break;
                case 31:
                    postfix = "st";
                    break;

                default:
                    postfix = "th";
                    break;
            }
            // figures out how many days are in the month selected
            switch (month)
            {
                case "January":
                    daysInMonth = 31;
                    break;
                case "February":
                    daysInMonth = 28;
                    break;
                case "March":
                    daysInMonth = 28;
                    break;
                case "April":
                    daysInMonth = 28;
                    break;
                case "May":
                    daysInMonth = 28;
                    break;
                case "June":
                    daysInMonth = 28;
                    break;
                case "July":
                    daysInMonth = 28;
                    break;
                case "August":
                    daysInMonth = 28;
                    break;
                case "September":
                    daysInMonth = 28;
                    break;
                case "October":
                    daysInMonth = 28;
                    break;
                case "November":
                    daysInMonth = 28;
                    break;
                case "December":
                    daysInMonth = 28;
                    break;

            }

            // if the bill is due on a day that dosent exist in the month this automatically sets it to the last day of the month
            if (daysInMonth < b.DayDue)
            {
                b.DayDue = daysInMonth;
            }

            switch (b.DayDue)
            {
                case 1:
                    postfix = "st";
                    break;
                case 2:
                    postfix = "nd";
                    break;
                case 3:
                    postfix = "rd";
                    break;
                case 21:
                    postfix = "st";
                    break;
                case 22:
                    postfix = "nd";
                    break;
                case 23:
                    postfix = "rd";
                    break;
                case 31:
                    postfix = "st";
                    break;

                default:
                    postfix = "th";
                    break;
            }


            

            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = b.Name + " " + "$" + b.Amount.ToString();
            view.FindViewById<TextView>(Android.Resource.Id.Text2).Text = "Is due on the" + " " + b.DayDue.ToString() + postfix + " " + "of the month";

            return view;
        }
    }
}