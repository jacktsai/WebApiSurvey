using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.BLL.Entities;
using WebApi.BLL;

public class MyDataModel
{
    public IQueryable<MyData> MyData
    {
        get
        {
            return new MyData[]
            {
                new MyData { Id = 1, Name = "Data 1" },
                new MyData { Id = 2, Name = "Data 2" },
                new MyData { Id = 3, Name = "Data 3" },
            }.AsQueryable();
        }
    }

    public IQueryable<User> Users
    {
        get
        {
            return new User[]
            {
                new User { Id = 1, Name = "Data 1" },
                new User { Id = 2, Name = "Data 2" },
                new User { Id = 3, Name = "Data 3" },
            }.AsQueryable();
        }
    }
}