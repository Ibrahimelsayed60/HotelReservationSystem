using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Payment.Payments.Service
{
    public interface IPaymobHmacValidator
    {
        bool IsValid(JsonElement obj, string receivedHmac);
    }
}
