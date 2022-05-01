using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApp.Business.Abstract
{
    public interface IHashingService
    {
        byte[] Hash(string password, byte[] salt);
        byte[] ProduceSalt(int size);
        bool CompareHash(string password, byte[] hash, byte[] salt);
    }
}
