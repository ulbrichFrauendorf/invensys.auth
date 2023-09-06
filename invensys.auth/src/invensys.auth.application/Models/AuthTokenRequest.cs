using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invensys.auth.application.Models;

public class AuthTokenRequest
{
    public string grant_type { get; set; }
    public string scope { get; set; }
    public string client_id { get; set; }
    public string client_secret { get; set; }
}