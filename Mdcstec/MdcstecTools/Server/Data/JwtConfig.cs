using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MdcstecTools.Server.Data;

public class JwtConfig
{
    public string SecretKey { get; set; }
    public TimeSpan ExpiryTimeFrame { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}
