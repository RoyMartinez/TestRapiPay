using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public sealed class UFE
    {
        private decimal Fee;
        private UFE() => SetFee();
        
        private static UFE _instance;

        public static UFE GetUFE()
        {
            if (_instance == null) 
            { 
                _instance = new UFE(); 
            }
            return _instance;
        }

        public void SetFee()
        {
            Fee = new Random().Next(1, 2);
        }
        public decimal GetFee => Fee;
    }
}
