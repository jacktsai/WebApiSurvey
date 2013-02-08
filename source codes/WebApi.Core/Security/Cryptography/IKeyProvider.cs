using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApi.Security.Cryptography
{
    public interface IKeyProvider
    {
        byte[] Key { get; }
        byte[] IV { get; }
    }
}
