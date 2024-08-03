﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Dtos.FinancialAccountDtos
{
    public class CreateFinancialAccountDto
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public string AppUserId { get; set; }
    }
}
