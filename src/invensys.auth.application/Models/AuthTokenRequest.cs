using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace invensys.auth.application.Models;

public class AuthTokenRequest
{
    [Required] public string grant_type { get; set; }
    public string scope { get; set; }
    [Required] public string client_id { get; set; }
    [Required] public string client_secret { get; set; }
}