using Payment.Payments.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Payments.Service
{
    public interface IPaymobService
    {

        Task<string> CreatePaymentSessionAsync(PaymentTransaction transaction);

    }
}
