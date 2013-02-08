using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Services.Common;

[DataServiceKey("Id")]
public class MyData
{
    public int Id { get; set; }
    public string Name { get; set; }
}