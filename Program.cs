//每月2號刷卡，看那天的匯率*當月的美金
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using ConsoleApp1;

namespace NETFLIX
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal beforePay = 13.99M;
            decimal afterPay = 15.99M;
            int[] getting_expensive = new int[2] {2019, 2};
            DateTime localDate = DateTime.Now;
            Console.Write("輸入從何時開始寄生串流的(YYYY-MM): ");
            string input = Console.ReadLine();
            List<int> joinTime = new List<int> ();
            foreach(string str in input.Split("-"))
                joinTime.Add(Convert.ToInt32(str));
            string url = "https://www.esunbank.com.tw/bank/personal/deposit/rate/forex/exchange-rate-chart?Currency=USD/TWD";
            webControl x = new webControl(url, joinTime);
            decimal totalPay = 0M;
            decimal totalPayInUSD = 0M;
            decimal pay;
            while(true)
            {
                if(x.date[0] == localDate.Year && x.date[1] > localDate.Month)
                    break;
                x.SearchDate();
                decimal rate = x.GetThisMonthRate();
                if(x.date[0] < getting_expensive[0] || 
                    (x.date[0] == getting_expensive[0] && x.date[1] < getting_expensive[1]))
                    pay = beforePay / 4;
                else
                    pay = afterPay / 4;
                if(x.date[1] < 12)
                    x.date[1] += 1;
                else
                {
                    x.date[0] += 1;
                    x.date[1] = 1;
                }
                totalPayInUSD += pay;
                totalPay += rate * pay;
            }
            x.Quit();
            Console.WriteLine($"總共{totalPayInUSD}美金，{totalPay}台幣");
            Console.Write("按任意鍵結束");
            Console.ReadKey();
        }
    }
}
