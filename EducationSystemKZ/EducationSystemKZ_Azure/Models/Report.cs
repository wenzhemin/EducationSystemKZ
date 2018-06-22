using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EducationSystemKZ_Azure.Models
{
    public class Report
    {
        private DateTime _period;
        private decimal _totalFeeReceived;
        private decimal _totalExpenses;
        private decimal _salary;
        private decimal _otherFee;


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/yyyy}")]
        public DateTime Period
        {
            get { return _period; }
            set { _period = value; }
        }
        public decimal TotalFeeReceived
        {
            get { return _totalFeeReceived; }
            set { _totalFeeReceived = value; }
        }
        public decimal TotalExpenses
        {
            get { return _totalExpenses; }
            set { _totalExpenses = value; }
        }

        public decimal Salary
        {
            get { return _salary; }
            set { _salary = value; }
        }

        public decimal OtherFee
        {
            get { return _otherFee; }
            set { _otherFee = value; }
        }

        public Report()
        {

        }

        public Report(DateTime period, decimal totalFeeReceived, decimal totalExpenses, decimal salary, decimal otherFee)
        {
            _period = period;
            _totalFeeReceived = totalFeeReceived;
            _totalExpenses = totalExpenses;
            _salary = salary;
            _otherFee = otherFee;




        }
    }
}